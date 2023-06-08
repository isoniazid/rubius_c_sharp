public class ConcertService : IConcertService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;
    public ConcertService(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task AddNewConcert(AddConcertDto dto)
    {
        var validation = Validate(dto);
        if (validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);

        var dataToSave = _mapper.Map<Concert>(dto);
        dataToSave.Hall = await _context.Halls.FindAsync(dto.HallId) ?? throw new APIException("Зал не найден", 404);



        await _context.Concerts.AddAsync(dataToSave);

        for (int i = 0; i < dataToSave.Hall.MaxViewers; ++i) //NB может, это можно сделать лучше?
        {
            await _context.Tickets.AddAsync(new Ticket() { Concert = dataToSave, State = TicketState.RELEVANT });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<BaseInfoTicketDto>> GetAllTickets(long id) //NB можно сделать быстрей
    {
        if (await _context.Concerts.FindAsync(id) == null) throw new APIException($"Концерт с id {id} не найден", 404);
        return await _context.Tickets.Include(p => p.Concert).Where(p => p.ConcertId == id).ProjectTo<BaseInfoTicketDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<BaseInfoTicketDto>> GetSoldTickets(long id) //NB можно сделать быстрей
    {
        if (await _context.Concerts.FindAsync(id) == null) throw new APIException($"Концерт с id {id} не найден", 404);
        return await _context.Tickets.Include(p => p.Concert).Where(p => p.ConcertId == id && p.Hash != null).ProjectTo<BaseInfoTicketDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<BaseInfoTicketDto>> GetUnsoldTickets(long id) //NB можно сделать быстрей
    {
        if (await _context.Concerts.FindAsync(id) == null) throw new APIException($"Концерт с id {id} не найден", 404);
        return await _context.Tickets.Include(p => p.Concert).Where(p => p.ConcertId == id && p.Hash == null).ProjectTo<BaseInfoTicketDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<BaseInfoConcertDto> GetBaseInfo(long id)
    {
        return _mapper.Map<BaseInfoConcertDto>(
            await _context.Concerts
            .Include(p => p.Hall)
            .Include(p => p.Performances!).ThenInclude(p => p.Band)
            .FirstOrDefaultAsync(p => p.Id == id)) ?? throw new APIException($"Концерт с id {id} не найден", 404);
    }

    public async Task AddBand(AddBandPerformanceDto dto)
    {
        var validation = Validate(dto);
        if (validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);

        var dataToSave = _mapper.Map<Performance>(dto);
        dataToSave.Concert = await _context.Concerts.FindAsync(dto.ConcertId) ?? throw new APIException("концерт с таким id не найден", 404);
        dataToSave.Band = await _context.Bands.FindAsync(dto.BandId) ?? throw new APIException("группа с таким id не найдена", 404);

        await _context.Performances.AddAsync(dataToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BaseInfoConcertDto>> GetAll()
    {
        return await _context.Concerts.ProjectTo<BaseInfoConcertDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task <BaseInfoConcertDto> CancelConcert(long id)
    {
        var concertToCancel = await _context.Concerts.Include(p=>p.Hall).FirstOrDefaultAsync(p => p.Id == id) ?? throw new APIException("концерт с таким id не найден", 404);
        var concertTickets = await _context.Tickets.Where(p => p.ConcertId == id).ToListAsync();

        foreach(var currentTicket in concertTickets)
        {
            currentTicket.State = TicketState.REFUND;
        }
        concertToCancel.State = ConcertState.CANCELLED;

        await _context.SaveChangesAsync();
        return _mapper.Map<BaseInfoConcertDto>(concertToCancel);
    }

    private ValidationResult Validate(AddConcertDto dto)
    {
        var result = new ValidationResult() { state = ValidationResult.Fail };
        //NB допили валидацию
        if (_context.Halls.Find(dto.HallId) == null) result.message += "Неверно указан зал; ";
        if (_context.Concerts
        .FirstOrDefault(p => p.Date.Day == dto.Date.Day && p.Date.Month == dto.Date.Month && p.Date.Year == dto.Date.Year && p.HallId == dto.HallId)
         != null) result.message += "В указанный день в этом зале уже будет другой концерт; ";
        if (dto.Date <= DateTime.Now) result.message += "Дата указана неверно; ";
        if (dto.Name == string.Empty) result.message += "Укажите название концерта; ";
        if (result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }

    private ValidationResult Validate(AddBandPerformanceDto dto)
    {
        var result = new ValidationResult() { state = ValidationResult.Fail };
        //NB допили валидацию
        if (_context.Bands.Find(dto.BandId) == null) result.message += "Идентификатор группы указан неверно; ";
        if (_context.Concerts.Find(dto.ConcertId) == null) result.message += "Указан несуществующий концерт; ";
        if (result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }
}
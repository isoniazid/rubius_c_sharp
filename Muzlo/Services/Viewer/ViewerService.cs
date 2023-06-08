public class ViewerService : IViewerService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;
    public ViewerService(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task AddViewer(AddViewerDto dto)
    {
        var validation = Validate(dto);
        if(validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);

        var dataToSave = _mapper.Map<Viewer>(dto);
        await _context.Viewers.AddAsync(dataToSave);
        await _context.SaveChangesAsync();
    }

    public async Task BuyTicket(BuyTicketDto dto)
    {
        var possibleTicket = await _context.Tickets
        .Include(p => p.Concert)
        .Where(p => p.Hash == null && p.State != TicketState.REFUND && p.State != TicketState.IRRELEVANT)
        .FirstOrDefaultAsync(p => p.ConcertId == dto.ConcertId) 
        ?? throw new APIException($"Отсутствуют свободные билеты на концерт {dto.ConcertId}",400);

        possibleTicket.Viewer = await _context.Viewers.FindAsync(dto.ViewerId)
        ?? throw new APIException($"Отсутствует пользователь {dto.ViewerId}", 400);

        /////////////////
        Console.WriteLine("Здесь происходит оплата и прочие штуки....");
        ////////////////


        possibleTicket.ViewerId = possibleTicket.Viewer.Id;
        possibleTicket.CreateHash();
         
        await _context.SaveChangesAsync();
    }

    public async Task<List<BaseInfoTicketDto>> GetAllTickets(long viewerId)
    {
        if(await _context.Viewers.FindAsync(viewerId) == null) throw new APIException("Запрошенного пользователя не существует", 404);

        var result = await _context.Tickets
        .Include(p=>p.Concert)
        .Where(p=>p.ViewerId == viewerId)
        .ProjectTo<BaseInfoTicketDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return result;
    }



    private ValidationResult Validate(AddViewerDto dto)
    {
        var result = new ValidationResult() { state = ValidationResult.Fail };

        if (dto.Name == string.Empty) result.message += "Укажите имя; ";
        if (result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }
}
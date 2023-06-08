public class BandService : IBandService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;
    public BandService(ApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task AddNewBand(AddBandDto dto)
    {
        var validation = Validate(dto);
        if (validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);


        var bandToSave = _mapper.Map<Band>(dto);
        await _context.Bands.AddAsync(bandToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<List<BaseInfoBandDto>> GetAll()
    {
        return await _context.Bands.ProjectTo<BaseInfoBandDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<BaseInfoBandDto> GetBaseInfo(long id)
    {
        var raw_result = await _context.Bands.Include(p => p.BandMembers).FirstOrDefaultAsync(p => p.Id == id) ?? throw new APIException($"группа {id} не найден", 404);
        var result = _mapper.Map<BaseInfoBandDto>(raw_result);
        return result;
    }

    public async Task AddMember(AddBandMemberDto dto)
    {
        var validation = Validate(dto);
        if (validation.state == ValidationResult.Fail) throw new APIException(validation.message, 400);

        var bandMemberToSave = _mapper.Map<BandMember>(dto);
        bandMemberToSave.Band = _context.Bands.Find(bandMemberToSave.BandId) ?? throw new APIException("Группа не найдена", 404);
        await _context.BandMembers.AddAsync(bandMemberToSave);
        await _context.SaveChangesAsync();
    }

    public async Task<BandMemberDto> DeleteMember(long id)
    {
        var memberToDelete = await _context.BandMembers.FindAsync(id) ?? throw new APIException($"Член группы с id {id} не найден", 404);
        _context.BandMembers.Remove(memberToDelete);
        await _context.SaveChangesAsync();
        return _mapper.Map<BandMemberDto>(memberToDelete);
    }


    private ValidationResult Validate(AddBandDto dto)
    {
        var result = new ValidationResult() { state = ValidationResult.Fail };

        if (dto.Name == string.Empty) result.message += "Укажите название группы; ";
        if (result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }


    private ValidationResult Validate(AddBandMemberDto dto)
    {
        var result = new ValidationResult() { state = ValidationResult.Fail };

        if (dto.Name == string.Empty) result.message += "Укажите имя участника; ";
        if (_context.Bands.Find(dto.BandId) == null) result.message += $"Группа {dto.BandId} не найдена; ";
        if (result.message == string.Empty) result.state = ValidationResult.Ok;

        return result;
    }
}
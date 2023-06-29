public interface IBandService
{


    public Task AddNewBand(AddBandDto dto);

    public  Task<List<BaseInfoBandDto>> GetAll();

    public  Task<BaseInfoBandDto> GetBaseInfo(long id);

    public  Task AddMember(AddBandMemberDto dto);

    public Task<BandMemberDto> DeleteMember(long id);
}
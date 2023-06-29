public interface IHallService
{
    public Task AddNewHall(AddHallDto dto);
    public Task<List<BaseInfoHallDto>> GetAll();
    public  Task<BaseInfoHallDto> GetBaseInfo(long id);
    public  Task<List<GetAllConcertsFromHallDto>> GetConcerts(long id);

    public Task<BaseInfoHallDto> DeleteHall(long id);
}
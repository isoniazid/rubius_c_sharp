public class BaseInfoBandDto : IMapFrom<Band>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<BandMemberDto>? BandMembers {get; set;}
}
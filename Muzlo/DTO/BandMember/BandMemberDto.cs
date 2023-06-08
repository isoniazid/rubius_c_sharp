public class BandMemberDto : IMapFrom<BandMember>
{
    public long Id {get; set;}
    public string Name { get; set; } = string.Empty;
    public long BandId { get; set; }
}
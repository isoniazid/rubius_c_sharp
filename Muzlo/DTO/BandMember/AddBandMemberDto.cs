public class AddBandMemberDto : IMapTo<BandMember>
{
    public string Name { get; set; } = string.Empty;
    public long BandId { get; set; }
}
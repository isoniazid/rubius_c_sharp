public class Band
{
 public long Id {get; set;}
 public string Name {get; set;} = string.Empty;
 public List<Performance>? Performances {get; set;} 
public List<BandMember>? BandMembers {get; set;}
}
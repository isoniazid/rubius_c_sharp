public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Band> Bands => Set<Band>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();

    public DbSet<Concert> Concerts => Set<Concert>();
    public DbSet<Hall> Halls => Set<Hall>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Viewer> Viewers => Set<Viewer>();
    public DbSet<Performance> Performances => Set<Performance>();
}
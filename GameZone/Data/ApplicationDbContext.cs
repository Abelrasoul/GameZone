namespace GameZone;

public class ApplicationDbContext : DbContext

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {


    }
    public DbSet<Game> Games { get; set; }
    public DbSet<Cateogry> Cateogries { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<GameDevice> GameDevices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<Cateogry>().HasData(new Cateogry[]{
            new Cateogry(){Id=1,Name="Sports"},
            new Cateogry(){Id=2,Name="Action"},
            new Cateogry(){Id=3,Name="Adventure"},
            new Cateogry(){Id=4,Name="Racing"},
            new Cateogry(){Id=5,Name="Fighting"},
            new Cateogry(){Id=6,Name="Film"},

        });
        modelBuilder.Entity<Device>().HasData(new Device[] {
            new Device(){  Id=1,Name="PalyStaion", Icon ="bi bi-playstaio"},
            new Device(){Id=2,Name="Xbox" , Icon ="bi bi-xbox"},
            new Device(){Id=3,Name="Nintendo Switch" , Icon ="bi bi-nintendo-switch"},
            new Device(){Id=4,Name="Pc" , Icon ="bi bi-pc-display"},

        });
        modelBuilder.Entity<GameDevice>().HasKey(e => new { e.GameId, e.DeviceId });
        base.OnModelCreating(modelBuilder);
    }


}

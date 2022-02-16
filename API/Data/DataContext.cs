namespace API.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Park> Parks { get; set; }
    public DbSet<Trail> Trails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Park>()
            .HasData(new Park
            {
                Id = 1,
                Name = "Park 1",
                Created = DateTime.Now,
                Established = new DateTime(2001, 1, 1),
                State = "Shebin"
            }
            ,
            new Park
            {
                Id = 2,
                Name = "Park 2",
                Created = DateTime.Now.AddDays(-7),
                Established = new DateTime(2002, 2, 2),
                State = "Sadat"
            });
    }
}
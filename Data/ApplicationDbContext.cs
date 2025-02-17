using ExamMidTerm.Models;
using Microsoft.EntityFrameworkCore;
namespace ExamMidTerm.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Add your DbSets here (e.g., tables)
    public DbSet<Category> Categories { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Village> Villages { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Commune> Communes { get; set; }

}

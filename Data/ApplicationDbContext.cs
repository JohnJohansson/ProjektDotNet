using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektDotNet.Models;

namespace ProjektDotNet.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    //Our tables/classes
    public DbSet<Game> Games { get; set; }
    public DbSet<GameConsole> GameConsoles { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<GameContent> GameContents { get; set; }
}

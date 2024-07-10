using IdentityCore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace IdentityCore.Data;

public class IdentityCoreContext : IdentityDbContext<User>
{
    public IdentityCoreContext(DbContextOptions<IdentityCoreContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Environment.GetEnvironmentVariable("DBCONN") != null)
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DBCONN"), options =>
            {
                options.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), new List<string>());
                options.MigrationsHistoryTable("_MigrationHistory", "Identity");
            });
        else
            optionsBuilder.UseInMemoryDatabase("TelegramBotInMemory");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Identity");
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

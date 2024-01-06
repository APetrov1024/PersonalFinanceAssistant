using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PersonalFinanceAssistant.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class PersonalFinanceAssistantDbContextFactory : IDesignTimeDbContextFactory<PersonalFinanceAssistantDbContext>
{
    public PersonalFinanceAssistantDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        PersonalFinanceAssistantEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PersonalFinanceAssistantDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new PersonalFinanceAssistantDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PersonalFinanceAssistant.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets(System.Reflection.Assembly.Load("PersonalFinanceAssistant.DbMigrator"));

        return builder.Build();
    }
}

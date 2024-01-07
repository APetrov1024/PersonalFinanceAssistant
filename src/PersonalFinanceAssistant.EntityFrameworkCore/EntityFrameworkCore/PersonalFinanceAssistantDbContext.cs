using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.FinanceAccounts;
using PersonalFinanceAssistant.FinanceOperations;
using PersonalFinanceAssistant.Goods;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace PersonalFinanceAssistant.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class PersonalFinanceAssistantDbContext :
    AbpDbContext<PersonalFinanceAssistantDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Good> Goods { get; set; }
    public DbSet<GoodCategory> GoodCategories { get; set; }
    public DbSet<FinanceAccount> FinanceAccounts { get; set; }
    public DbSet<FinanceOperation> FinanceOperations { get; set; }


    public PersonalFinanceAssistantDbContext(DbContextOptions<PersonalFinanceAssistantDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(PersonalFinanceAssistantConsts.DbTablePrefix + "YourEntities", PersonalFinanceAssistantConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Good>(b =>
        {
            b.ToTable("Goods");
            b.Property(x => x.Name).HasMaxLength(GoodConsts.MaxNameLength).IsRequired();
            b.HasOne(x => x.Category).WithMany(x => x.Goods).HasForeignKey(x => x.CategoryId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<GoodCategory>(b =>
        {
            b.ToTable("GoodCategories");
            b.Property(x => x.Name).HasMaxLength(GoodCategoryConsts.MaxNameLength).IsRequired();
            b.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.ParentCategory).WithMany(x => x.ChildCategories).HasForeignKey(x => x.ParentCategoryId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<FinanceAccount>(b =>
        {
            b.ToTable("FinanceAccounts");
            b.Property(x => x.Name).HasMaxLength(FinanceAccountConsts.MaxNameLength).IsRequired();
            b.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<FinanceOperation>(b =>
        {
            b.ToTable("FinanceOperations");
            b.Property(x => x.Comment).HasMaxLength(FinanceOperationConsts.MaxCommentLength).IsRequired(false);
            b.HasOne(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.FinanceAccount).WithMany().HasForeignKey(x => x.FinanceAccountId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Good).WithMany().HasForeignKey(x => x.GoodId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        });
    }
}

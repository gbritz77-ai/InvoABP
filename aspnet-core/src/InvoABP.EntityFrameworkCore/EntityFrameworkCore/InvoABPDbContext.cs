using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

// 👇 add your domain namespaces
using InvoABP.Customers;
using InvoABP.Invoices;

namespace InvoABP.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class InvoABPDbContext :
    AbpDbContext<InvoABPDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    // Identity
    public DbSet<IdentityUser> Users { get; set; } = default!;
    public DbSet<IdentityRole> Roles { get; set; } = default!;
    public DbSet<IdentityClaimType> ClaimTypes { get; set; } = default!;
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; } = default!;
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; } = default!;
    public DbSet<IdentityLinkUser> LinkUsers { get; set; } = default!;
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; } = default!;
    public DbSet<IdentitySession> Sessions { get; set; } = default!;

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; } = default!;
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; } = default!;

    #endregion

    #region App Entities (your own)

    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;

    #endregion

    public InvoABPDbContext(DbContextOptions<InvoABPDbContext> options)
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

        // Customer
        builder.Entity<Customer>(b =>
        {
            b.ToTable(InvoABPConsts.DbTablePrefix + "Customers", InvoABPConsts.DbSchema);
            b.ConfigureByConvention(); // auto-configure base class props

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            b.Property(x => x.Email)
                .HasMaxLength(256);

            b.Property(x => x.Phone)
                .HasMaxLength(32);

            b.Property(x => x.BillingAddress)
                .HasMaxLength(512);
        });

        // Invoice
        builder.Entity<Invoice>(b =>
        {
            b.ToTable(InvoABPConsts.DbTablePrefix + "Invoices", InvoABPConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(32);

            b.HasOne<Customer>()                // link to customer
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.Lines)
                .WithOne()
                .HasForeignKey(l => l.InvoiceId);
        });

        // InvoiceLine
        builder.Entity<InvoiceLine>(b =>
        {
            b.ToTable(InvoABPConsts.DbTablePrefix + "InvoiceLines", InvoABPConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(256);
        });

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(InvoABPConsts.DbTablePrefix + "YourEntities", InvoABPConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}

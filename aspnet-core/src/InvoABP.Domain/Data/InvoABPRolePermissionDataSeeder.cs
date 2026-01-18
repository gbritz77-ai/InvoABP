using InvoABP.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace InvoABP.Data;

public class InvoABPRolePermissionDataSeeder :
    IDataSeedContributor,
    ITransientDependency
{
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionManager _permissionManager;
    private readonly ILogger<InvoABPRolePermissionDataSeeder> _logger;
    private readonly IGuidGenerator _guidGenerator;

    public InvoABPRolePermissionDataSeeder(
        IIdentityRoleRepository roleRepository,
        IdentityRoleManager roleManager,
        IPermissionManager permissionManager,
        ILogger<InvoABPRolePermissionDataSeeder> logger,
        IGuidGenerator guidGenerator)
    {
        _roleRepository = roleRepository;
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _logger = logger;
        _guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // 1) Ensure Staff role exists
        var staffRole = await _roleRepository.FindByNormalizedNameAsync("STAFF");
        if (staffRole == null)
        {
            staffRole = new IdentityRole(
                _guidGenerator.Create(),
                "staff",
                context.TenantId
            );

            (await _roleManager.CreateAsync(staffRole)).CheckErrors();
            _logger.LogInformation("Created 'staff' role");
        }

        // 2) Admin role usually exists already
        var adminRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
        if (adminRole == null)
        {
            _logger.LogWarning("Admin role not found; skipping admin permission seeding.");
        }

        // 3) Grant permissions for Admin
        if (adminRole != null)
        {
            await GrantAdminPermissionsAsync(adminRole);
        }

        // 4) Grant permissions for Staff
        await GrantStaffPermissionsAsync(staffRole);
    }

    private async Task GrantAdminPermissionsAsync(IdentityRole adminRole)
    {
        // Admin gets full control over Customers + Invoices
        await SetRolePermissionAsync(adminRole, InvoABPPermissions.Customers.Default, true);
        await SetRolePermissionAsync(adminRole, InvoABPPermissions.Customers.Delete, true);

        await SetRolePermissionAsync(adminRole, InvoABPPermissions.Invoices.Default, true);
        await SetRolePermissionAsync(adminRole, InvoABPPermissions.Invoices.Create, true);
    }

    private async Task GrantStaffPermissionsAsync(IdentityRole staffRole)
    {
        // Staff:
        // - Can see customers (Default)
        // - Cannot delete customers (no Customers.Delete)
        // - Can view & create invoices
        await SetRolePermissionAsync(staffRole, InvoABPPermissions.Customers.Default, true);
        await SetRolePermissionAsync(staffRole, InvoABPPermissions.Customers.Delete, false);

        await SetRolePermissionAsync(staffRole, InvoABPPermissions.Invoices.Default, true);
        await SetRolePermissionAsync(staffRole, InvoABPPermissions.Invoices.Create, true);
    }

    private async Task SetRolePermissionAsync(IdentityRole role, string permissionName, bool isGranted)
    {
        // "R" = Role permission value provider
        await _permissionManager.SetAsync(
            permissionName,
            "R",
            role.Name,
            isGranted
        );
    }
}

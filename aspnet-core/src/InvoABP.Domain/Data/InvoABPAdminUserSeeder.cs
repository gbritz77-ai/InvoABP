using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

public class InvoABPAdminUserSeeder :
    IDataSeedContributor,
    ITransientDependency
{
    private readonly IIdentityUserRepository _userRepository;
    private readonly IdentityUserManager _userManager;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IdentityRoleManager _roleManager;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ILogger<InvoABPAdminUserSeeder> _logger;

    public InvoABPAdminUserSeeder(
        IIdentityUserRepository userRepository,
        IdentityUserManager userManager,
        IIdentityRoleRepository roleRepository,
        IdentityRoleManager roleManager,
        IGuidGenerator guidGenerator,
        ILogger<InvoABPAdminUserSeeder> logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _roleManager = roleManager;
        _guidGenerator = guidGenerator;
        _logger = logger;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // 1) Ensure admin role exists
        var adminRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
        if (adminRole == null)
        {
            adminRole = new IdentityRole(
                _guidGenerator.Create(),
                "admin",
                context.TenantId
            );

            (await _roleManager.CreateAsync(adminRole)).CheckErrors();
            _logger.LogInformation("Created 'admin' role.");
        }

        // 2) Ensure user exists
        var adminUser = await _userRepository.FindByNormalizedUserNameAsync("ADMIN1");
        if (adminUser == null)
        {
            adminUser = new IdentityUser(
                _guidGenerator.Create(),
                "admin1",
                "admin1@demo.local",
                context.TenantId
            );

            const string newPassword = "Password@123";

            (await _userManager.CreateAsync(adminUser, newPassword)).CheckErrors();
            (await _userManager.AddToRoleAsync(adminUser, adminRole.Name)).CheckErrors();

            _logger.LogInformation(
                "Created admin user 'admin1' with password '{Password}' and role 'admin'.",
                newPassword
            );
        }
        else
        {
            _logger.LogInformation("Admin user already exists.");
        }
    }
}

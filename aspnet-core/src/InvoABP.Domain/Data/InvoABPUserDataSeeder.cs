using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace InvoABP.Data;

public class InvoABPUserDataSeeder :
    IDataSeedContributor,
    ITransientDependency
{
    private readonly IIdentityUserRepository _userRepository;
    private readonly IdentityUserManager _userManager;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly ILogger<InvoABPUserDataSeeder> _logger;

    public InvoABPUserDataSeeder(
        IIdentityUserRepository userRepository,
        IdentityUserManager userManager,
        IIdentityRoleRepository roleRepository,
        ILogger<InvoABPUserDataSeeder> logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleRepository = roleRepository;
        _logger = logger;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        // Ensure staff role exists
        var staffRole = await _roleRepository.FindByNormalizedNameAsync("STAFF");
        if (staffRole == null)
        {
            _logger.LogWarning("Staff role not found, user seeding skipped.");
            return;
        }

        // Check if staff user exists
        var staffUser = await _userRepository.FindByNormalizedUserNameAsync("STAFF1");
        if (staffUser != null)
        {
            return;
        }

        staffUser = new IdentityUser(
            Guid.NewGuid(),
            "staff1",
            "staff1@demo.local",
            context.TenantId
        );

        (await _userManager.CreateAsync(staffUser, "P@ssword123")).CheckErrors();
        (await _userManager.AddToRoleAsync(staffUser, staffRole.Name)).CheckErrors();

        _logger.LogInformation("Created staff user 'staff1' with role 'staff'.");
    }
}

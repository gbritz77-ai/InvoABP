using InvoABP.Localization;
using InvoABP.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace InvoABP.Permissions;

public class InvoABPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // One group for your app
        var appGroup = context.AddGroup(
            InvoABPPermissions.GroupName,
            L("Permission:InvoABP")
        );

        // Customers
        var customers = appGroup.AddPermission(
            InvoABPPermissions.Customers.Default,
            L("Permission:Customers")
        );
        customers.AddChild(
            InvoABPPermissions.Customers.Delete,
            L("Permission:Customers.Delete")
        );

        // Invoices
        var invoices = appGroup.AddPermission(
            InvoABPPermissions.Invoices.Default,
            L("Permission:Invoices")
        );
        invoices.AddChild(
            InvoABPPermissions.Invoices.Create,
            L("Permission:Invoices.Create")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InvoABPResource>(name);
    }
}

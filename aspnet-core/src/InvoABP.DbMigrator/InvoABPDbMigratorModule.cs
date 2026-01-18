using InvoABP.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace InvoABP.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InvoABPEntityFrameworkCoreModule),
    typeof(InvoABPApplicationContractsModule)
    )]
public class InvoABPDbMigratorModule : AbpModule
{
}

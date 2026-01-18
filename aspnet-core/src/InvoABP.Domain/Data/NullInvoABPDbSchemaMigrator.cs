using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace InvoABP.Data;

/* This is used if database provider does't define
 * IInvoABPDbSchemaMigrator implementation.
 */
public class NullInvoABPDbSchemaMigrator : IInvoABPDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

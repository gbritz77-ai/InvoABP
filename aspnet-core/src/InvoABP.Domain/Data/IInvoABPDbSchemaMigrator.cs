using System.Threading.Tasks;

namespace InvoABP.Data;

public interface IInvoABPDbSchemaMigrator
{
    Task MigrateAsync();
}

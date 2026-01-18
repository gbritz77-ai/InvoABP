using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoABP.Invoices;

public interface IInvoiceAppService :
    ICrudAppService<
        InvoiceDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateInvoiceDto>
{
    Task<InvoiceDto> UpdateStatusAsync(Guid id, InvoiceStatus status);
}

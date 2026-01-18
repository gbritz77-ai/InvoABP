using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InvoABP.Customers;

public interface ICustomerAppService :
    ICrudAppService<
        CustomerDto,                 // TGetOutputDto
        Guid,                        // TKey
        PagedAndSortedResultRequestDto, // TGetListInput
        CreateUpdateCustomerDto>     // TCreateUpdateDto
{
}

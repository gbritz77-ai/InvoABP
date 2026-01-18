using System;
using Volo.Abp.Application.Dtos;

namespace InvoABP.Customers;

public class CustomerDto : EntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? BillingAddress { get; set; }
}

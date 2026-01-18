using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace InvoABP.Customers;

public class Customer : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string Name { get; private set; } = default!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? BillingAddress { get; private set; }

    private Customer() { }

    public Customer(Guid id, string name, string? email, string? phone, string? billingAddress)
        : base(id)
    {
        SetName(name);
        Email = email;
        Phone = phone;
        BillingAddress = billingAddress;
    }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: 128);
    }

    public void Update(string name, string? email, string? phone, string? billingAddress)
    {
        SetName(name);
        Email = email;
        Phone = phone;
        BillingAddress = billingAddress;
    }
}

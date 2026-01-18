using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace InvoABP.Invoices;

public class Invoice : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public string Number { get; private set; } = default!;
    public Guid CustomerId { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public InvoiceStatus Status { get; private set; }

    public decimal Subtotal { get; private set; }
    public decimal TaxTotal { get; private set; }
    public decimal GrandTotal { get; private set; }

    public ICollection<InvoiceLine> Lines { get; private set; } = new List<InvoiceLine>();

    // EF Core ctor
    private Invoice() { }

    public Invoice(Guid id, string number, Guid customerId, DateTime invoiceDate)
        : base(id)
    {
        Number = Check.NotNullOrWhiteSpace(number, nameof(number), maxLength: 32);
        CustomerId = customerId;
        InvoiceDate = invoiceDate;
        Status = InvoiceStatus.Draft;
    }

    public void SetStatus(InvoiceStatus status) => Status = status;
    public void SetInvoiceDate(DateTime date) => InvoiceDate = date;
    public void SetCustomer(Guid customerId) => CustomerId = customerId;

    public void SetLines(IEnumerable<InvoiceLine> lines)
    {
        Lines.Clear();
        foreach (var line in lines)
        {
            Lines.Add(line);
        }
        RecalculateTotals();
    }

    public void RecalculateTotals()
    {
        Subtotal = Lines.Sum(l => l.LineTotal);
        TaxTotal = Lines.Sum(l => l.TaxAmount);
        GrandTotal = Subtotal + TaxTotal;
    }
}

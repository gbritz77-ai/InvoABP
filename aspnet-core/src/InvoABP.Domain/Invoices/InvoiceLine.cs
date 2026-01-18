using System;
using Volo.Abp.Domain.Entities;

namespace InvoABP.Invoices;

public class InvoiceLine : Entity<Guid>
{
    public Guid InvoiceId { get; private set; }

    public string Description { get; private set; } = default!;
    public decimal Qty { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TaxRate { get; private set; }

    public decimal LineTotal { get; private set; }
    public decimal TaxAmount { get; private set; }

    // EF Core needs a parameterless ctor
    private InvoiceLine()
    {
    }

    public InvoiceLine(
        Guid id,
        Guid invoiceId,
        string description,
        decimal qty,
        decimal unitPrice,
        decimal taxRate)
        : base(id)
    {
        UpdateInternal(invoiceId, description, qty, unitPrice, taxRate);
    }

    public void Update(
        Guid invoiceId,
        string description,
        decimal qty,
        decimal unitPrice,
        decimal taxRate)
    {
        UpdateInternal(invoiceId, description, qty, unitPrice, taxRate);
    }

    private void UpdateInternal(
        Guid invoiceId,
        string description,
        decimal qty,
        decimal unitPrice,
        decimal taxRate)
    {
        InvoiceId = invoiceId;
        Description = description ?? string.Empty;
        Qty = qty;
        UnitPrice = unitPrice;
        TaxRate = taxRate;

        LineTotal = qty * unitPrice;
        TaxAmount = LineTotal * taxRate / 100m;
    }
}

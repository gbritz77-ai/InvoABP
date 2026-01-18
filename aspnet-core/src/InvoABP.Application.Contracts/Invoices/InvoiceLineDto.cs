using System;

namespace InvoABP.Invoices;

public class InvoiceLineDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public decimal Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public decimal LineTotal { get; set; }
    public decimal TaxAmount { get; set; }
}

namespace InvoABP.Invoices;

public class CreateUpdateInvoiceLineDto
{
    public string Description { get; set; } = default!;
    public decimal Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
}

using System;
using System.Collections.Generic;

namespace InvoABP.Invoices;

public class CreateUpdateInvoiceDto
{
    public Guid CustomerId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public List<CreateUpdateInvoiceLineDto> Lines { get; set; } = new();
}

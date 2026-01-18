using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace InvoABP.Invoices;

public class InvoiceDto : EntityDto<Guid>
{
    public string Number { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public InvoiceStatus Status { get; set; }

    public decimal Subtotal { get; set; }
    public decimal TaxTotal { get; set; }
    public decimal GrandTotal { get; set; }

    public List<InvoiceLineDto> Lines { get; set; } = new();
}

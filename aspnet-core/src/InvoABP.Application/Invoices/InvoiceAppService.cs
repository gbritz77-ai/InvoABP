using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using InvoABP.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InvoABP.Invoices;

[Authorize(InvoABPPermissions.Invoices.Default)]
public class InvoiceAppService :
    CrudAppService<
        Invoice,                    // Entity
        InvoiceDto,                 // DTO returned to client
        Guid,                       // PK
        PagedAndSortedResultRequestDto,
        CreateUpdateInvoiceDto>,    // DTO used for create/update
    IInvoiceAppService
{
    public InvoiceAppService(IRepository<Invoice, Guid> repository)
        : base(repository)
    {
        GetPolicyName = InvoABPPermissions.Invoices.Default;
        GetListPolicyName = InvoABPPermissions.Invoices.Default;
        CreatePolicyName = InvoABPPermissions.Invoices.Default;
        UpdatePolicyName = InvoABPPermissions.Invoices.Default;
        DeletePolicyName = InvoABPPermissions.Invoices.Delete;
    }

    private string GenerateInvoiceNumber()
    {
        // Simple number generator for now
        return $"INV-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
    }

    // ---------- CREATE: DTO -> Entity ----------
    protected override Invoice MapToEntity(CreateUpdateInvoiceDto input)
    {
        var invoiceId = GuidGenerator.Create();
        var number = GenerateInvoiceNumber();

        // Use your rich constructor
        var invoice = new Invoice(
            invoiceId,
            number,
            input.CustomerId,
            input.InvoiceDate
        );

        // Build lines via InvoiceLine constructor
        foreach (var lineDto in input.Lines)
        {
            var line = new InvoiceLine(
                GuidGenerator.Create(),
                invoiceId,
                lineDto.Description,
                lineDto.Qty,
                lineDto.UnitPrice,
                lineDto.TaxRate
            );

            invoice.Lines.Add(line);
        }

        invoice.RecalculateTotals();
        return invoice;
    }

    // ---------- UPDATE: DTO -> existing Entity ----------
    protected override void MapToEntity(CreateUpdateInvoiceDto input, Invoice entity)
    {
        entity.SetCustomer(input.CustomerId);
        entity.SetInvoiceDate(input.InvoiceDate);

        var newLines = input.Lines.Select(l =>
            new InvoiceLine(
                GuidGenerator.Create(),
                entity.Id,
                l.Description,
                l.Qty,
                l.UnitPrice,
                l.TaxRate
            )
        ).ToList();

        entity.SetLines(newLines);
        entity.RecalculateTotals();
    }

    // ---------- READ: Entity -> DTO (single) ----------
    protected override InvoiceDto MapToGetOutputDto(Invoice entity)
    {
        return new InvoiceDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            InvoiceDate = entity.InvoiceDate,
            Subtotal = entity.Subtotal,
            TaxTotal = entity.TaxTotal,
            GrandTotal = entity.GrandTotal,
            Status = entity.Status,
            Lines = entity.Lines.Select(l => new InvoiceLineDto
            {
                Id = l.Id,
                Description = l.Description,
                Qty = l.Qty,
                UnitPrice = l.UnitPrice,
                TaxRate = l.TaxRate,
                LineTotal = l.LineTotal,
                TaxAmount = l.TaxAmount
            }).ToList()
        };
    }

    // ---------- READ: Entity -> DTO (list) ----------
    protected override InvoiceDto MapToGetListOutputDto(Invoice entity)
    {
        return MapToGetOutputDto(entity);
    }

    // ---------- EXTRA: Update status (from IInvoiceAppService) ----------
    [Authorize(InvoABPPermissions.Invoices.Default)]
    public async Task<InvoiceDto> UpdateStatusAsync(Guid id, InvoiceStatus status)
    {
        var invoice = await Repository.GetAsync(id);
        invoice.SetStatus(status);

        await Repository.UpdateAsync(invoice, autoSave: true);

        return MapToGetOutputDto(invoice);
    }
}

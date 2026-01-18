using AutoMapper;
using InvoABP.Customers;
using InvoABP.Invoices;

namespace InvoABP;

public class InvoABPApplicationAutoMapperProfile : Profile
{
    public InvoABPApplicationAutoMapperProfile()
    {
        // Customers
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateUpdateCustomerDto, Customer>();

        // Invoices
        CreateMap<InvoiceLine, InvoiceLineDto>();
        CreateMap<CreateUpdateInvoiceLineDto, InvoiceLine>();
        CreateMap<Invoice, InvoiceDto>();
    }
}

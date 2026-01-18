namespace InvoABP.Customers;

public class CreateUpdateCustomerDto
{
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? BillingAddress { get; set; }
}

using System;
using Microsoft.AspNetCore.Authorization;
using InvoABP.Customers;
using InvoABP.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InvoABP.Customers
{
    [Authorize(InvoABPPermissions.Customers.Default)]
    public class CustomerAppService :
        CrudAppService<
            Customer,                    // Entity
            CustomerDto,                 // DTO returned to client
            Guid,                        // Primary key
            PagedAndSortedResultRequestDto,
            CreateUpdateCustomerDto>,    // DTO used for create/update
        ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, Guid> repository)
            : base(repository)
        {
            GetPolicyName = InvoABPPermissions.Customers.Default;
            GetListPolicyName = InvoABPPermissions.Customers.Default;
            CreatePolicyName = InvoABPPermissions.Customers.Default;
            UpdatePolicyName = InvoABPPermissions.Customers.Default;
            DeletePolicyName = InvoABPPermissions.Customers.Delete;
        }

        // ---- Manual mappings to avoid "No object mapping was found" ----

        // DTO -> Entity (create)
        protected override Customer MapToEntity(CreateUpdateCustomerDto input)
        {
            // Use the public constructor + GuidGenerator from CrudAppService
            var id = GuidGenerator.Create();

            return new Customer(
                id,
                input.Name,
                input.Email,
                input.Phone,
                input.BillingAddress
            );
        }

        // DTO -> existing Entity (update)
        protected override void MapToEntity(CreateUpdateCustomerDto input, Customer entity)
        {
            // Use the domain method instead of touching setters
            entity.Update(
                input.Name,
                input.Email,
                input.Phone,
                input.BillingAddress
            );
        }

        // Entity -> DTO (read)
        protected override CustomerDto MapToGetOutputDto(Customer entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,                    // getter is public, so this is fine
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                BillingAddress = entity.BillingAddress
            };
        }
    }
}

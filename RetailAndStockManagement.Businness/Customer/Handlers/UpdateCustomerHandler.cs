using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Customer.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Customer.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, UpdateCustomerModel>
{
    private readonly RetailAndStockManagementContext _context;

    public UpdateCustomerHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<UpdateCustomerModel> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            return new UpdateCustomerModel
            {
                IsSuccess = false,
                Message = $"Customer with ID {request.Id} not found."
            };
        }

        customer.Username = request.Username;
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerModel
        {
            IsSuccess = true,
            Message = $"Customer with ID {customer.Id} updated successfully."
        };
    }
}

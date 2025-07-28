using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Customer.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Customer.Handlers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, DeleteCustomerModel>
{
    private readonly RetailAndStockManagementContext _context;

    public DeleteCustomerHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<DeleteCustomerModel> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            return new DeleteCustomerModel
            {
                IsSuccess = false,
                Message = $"Customer with ID {request.Id} not found."
            };
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteCustomerModel
        {
            IsSuccess = true,
            Message = $"Customer with ID {request.Id} has been deleted."
        };
    }
}

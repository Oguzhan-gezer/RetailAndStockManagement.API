// Handlers/GetAllCustomerHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Customer.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Customer.Handlers;

public class GetAllCustomerHandler
    : IRequestHandler<GetAllCustomerRequest, List<GetAllCustomerModel>>
{
    private readonly RetailAndStockManagementContext _context;

    public GetAllCustomerHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<GetAllCustomerModel>> Handle(
        GetAllCustomerRequest request,
        CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Select(x => new GetAllCustomerModel
            {
                Id = x.Id,
                Username = x.Username
            })
            .ToListAsync(cancellationToken);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Customer.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Customer.Handlers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerModel>
{
    private readonly RetailAndStockManagementContext _context;

    public CreateCustomerHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<CreateCustomerModel> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcı adı zaten var mı kontrol et
        var isExist = await _context.Customers
            .AnyAsync(x => x.Username == request.Username, cancellationToken);

        if (isExist)
        {
            return new CreateCustomerModel
            {
                IsSuccess = false,
                Message = $"Username '{request.Username}' is already taken. Please choose another one."
            };
        }

        var entity = new CustomerModel
        {
            Username = request.Username,
            Password = request.Password
        };

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCustomerModel
        {
            IsSuccess = true,
            Message = $"Customer '{entity.Username}' saved with ID {entity.Id}"
        };
    }
}

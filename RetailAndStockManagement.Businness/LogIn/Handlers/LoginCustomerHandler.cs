using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Customer.Requests;
using RetailAndStockManagement.Data.EF;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Customer.Handlers
{
    public class LoginCustomerHandler : IRequestHandler<LoginCustomerRequest, LoginCustomerModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public LoginCustomerHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<LoginCustomerModel> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
        {

            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

            if (customer == null || customer.Password != request.Password)
            {
                return new LoginCustomerModel
                {
                    IsSuccess = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            }

            return new LoginCustomerModel
            {
                IsSuccess = true,
                Message = "Giriş başarılı.",
                Username = customer.Username,
                CustomerId = customer.Id
            };
        }
    }
}

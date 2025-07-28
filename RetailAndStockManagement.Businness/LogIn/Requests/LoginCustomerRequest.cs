using MediatR;
using RetailAndStockManagement.Businness.Customer.Models;

namespace RetailAndStockManagement.Businness.Customer.Requests
{
    public class LoginCustomerRequest : IRequest<LoginCustomerModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
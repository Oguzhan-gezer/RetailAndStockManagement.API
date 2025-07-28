using MediatR;
using RetailAndStockManagement.Businness.Customer.Models;

namespace RetailAndStockManagement.Businness.Customer.Requests;

public class CreateCustomerRequest : IRequest<CreateCustomerModel>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

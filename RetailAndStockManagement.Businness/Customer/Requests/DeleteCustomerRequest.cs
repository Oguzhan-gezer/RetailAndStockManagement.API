using MediatR;
using RetailAndStockManagement.Businness.Customer.Models;

namespace RetailAndStockManagement.Businness.Customer.Requests;

public class DeleteCustomerRequest : IRequest<DeleteCustomerModel>
{
    public int Id { get; set; }
}
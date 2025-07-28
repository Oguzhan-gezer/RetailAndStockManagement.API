using MediatR;
using RetailAndStockManagement.Businness.Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Customer.Requests;

public class UpdateCustomerRequest : IRequest<UpdateCustomerModel>
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
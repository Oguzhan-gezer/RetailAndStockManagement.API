using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Data.EF;
[Table("Customer", Schema = "dbo")]


public class CustomerModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

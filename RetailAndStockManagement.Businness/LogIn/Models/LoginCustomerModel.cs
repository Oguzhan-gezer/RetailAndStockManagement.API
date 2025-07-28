namespace RetailAndStockManagement.Businness.Customer.Models
{
    public class LoginCustomerModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public int CustomerId { get; set; }
    }
}
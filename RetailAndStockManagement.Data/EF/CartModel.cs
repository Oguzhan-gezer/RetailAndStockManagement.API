using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailAndStockManagement.Data.EF
{
    [Table("Cart", Schema = "dbo")]
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
    }
}
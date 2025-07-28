using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RetailAndStockManagement.Data.EF
{
    [Table("CartItem", Schema = "dbo")]
    public class CartItemModel
    {
        [Key]
        public int CartItemId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [JsonIgnore]
        public CartModel Cart { get; set; }

        [Required]
        public string Barcode { get; set; }

        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public StoreModel Store { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageBase64 { get; set; }
    }
}
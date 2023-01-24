using GeekShooping.CartAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.CartAPI.Models
{
    [Table("cart_header")]
    public class CartHeader : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("cupon_code")]
        public string? CouponCode { get; set; }
    }
}

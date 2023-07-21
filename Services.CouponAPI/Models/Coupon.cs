using System.ComponentModel.DataAnnotations;

namespace Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double CouponAmount { get; set; }
        public int MinAmount { get; set; }
        //public DateTime LastUpdated { get; set; }
    }
}

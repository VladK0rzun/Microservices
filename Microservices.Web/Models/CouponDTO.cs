namespace Services.Web.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double CouponAmount { get; set; }
        public int MinAmount { get; set; }
    }
}

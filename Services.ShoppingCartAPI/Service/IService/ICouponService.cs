using Services.ShoppingCartAPI.Models.Dto;

namespace Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}

using Services.Web.Models;

namespace Services.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetCoupon(string CouponCode);
        Task<ResponseDTO?> GetAllCouponsAsync();
        Task<ResponseDTO?> GetCouponByIdAsync(int id);
        Task<ResponseDTO?> CreateCouponsAsync(CouponDTO couponDTO);
        Task<ResponseDTO?> UpdateCouponsAsync(CouponDTO couponDTO);
        Task<ResponseDTO?> DeleteCouponsAsync(int id);
    }
}

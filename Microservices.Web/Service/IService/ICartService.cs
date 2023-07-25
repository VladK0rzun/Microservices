using Services.Web.Models;

namespace Services.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDTO?> GetcartByUserIdAsync(string userId);
        Task<ResponseDTO?> UpsertCartAsync(CartDTO cartDTO);
        Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDTO?> ApplyCouponAsync(CartDTO cartDTO);
        
    }
}

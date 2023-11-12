using Services.Web.Models;

namespace Services.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDTO?> CreateOrder(CartDTO cartDTO);
        Task<ResponseDTO?> CreateStripeSession(StripeRequestDTO stripeRequestDTO);
    }
}

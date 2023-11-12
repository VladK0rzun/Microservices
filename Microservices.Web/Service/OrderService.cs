using Services.Web.Models;
using Services.Web.Service.IService;
using Services.Web.Utility;

namespace Services.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
           _baseService = baseService;
        }



        public async Task<ResponseDTO?> CreateOrder(CartDTO cartDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        public async Task<ResponseDTO?> CreateStripeSession(StripeRequestDTO stripeRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = stripeRequestDTO,
                Url = SD.OrderAPIBase + "/api/order/CreateStripeSession"
            });
        }
    }
}

using Services.Web.Models;

namespace Services.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);
    }
}

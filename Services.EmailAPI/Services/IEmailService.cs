using Services.EmailAPI.Message;
using Services.EmailAPI.Models.DTO;

namespace Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDTO cartDTO);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardMessage rewardDTO);
    }
}

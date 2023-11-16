using Services.RewardAPI.Message;

namespace Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardMessage rewardMessage);
    }
}

using Microsoft.EntityFrameworkCore;
using Services.RewardAPI.Message;
using Services.RewardAPI.Data;
using System.Text;
using Services.RewardAPI.Models;

namespace Services.RewardAPI.Services
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public RewardService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }


        public async Task UpdateRewards(RewardMessage rewardMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                   OrderId = rewardMessage.OrderId,
                   RewardsActivity = rewardMessage.RewardsActivity,
                   UserId = rewardMessage.UserId,
                   RewardsDate = DateTime.Now
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
            }
        }

    }
}

﻿using Microsoft.EntityFrameworkCore;
using Services.EmailAPI.Data;
using Services.EmailAPI.Message;
using Services.EmailAPI.Models;
using Services.EmailAPI.Models.DTO;
using System.Text;

namespace Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDTO cartDTO)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDTO.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDTO.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");

            await LogAndEmail(message.ToString(), cartDTO.CartHeader.Email);
        }

        public async Task LogOrderPlaced(RewardMessage rewardDTO)
        {
            string message = "<br/>New OrderPlaced. <br> Order ID : " + rewardDTO.OrderId;
            await LogAndEmail(message, "vlad2012759@gmail.com");
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "<br/>New User Registered. <br> Email : " + email;
            await LogAndEmail(message, "vlad2012759@gmail.com");
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

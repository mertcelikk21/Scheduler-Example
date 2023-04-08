using Coravel.Invocable;
using SchedulerForTcmbData.Abstract;
using SchedulerForTcmbData.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SchedulerForTcmbData.Services
{
    public class CustomScheduler : IInvocable
    {
        private readonly ITcmbRequestService tcmbRequestService;
        private readonly IEmailSender _emailSender;
        public CustomScheduler(ITcmbRequestService tcmbRequestService, IEmailSender emailSender)
        {
            this.tcmbRequestService = tcmbRequestService;
            _emailSender = emailSender;
        }
        public Task Invoke()
        {
            try
            {
                var data = tcmbRequestService.GetData();
                var emailAdressList = new List<EmailAddress>()
                {
                  new EmailAddress(){ Address = "Email address to be sent", DisplayName = "The sender's first and last name" },
                  new EmailAddress(){ Address = "Email address to be sent", DisplayName = "The sender's first and last name" }
                };
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                var message = new Message(emailAdressList, "Güncel Kur Bilgileri", jsonString);
                _emailSender.SendEmail(message);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

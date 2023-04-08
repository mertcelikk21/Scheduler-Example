using SchedulerForTcmbData.Models;

namespace SchedulerForTcmbData.Abstract
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}

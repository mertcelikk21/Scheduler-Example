using SchedulerForTcmbData.Models;

namespace SchedulerForTcmbData.Abstract
{
    public interface ITcmbRequestService
    {
        List<CurrencyExchangeDto> GetData();
    }
}

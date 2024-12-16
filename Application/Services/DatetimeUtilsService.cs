using Application.Contracts.Services;

namespace Application.Services
{
    public class DatetimeUtilsService : IDatetimeUtilsService
    {
        public DateTime GetNow()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}

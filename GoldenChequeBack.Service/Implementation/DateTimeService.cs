using GoldenChequeBack.Service.Contract;


namespace GoldenChequeBack.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
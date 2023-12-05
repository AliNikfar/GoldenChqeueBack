using GoldenChequeBack.Domain.Setting;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}

using GeekShooping.Email.Messages;

namespace GeekShooping.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
namespace DeliverySoftware.Business.Utilities
{
    public interface IEmailController
    {
        Task SendEmail (Email email);
    }
}
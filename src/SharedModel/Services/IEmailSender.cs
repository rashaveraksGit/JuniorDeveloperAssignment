namespace SharedModel.Services
{
    public interface IEmailSender
    {
        void Send(string orderString);
    }
}
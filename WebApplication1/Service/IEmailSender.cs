namespace WebApplication1.Service
{
    interface IEmailSender
    {
        public void Send(string from, string to, string subject, string html);
    }
}

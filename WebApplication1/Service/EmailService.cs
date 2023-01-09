using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace WebApplication1.Service
{
    public class EmailService : IEmailSender
    {
        //private readonly SmtpClient _smtp;
        private readonly string _host;
        private readonly int _port;
        private readonly SecureSocketOptions _connectionSecureOption;

        public EmailService(string host, int port, SecureSocketOptions connectionSecureOption)
        {
            this._host = host;
            this._port = port;
            this._connectionSecureOption = connectionSecureOption;
        }

        public async void SendAsync(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var _smtp = new SmtpClient();
            await _smtp.ConnectAsync(_host, _port, _connectionSecureOption);
            await _smtp.AuthenticateAsync("d.parol@business-care.pl", "P@tolek98");
            await _smtp.SendAsync(email);
            await _smtp.DisconnectAsync(true);
        }

        public void Send(string from, string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var _smtp = new SmtpClient();

            _smtp.Connect(_host, _port, _connectionSecureOption);
            _smtp.Authenticate("d.parol@business-care.pl", "");
            _smtp.Send(email);
            _smtp.Disconnect(true);

        }


    }
}

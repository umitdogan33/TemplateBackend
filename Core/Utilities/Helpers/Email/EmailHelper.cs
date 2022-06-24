using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Utilities.Helpers.Email
{
	public class EmailHelper : IEmailHelper
	{
		public IConfiguration Configuration { get; }
		EmailOption _emailOptions;
		public EmailHelper(IConfiguration configuration)
		{
			Configuration = configuration;
			_emailOptions = configuration.GetSection("EmailOptions").Get<EmailOption>();
		}

		public void Send(string toEmail,string body,string subject)
		{
			SmtpClient client = new SmtpClient();
			client.Port = _emailOptions.Port; // Genelde 587 ve 25 portları kullanılmaktadır.
			client.Host = _emailOptions.Host; // Hostunuzun smtp için mail domaini.
			client.EnableSsl = true; // Güvenlik ayarları, host'a ve gönderilen server'a göre değişebilir.
			client.Timeout = 1000000; // Milisaniye cinsten timeout
			client.DeliveryMethod = SmtpDeliveryMethod.Network; // Mailin yollanma methodu
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(_emailOptions.YourEmailAddress, _emailOptions.YourPassword); // Burada hangi hesabı kullanarak mail yollayacaksanız onun ayarlarını yapmanız gerekiyor
			MailMessage mm = new MailMessage(_emailOptions.YourEmailAddress, toEmail, body, subject); // Hangi mail adresinden nereye, konu ve içerik mail ayarlarını yapabilirsiniz
			mm.From = new MailAddress(_emailOptions.YourEmailAddress, "display sender name");
			mm.IsBodyHtml = true; // True: Html olarak Gönderme, False: Text olarak Gönderme	
			mm.BodyEncoding = UTF8Encoding.UTF8; // UTF8 encoding ayarı
			mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; // Hata olduğunda uyarı ver 
			client.Send(mm); // Mail yolla
		}
	}
}

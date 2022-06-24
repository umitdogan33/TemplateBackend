using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.Email
{
	public interface IEmailHelper
	{
		void Send(string toEmail, string body, string subject);
	}
}

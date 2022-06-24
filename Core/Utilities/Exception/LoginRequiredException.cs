using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Messages
{

	[Serializable]
	public class LoginRequiredException : Exception
	{
		//public MyException() { }	
		public LoginRequiredException() : base("Login Required") { }
		public LoginRequiredException(string message, Exception inner) : base(message, inner) { }
		protected LoginRequiredException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

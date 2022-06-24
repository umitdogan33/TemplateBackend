using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
   public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
		public Guid Client { get; set; }
		public string RefreshToken { get; set; }
	}
}

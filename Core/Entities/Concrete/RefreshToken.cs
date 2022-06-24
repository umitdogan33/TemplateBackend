using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Concrete
{
	//[Table("Users")]
	public class RefreshToken:IEntity
	{
		[Key]

		public int RefreshTokenId { get; set; }
		public Guid UserId { get; set; }
		public Guid Client { get; set; }
		public string TokenValue { get; set; }
		public string RefreshTokenValue { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }

        public virtual User user { get; set; }
	}
}

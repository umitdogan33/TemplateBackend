using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        //[Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int OperationClaimId { get; set; }

        public virtual User user {get; set;}
		public virtual OperationClaim operationClaim { get; set; }
	}
}

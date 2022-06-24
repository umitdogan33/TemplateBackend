using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Core.Entities.Concrete
{
    //[Table("OperationClaims")]
    public class OperationClaim:IEntity
    {

        [Key]
        public int Id { get; set; }
        public String Name { get; set; }

		public virtual List<UserOperationClaim> userOperationClaims { get; set; }
	}
}

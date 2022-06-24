using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
   public interface IUserDal:IEntityRepository<User>
    {
        public List<OperationClaim> GetClaims(Guid id);
    }
}

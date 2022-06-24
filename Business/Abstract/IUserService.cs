using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<User> GetById(Guid Id);
        IDataResult<List<OperationClaim>> GetClaims(Guid id);
        IResult EditProfil(User user, string password);
        IDataResult<User> GetUserByEmail(string email);
        User GetByMail(string mail);

    }
}

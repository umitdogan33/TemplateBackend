using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public IResult Add(User user)
        {
            IResult result = BusinessRules.Run(SameUserName(user.Email));

            if (result != null)
            {
                return result;
            }

            _userDal.Add(user);
            return new SuccessResult(Message.AddedUser);
        }



        public IResult Delete(User user)
        {
            if (_userDal.GetAll(p => p.Email == user.Email).Any())
            {
                _userDal.Delete(user);
                return new SuccessResult(Message.Deleted);
            }
            return new ErrorResult("kullanıcı bulunamadı");

        }

        public IResult EditProfil(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HasingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var updatedUser = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = user.Status
            };
            _userDal.Update(updatedUser);
            return new SuccessResult(Message.UserUpdated);
        }

        public IDataResult<List<User>> GetAll()
        {

            return new SuccessDataResult<List<User>>("kullanıcılar listelendi");
        }

        public IDataResult<User> GetById(Guid Id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == Id));
        }

        public User GetByMail(string mail)
        {
            var user = (_userDal.Get(u => u.Email == mail));
            return user;
        }

        public IDataResult<List<OperationClaim>> GetClaims(Guid id)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(id));
        }

        public IDataResult<User> GetUserByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Message.UpdateUser);
        }



        IDataResult<List<User>> IUserService.GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }



        IDataResult<User> IUserService.GetById(Guid Id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == Id));
        }



        private IResult SameUserName(string Email)
        {
            var result = _userDal.GetAll(p => p.Email == Email).Any();
            if (result)
            {
                return new ErrorResult(Message.SameUserName);
            }

            return new SuccessResult();
        }
    }
}

using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class RefreshTokenManager : IRefreshTokenService
	{
		IRefreshTokenDal refreshTokenDal;

		public RefreshTokenManager(IRefreshTokenDal refreshTokenDal)
		{
			this.refreshTokenDal = refreshTokenDal;
		}

		public IResult Add(RefreshToken entity)
		{
			refreshTokenDal.Add(entity);
			return new SuccessResult(Message.Added);
		}

		public IResult Delete(int entity)
		{
			var result = GetById(entity);
			refreshTokenDal.Delete(result.Data);
			return new SuccessResult(Message.Deleted);
			
		}

		public IDataResult<List<RefreshToken>> GetAll()
		{
			return new SuccessDataResult<List<RefreshToken>>(refreshTokenDal.GetAll());
		}

		public IDataResult<RefreshToken> GetById(int entity)
		{
			return new SuccessDataResult<RefreshToken>(refreshTokenDal.Get(p=>p.RefreshTokenId == entity));
		}

		public IDataResult<RefreshToken> GetByRefreshToken(string refreshToken)
		{
			return new SuccessDataResult<RefreshToken>(refreshTokenDal.Get(p => p.RefreshTokenValue == refreshToken));
		}

		public IDataResult<RefreshToken> GetByRefreshTokenAndClientId(string refreshToken,Guid client)
		{
			return new SuccessDataResult<RefreshToken>(refreshTokenDal.Get(p => p.RefreshTokenValue == refreshToken && p.Client == client));
		}

        public IDataResult<List<RefreshToken>> GetByRefreshTokenEndDateAndUserId(Guid userId)
        {
			return new SuccessDataResult<List<RefreshToken>>(refreshTokenDal.GetAll(p=>p.RefreshTokenEndDate <= DateTime.Now && p.UserId == userId));
        }

        public IDataResult<RefreshToken> GetByToken(string Token)
		{
			return new SuccessDataResult<RefreshToken>(refreshTokenDal.Get(p => p.TokenValue == Token));
		}

		public IDataResult<List<RefreshToken>> GetByUserId(Guid userId)
		{
			return new SuccessDataResult<List<RefreshToken>>(refreshTokenDal.GetAll(p => p.UserId == userId));
		}

		public IResult Update(RefreshToken entity)
		{
			refreshTokenDal.Update(entity);
			return new SuccessResult(Message.Updated);
		}
	}
}

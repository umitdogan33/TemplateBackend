using Core;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
	public interface IRefreshTokenService:IBusinessRepository<RefreshToken,int>
	{
		IDataResult<RefreshToken> GetByRefreshToken(string refreshToken);
		IDataResult<RefreshToken> GetByToken(string Token);
		IDataResult<List<RefreshToken>> GetByRefreshTokenEndDateAndUserId(Guid userId);
		IDataResult<List<RefreshToken>> GetByUserId(Guid userId);
		IDataResult<RefreshToken> GetByRefreshTokenAndClientId(string refreshToken, Guid client);
	}
}

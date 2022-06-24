using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.RefreshTokenHelper
{
	public interface IRefreshTokenHelper
	{
		string CreateRefreshToken();
		RefreshToken CreateNewRefreshToken(User user, string tokenValue);
		RefreshToken CreateNewRefreshTokenAndStillClient(User user, string tokenValue);
		//void CreateDifferentRefreshToken(RefreshToken refreshToken);
	}
}

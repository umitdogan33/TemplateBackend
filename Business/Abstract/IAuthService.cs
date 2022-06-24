using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IAuthService
    {
        IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IResult Logout(string refreshToken, Guid client);

        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult UserExists(string email);
        IDataResult<RefreshTokenReturnEntity> CreateAccessTokenWithRefreshToken(string refreshToken, Guid client);
    }
}

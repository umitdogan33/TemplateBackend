using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;

namespace Business.Helpers.RefreshTokenHelper
{
	public class RefreshTokenHelper : IRefreshTokenHelper
	{
        private TokenOptions _tokenOptions;
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenHelper(IRefreshTokenService refreshTokenService, IConfiguration configuration)
		{
            _refreshTokenService = refreshTokenService;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }


        public string CreateRefreshToken()
		{
			var number = new byte[32];
			using (var random = RandomNumberGenerator.Create())
			{
				random.GetBytes(number);
				return Convert.ToBase64String(number);
			}
		}

        public RefreshToken CreateNewRefreshToken(User user, string tokenValue)
		{
            //var result = _refreshTokenService.GetByUserId(user.Id);
			//foreach (var item in result.Data)
			//{
			//	if (item.RefreshTokenEndDate <= DateTime.Now)
			//	{
   //                 _refreshTokenService.Delete(item.RefreshTokenId);
			//	}
			//}

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                RefreshTokenValue = CreateRefreshToken(),
                TokenValue = tokenValue,
                RefreshTokenEndDate = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration),
                Client = Guid.NewGuid(),
            };
            
            _refreshTokenService.Add(newRefreshToken);

            return newRefreshToken;
        }

		public RefreshToken CreateNewRefreshTokenAndStillClient(User user, string tokenValue)
		{
            var result = _refreshTokenService.GetByToken(tokenValue);


            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                RefreshTokenValue = CreateRefreshToken(),
                TokenValue = tokenValue,
                Client = result.Data.Client,
                RefreshTokenEndDate= DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration),
            };

            _refreshTokenService.Add(newRefreshToken);

            return newRefreshToken;
        }
	}
}

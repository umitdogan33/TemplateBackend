using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.Helpers.RefreshTokenHelper;
using Core.Entities.Concrete;
using Core.Entities.Dto;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
	public class AuthManager : IAuthService
	{
		private IUserService _userService;
		private ITokenHelper _tokenHelper;
		private IRefreshTokenHelper _refreshTokenHelper;
		private IRefreshTokenService _refreshTokenService;


		public AuthManager(IUserService userService, ITokenHelper tokenHelper, IRefreshTokenHelper refreshTokenHelper, IRefreshTokenService refreshTokenService)
		{
			_userService = userService;
			_tokenHelper = tokenHelper;
			_refreshTokenHelper = refreshTokenHelper;
			_refreshTokenService = refreshTokenService;
		}


		public IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto, string password)
		{
			byte[] passwordHash, passwordSalt;

			HasingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
			var user = new User
			{
				Email = userForRegisterDto.Email,
				FirstName = userForRegisterDto.FirstName,
				LastName = userForRegisterDto.LastName,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				Status = true
			};
			var TokenModel = CreateAccessToken(user);
			var refreshToken = _refreshTokenHelper.CreateNewRefreshToken(user,TokenModel.Data.Token);
			TokenModel.Data.RefreshToken = refreshToken.RefreshTokenValue;
			TokenModel.Data.Client = refreshToken.Client;
			_userService.Add(user);
			return new SuccessDataResult<AccessToken>(TokenModel.Data,Message.UserRegistered);
		}

		public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
		{

			var userToCheck = _userService.GetByMail(userForLoginDto.Email);
			if (userToCheck == null)
			{
				return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı");
			}

			if (!HasingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
			{
				return new ErrorDataResult<AccessToken>("Parola hatası");
			}
			var TokenModel = CreateAccessToken(userToCheck);
			var refreshTokenHelperReturnValue = _refreshTokenHelper.CreateNewRefreshToken(userToCheck, TokenModel.Data.Token);
			TokenModel.Data.RefreshToken = refreshTokenHelperReturnValue.RefreshTokenValue;
			TokenModel.Data.Client = refreshTokenHelperReturnValue.Client; 
			return TokenModel;
		}


		public IResult UserExists(string email)
		{
			if (_userService.GetByMail(email) != null)
			{
				return new ErrorResult(Message.UserAlreadyExists);
			}
			return new SuccessResult("ekleme başarılı");
		}

		public IDataResult<AccessToken> CreateAccessToken(User user)
		{
			var claims = _userService.GetClaims(user.Id);
			var accessToken = _tokenHelper.CreateToken(user, claims.Data);
			return new SuccessDataResult<AccessToken>(accessToken, Message.AccessTokenCreated);
		}

		public IDataResult<RefreshTokenReturnEntity> CreateAccessTokenWithRefreshToken(string refreshToken,Guid client)
		{
			var result = _refreshTokenService.GetByRefreshTokenAndClientId(refreshToken, client).Data;
			if (result==null) {
				throw new Exception("Çıkış Yapıp Tekrar Deneyiniz");
			}
			User user = _userService.GetById(result.UserId).Data;
			var token2 = CreateAccessToken(user);
			var newData = _refreshTokenHelper.CreateNewRefreshTokenAndStillClient(user,result.TokenValue);
			_refreshTokenService.Delete(result.RefreshTokenId);
			RefreshTokenReturnEntity refreshTokenReturnEntity = new RefreshTokenReturnEntity{ 
			RefreshToken = newData.RefreshTokenValue,
			Token = token2.Data.Token,
			};
			return new SuccessDataResult<RefreshTokenReturnEntity>(refreshTokenReturnEntity);
		}

		public IResult Logout(string refreshToken, Guid client)
		{
			var data = _refreshTokenService.GetByRefreshTokenAndClientId(refreshToken, client);
			if (data.Data != null)
			{
				_refreshTokenService.Delete(data.Data.RefreshTokenId);
			}

			return new SuccessResult("");


		}
    }
}

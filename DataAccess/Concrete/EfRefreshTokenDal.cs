using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
	public class EfRefreshTokenDal:EfEntityRepositoryBase<RefreshToken,LayeredArchitectureContext>,IRefreshTokenDal
	{
	}
}

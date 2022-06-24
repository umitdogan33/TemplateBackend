using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
	public interface IBusinessRepository<Entity,IdType>
	{
		IResult Add(Entity entity);
		IResult Delete(IdType entity);
		IResult Update(Entity entity);
		IDataResult<List<Entity>> GetAll();

		IDataResult<Entity> GetById(IdType entity);

	}
}

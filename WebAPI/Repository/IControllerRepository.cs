using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Repository
{
	public interface IControllerRepository<TEntity,idType>
	{
		ActionResult Add(TEntity entity);
		ActionResult Delete(idType id);
		ActionResult Update(TEntity entity);
		ActionResult GetAll();
		ActionResult GetById(idType id);
	}
}

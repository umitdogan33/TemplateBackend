using Core.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Repository
{
	[Route("api/[controller]")]
	[ApiController]
	public class ControllerRepositoryBase<TEntity, idType> : Controller, IControllerRepository<TEntity, idType>
	{
		private IBusinessRepository<TEntity,idType> _businessRepository;

		public ControllerRepositoryBase(IBusinessRepository<TEntity, idType> businessRepository)
		{
			_businessRepository = businessRepository;
		}

		public ActionResult Add(TEntity entity)
		{
			var result = _businessRepository.Add(entity);
			if (result.Success)
				return Ok(result);
			return BadRequest(result);
		}

		public ActionResult Delete(idType id)
		{
			var result = _businessRepository.Delete(id);
			if (result.Success)
				return Ok(result);
			return BadRequest(result);
		}

		public ActionResult GetAll()
		{
			var result = _businessRepository.GetAll();
			if (result.Success)
				return Ok(result);
			return BadRequest(result);
		}

		public ActionResult GetById(idType id)
		{
			var result = _businessRepository.GetById(id);
			if (result.Success)
				return Ok(result);
			return BadRequest(result);
		}

		public ActionResult Update(TEntity entity)
		{
			var result = _businessRepository.Add(entity);
			if (result.Success)
				return Ok(result);
			return BadRequest(result);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Festival.BL.Mappers;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Festival.DAL.Interfaces;

namespace Festival.BL.Repositories
{
    public class StageRepository : IRepository<StageListModel,StageDetailModel>
	{
		private readonly IDbContextFactory _dbContextFactory;

		public StageRepository(IDbContextFactory dBContextFactory)
        {
			_dbContextFactory = dBContextFactory;
        }

		public IEnumerable<StageListModel> GetAll()
        {
			using var dbContext = _dbContextFactory.Create();
			return dbContext.Stages
				.Select(e => StageMapper.MapToListModel(e)).ToArray();
		}

		public StageDetailModel GetById(Guid id)
		{
			using var dbContext = _dbContextFactory.Create();

			var entity = dbContext.Stages.Single(t => t.Id == id);

			return StageMapper.MapToDetailModel(entity);
        }

		public StageDetailModel InsertOrUpdate(StageDetailModel model)
        {
			using var dbContext = _dbContextFactory.Create();


			var entity = StageMapper.MapToEntity(model, null);

			dbContext.Stages.Update(entity);
			dbContext.SaveChanges();

			return StageMapper.MapToDetailModel(entity);
        }

		public void Delete(Guid EntityId)
        {
			using var dbContext = _dbContextFactory.Create();

			var entity = new StageEntity { Id = EntityId };

			dbContext.Remove(entity);
			dbContext.SaveChanges();
		}
	}
}

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
    public class BandRepository : IRepository<BandListModel,BandDetailModel> 
    {
        private readonly IDbContextFactory _dbContextFactory;
        public BandRepository(IDbContextFactory dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public IEnumerable<BandListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Bands
                .Select(e => BandMapper.MapToListModel(e)).ToArray();
        }

        public BandDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Bands.SingleOrDefault(t => t.Id == id);

            return BandMapper.MapToDetailModel(entity);
        }

        public BandDetailModel InsertOrUpdate(BandDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = BandMapper.MapToEntity(model, null);

            dbContext.Bands.Update(entity);
            dbContext.SaveChanges();

            return BandMapper.MapToDetailModel(entity);
        }
        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new BandEntity { Id = id };

            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }
    }
}

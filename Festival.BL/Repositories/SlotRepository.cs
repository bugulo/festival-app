﻿using System;
using System.Collections.Generic;
using System.Linq;
using Festival.BL.Mappers;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Factories;
using Festival.DAL.Interfaces;

namespace Festival.BL.Repositories
{
    public class SlotRepository : IRepository<SlotListModel,SlotDetailModel>
    {
        private readonly IDbContextFactory _dbContextFactory;
        public SlotRepository(IDbContextFactory dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        private bool IsSlotAvailable(SlotDetailModel model, DAL.FestivalDbContext dbContext)
        {
            return dbContext.Slots.Where(
                    x => x.StartAt < model.FinishAt && model.StartAt < x.FinishAt &&
                    (x.StageId == model.StageId || x.BandId == model.BandId) &&
                    (x.Id != model.Id)
                ).Count() == 0;
        }

        public IEnumerable<SlotListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Slots
                .Select(e => SlotMapper.MapToListModel(e)).ToArray();
        }

        public SlotDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Slots.Single(t => t.Id == id);

            return SlotMapper.MapToDetailModel(entity);
        }

        public SlotDetailModel InsertOrUpdate(SlotDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();
            
            if (IsSlotAvailable(model, dbContext))
            {
                var entity = SlotMapper.MapToEntity(model, null);

                dbContext.Slots.Update(entity);
                dbContext.SaveChanges();

                return SlotMapper.MapToDetailModel(entity);
            }

            return null;
        }

        public void Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new SlotEntity { Id = id };

            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }

    }
}

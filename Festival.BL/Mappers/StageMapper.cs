using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.DAL.Enums;
using Festival.BL.Factories;
using Festival.BL.Interfaces;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Interfaces;

namespace Festival.BL.Mappers
{
    public static class StageMapper
    {
        public static StageDetailModel MapToDetailModel(StageEntity entity) =>
            entity == null ? null : new StageDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                PhotoURL = entity.PhotoURL,
                Description = entity.Description
            };
        public static StageListModel MapToListModel(StageEntity entity) =>
            entity == null ? null : new StageListModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        public static StageEntity MapToEntity(StageDetailModel detailModel, IEntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new DefaultEntityFactory()).Create<StageEntity>(detailModel.Id);
            entity.Id = detailModel.Id;
            entity.Name = detailModel.Name;
            entity.Description = detailModel.Description;
            entity.PhotoURL = detailModel.PhotoURL;
            return entity;
        }
    }
}

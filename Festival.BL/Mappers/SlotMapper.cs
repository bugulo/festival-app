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
    internal static class SlotMapper
    {
        public static SlotDetailModel MapToDetailModel(SlotEntity entity) =>
            entity == null ? null : new SlotDetailModel
            {
                Id = entity.Id,
                BandId = entity.BandId,
                StageId = entity.StageId,
                
                StartAt = entity.StartAt,
                FinishAt = entity.FinishAt
            };
        public static SlotListModel MapToListModel(SlotEntity entity) =>
            entity == null ? null : new SlotListModel
            {
                Id = entity.Id,

                StartAt = entity.StartAt,
                FinishAt = entity.FinishAt
            };
        public static SlotEntity MapToEntity(SlotDetailModel detailModel, IEntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new DefaultEntityFactory()).Create<SlotEntity>(detailModel.Id);
            entity.Id = detailModel.Id;
            entity.BandId = detailModel.BandId;
            entity.StageId = detailModel.StageId;
            
            entity.StartAt = detailModel.StartAt;
            entity.FinishAt = detailModel.FinishAt;
            
            return entity;
        }
    }
}

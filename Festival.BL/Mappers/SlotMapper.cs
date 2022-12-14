using Festival.BL.Factories;
using Festival.BL.Models;
using Festival.DAL.Entities;
using Festival.DAL.Interfaces;

namespace Festival.BL.Mappers
{
    public static class SlotMapper
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
                FinishAt = entity.FinishAt,

                BandName = entity.Band.Name,
                StageName = entity.Stage.Name
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

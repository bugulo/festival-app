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
    internal static class BandMapper
    {
        public static BandDetailModel MapToDetailModel(BandEntity entity) =>
            entity == null ? null : new BandDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Genre = entity.Genre,
                PhotoURL = entity.PhotoURL,
                Country = (Country)entity.Country,
                Description = entity.Description
            };
        public static BandListModel MapToListModel(BandEntity entity) =>
            entity == null ? null : new BandListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Genre = entity.Genre,
                Country = (Country)entity.Country
            };
        public static BandEntity MapToEntity(BandDetailModel detailModel, IEntityFactory entityFactory)
        {
            var entity = (entityFactory ??= new DefaultEntityFactory()).Create<BandEntity>(detailModel.Id);
            entity.Id = detailModel.Id;
            entity.Name = detailModel.Name;
            entity.Description = detailModel.Description;
            entity.PhotoURL = detailModel.PhotoURL;
            entity.Country = (DAL.Enums.Country)detailModel.Country;
            return entity;
        }
    }
}

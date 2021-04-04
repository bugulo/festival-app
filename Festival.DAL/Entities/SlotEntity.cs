using System;
using System.Collections.Generic;

namespace Festival.DAL.Entites
{
    public record SlotEntity : BaseEntity
    {
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }

        public Guid BandId { get; set; }
        public BandEntity Band { get; set; }

        public Guid StageId { get; set; }
        public StageEntity Stage { get; set; }

        public sealed class SlotEqualityComparer : IEqualityComparer<SlotEntity>
        {
            public bool Equals(SlotEntity x, SlotEntity y)
            {
                if(ReferenceEquals(x, y)) return true;
                if(ReferenceEquals(x, null)) return false;
                if(ReferenceEquals(y, null)) return false;

                return x.Id.Equals(y.Id) 
                    && DateTime.Equals(x.StartAt, y.StartAt)
                    && DateTime.Equals(x.StartAt, y.StartAt)
                    && BandEntity.BandWithoutSlotsComparer.Equals(x.Band, y.Band)
                    && StageEntity.StageWithoutSlotsComparer.Equals(x.Stage, y.Stage);
            }
            
            public int GetHashCode(SlotEntity entity)
            {
                unchecked
                {
                    var hashCode = entity.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (entity.StartAt.GetHashCode());
                    hashCode = (hashCode * 397) ^ (entity.FinishAt.GetHashCode());
                    hashCode = (hashCode * 397) ^ (entity.Band?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Stage?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<SlotEntity> SlotComparer { get; } = new SlotEqualityComparer();
    }
}
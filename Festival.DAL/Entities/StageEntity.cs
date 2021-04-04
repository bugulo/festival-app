using System.Linq;
using System.Collections.Generic;

namespace Festival.DAL.Entities
{
    public record StageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }

        public ICollection<SlotEntity> Slots { get; set; } = new List<SlotEntity>();

        public sealed class StageEqualityComparer : IEqualityComparer<StageEntity>
        {
            public bool Equals(StageEntity x, StageEntity y)
            {
                if(ReferenceEquals(x, y)) return true;
                if(ReferenceEquals(x, null)) return false;
                if(ReferenceEquals(y, null)) return false;

                return x.Id.Equals(y.Id) 
                    && string.Equals(x.Name, y.Name)
                    && string.Equals(x.Description, y.Description)
                    && string.Equals(x.PhotoURL, y.PhotoURL)
                    && x.Slots.OrderBy(slot => slot.Id).SequenceEqual(y.Slots.OrderBy(slot => slot.Id), SlotEntity.SlotComparer);
            }
            
            public int GetHashCode(StageEntity entity)
            {
                unchecked
                {
                    var hashCode = entity.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (entity.Name?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Description?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.PhotoURL?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Slots?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        public sealed class StageWithoutSlotsEqualityComparer : IEqualityComparer<StageEntity>
        {
            public bool Equals(StageEntity x, StageEntity y)
            {
                if(ReferenceEquals(x, y)) return true;
                if(ReferenceEquals(x, null)) return false;
                if(ReferenceEquals(y, null)) return false;

                return x.Id.Equals(y.Id) 
                    && string.Equals(x.Name, y.Name)
                    && string.Equals(x.Description, y.Description)
                    && string.Equals(x.PhotoURL, y.PhotoURL);
            }
            
            public int GetHashCode(StageEntity entity)
            {
                unchecked
                {
                    var hashCode = entity.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (entity.Name?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Description?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.PhotoURL?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<StageEntity> StageComparer { get; } = new StageEqualityComparer();
        public static IEqualityComparer<StageEntity> StageWithoutSlotsComparer { get; } = new StageWithoutSlotsEqualityComparer();
    }
}
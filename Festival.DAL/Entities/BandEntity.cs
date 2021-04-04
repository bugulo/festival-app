using System.Linq;
using System.Collections.Generic;

using Festival.DAL.Enums;

namespace Festival.DAL.Entites
{
    public record BandEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string PhotoURL { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
        
        public string ProgramDescription { get; set; }

        public ICollection<SlotEntity> Slots { get; } = new List<SlotEntity>();

        public sealed class BandEqualityComparer : IEqualityComparer<BandEntity>
        {
            public bool Equals(BandEntity x, BandEntity y)
            {
                if(ReferenceEquals(x, y)) return true;
                if(ReferenceEquals(x, null)) return false;
                if(ReferenceEquals(y, null)) return false;

                return x.Id.Equals(y.Id) 
                    && string.Equals(x.Name, y.Name)
                    && string.Equals(x.Genre, y.Genre)
                    && string.Equals(x.PhotoURL, y.PhotoURL)
                    && string.Equals(x.Description, y.Description)
                    && string.Equals(x.ProgramDescription, y.ProgramDescription)
                    && x.Country == y.Country
                    && x.Slots.OrderBy(slot => slot.Id).SequenceEqual(y.Slots.OrderBy(slot => slot.Id), SlotEntity.SlotComparer);
            }
            
            public int GetHashCode(BandEntity entity)
            {
                unchecked
                {
                    var hashCode = entity.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (entity.Name?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Genre?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.PhotoURL?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Description?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.ProgramDescription?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (int) (entity.Country);
                    hashCode = (hashCode * 397) ^ (entity.Slots?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        public sealed class BandWithoutSlotsEqualityComparer : IEqualityComparer<BandEntity>
        {
            public bool Equals(BandEntity x, BandEntity y)
            {
                if(ReferenceEquals(x, y)) return true;
                if(ReferenceEquals(x, null)) return false;
                if(ReferenceEquals(y, null)) return false;

                return x.Id.Equals(y.Id) 
                    && string.Equals(x.Name, y.Name)
                    && string.Equals(x.Genre, y.Genre)
                    && string.Equals(x.PhotoURL, y.PhotoURL)
                    && string.Equals(x.Description, y.Description)
                    && string.Equals(x.ProgramDescription, y.ProgramDescription)
                    && x.Country == y.Country;
            }
            
            public int GetHashCode(BandEntity entity)
            {
                unchecked
                {
                    var hashCode = entity.Id.GetHashCode();
                    hashCode = (hashCode * 397) ^ (entity.Name?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Genre?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.PhotoURL?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.Description?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (entity.ProgramDescription?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (int) (entity.Country);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<BandEntity> BandComparer { get; } = new BandEqualityComparer();
        public static IEqualityComparer<BandEntity> BandWithoutSlotsComparer { get; } = new BandWithoutSlotsEqualityComparer();
    }
}
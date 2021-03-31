using System.Collections.Generic;

namespace Festival.DAL.Entites
{
    public class StageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }

        public ICollection<SlotEntity> Slots { get; } = new List<SlotEntity>();
    }
}
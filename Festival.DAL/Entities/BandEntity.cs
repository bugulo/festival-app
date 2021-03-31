using System.Collections.Generic;

using Festival.DAL.Enums;

namespace Festival.DAL.Entites
{
    public class BandEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string PhotoURL { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
        
        public string ProgramDescription { get; set; }

        public ICollection<SlotEntity> Slots { get; } = new List<SlotEntity>();
    }
}
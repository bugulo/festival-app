using System;

namespace Festival.DAL.Entites
{
    public class SlotEntity : BaseEntity
    {
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }

        public Guid BandId { get; set; }
        public BandEntity Band { get; set; }

        public Guid StageId { get; set; }
        public StageEntity Stage { get; set; }
    }
}
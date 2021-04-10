using System;

namespace Festival.BL.Models
{
    public record SlotListModel : ModelBase
    {
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }

        public string BandName { get; set; }
        public string StageName { get; set; }
    }
}

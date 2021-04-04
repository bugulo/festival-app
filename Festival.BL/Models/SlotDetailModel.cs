using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public record SlotDetailModel : ModelBase
    {
        public DateTime StartAt { get; set; }
        public DateTime FinishAt { get; set; }

        public Guid BandId { get; set; }
        public Guid StageId { get; set; }

        public string BandName { get; set; }
        public string BandDescription { get; set; }
        public string BandPhotoURL { get; set; }
        public string StageName { get; set; }
        public string StageDescription { get; set; }
    }
}

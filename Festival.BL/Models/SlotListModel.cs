using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

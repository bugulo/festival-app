using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.BL.Models
{
    public record StageDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }
    }
}

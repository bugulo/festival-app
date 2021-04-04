using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.DAL.Enums;

namespace Festival.BL.Models
{
    public record BandDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string PhotoURL { get; set; }
        public Country Country { get; set; }
        public string Description { get; set; }
    }
}

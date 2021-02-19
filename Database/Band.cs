using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Band
    {
        public string BandName { get; set; } //Unique
        public string Genre { get; set; }
        public string CountryOfOrigin { get; set; }
        public string BandDescription { get; set; }
        public string ProgramDescription { get; set; }
        public string PhotoURL { get; set; } //URL attachment means string
        public Slot Slots { get; set; }
    }
}

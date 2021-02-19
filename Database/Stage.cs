using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Stage
    {
        public string StageName { get; set; } //Unique
        public string NavRoute { get; set; } //Description to help visitors to find the stage
        public string PhotosURL { get; set; } //URL attachment means string
        public List<Band> StageBands { get; set; } = new List<Band>(); //List of Band classes (?)
        public List<Slot> StageSlots { get; set; } = new List<Slot>(); //List of Slot classes (?)
    }
}

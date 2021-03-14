using System;
using System.Collections.Generic;

namespace Database
{
    class Stage
    {
        public Guid Id { get; set; } // Unique identifier

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; } // Photo of stage

        public ICollection<Slot> Slots { get; set; } // Time slots for this stage tied to specific band
    }
}
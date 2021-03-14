using System;
using System.Collections.Generic;

namespace Database
{
    class Band
    {
        public Guid Id { get; set; } // Unique identifier

        public string Name { get; set; } // Original band name (Unique)
        public string Genre { get; set; }
        public string PhotoURL { get; set; } // Photo of band
        public string Country { get; set; } // Country of origin
        public string Description { get; set; }
        
        public string ProgramDescription { get; set; }

        public ICollection<Slot> Slots { get; set; } // Where is band performing
    }
}
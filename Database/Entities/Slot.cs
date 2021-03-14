using System;

namespace Database
{
    class Slot
    {
        public Guid Id { get; set; } // Unique identifier

        public Band Band { get; set; } // Band tied to this slot
        public Stage Stage { get; set; } // Stage tied to this slot

        public DateTime StartAt { get; set; } // When the performance starts
        public DateTime FinishAt { get; set; } // When the performance ends
    }
}
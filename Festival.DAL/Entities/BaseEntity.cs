using System;

using Festival.DAL.Interfaces;

namespace Festival.DAL.Entities
{
    public abstract record BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
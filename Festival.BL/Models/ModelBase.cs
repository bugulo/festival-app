using System;
using Festival.BL.Interfaces;

namespace Festival.BL.Models
{
    public abstract record ModelBase : IModel
    {
        public Guid Id { get; set; }
    }
}
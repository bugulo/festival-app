using System;
using System.Collections.Generic;
using System.Text;
using Festival.DAL.Factories;
using Festival.DAL.Interfaces;

namespace Festival.BL.Factories
{
    public class DefaultEntityFactory : IEntityFactory
    {
        public DefaultEntityFactory()
        {
        }
        
        public TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new() => new();
    }
}
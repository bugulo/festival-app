using System;

using Festival.DAL.Factories;

namespace Festival.DAL.Tests
{
    public class FestivalDbContextClassFixture : IDisposable
    {
        public readonly InMemoryDbContextFactory DbContextFactory = new InMemoryDbContextFactory(nameof(FestivalDbContextClassFixture));

        public FestivalDbContext DbContext { get; }

        public FestivalDbContextClassFixture()
        {
            DbContext = DbContextFactory.Create();
            DbContext.Database.EnsureCreated();
        }

        public void Dispose() 
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
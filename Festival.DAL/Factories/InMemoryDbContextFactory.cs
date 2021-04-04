using Microsoft.EntityFrameworkCore;

namespace Festival.DAL.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        private readonly string _databaseName;

        public InMemoryDbContextFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        public FestivalDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);
            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}
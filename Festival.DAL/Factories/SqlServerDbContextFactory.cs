using Microsoft.EntityFrameworkCore;

using Festival.DAL.Interfaces;

namespace Festival.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory
    {
        private readonly string _connectionString;

        public SqlServerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FestivalDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseSqlServer(_connectionString);
            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}
using Festival.DAL;
using Microsoft.EntityFrameworkCore;

namespace Festival.App.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<FestivalDbContext>
    {
        private readonly string _connectionString;

        public SqlServerDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FestivalDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new FestivalDbContext(optionsBuilder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore.Design;

using Festival.DAL.Interfaces;

namespace Festival.DAL.Factories
{
    public class DesignTimeDbContextFactory : IDbContextFactory, IDesignTimeDbContextFactory<FestivalDbContext>
    {
        public FestivalDbContext Create() 
            => new SqlServerDbContextFactory(@"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = MigrationDb;MultipleActiveResultSets = True;Integrated Security = True; ").Create();
    
        public FestivalDbContext CreateDbContext(string[] args) => Create();
    }
}
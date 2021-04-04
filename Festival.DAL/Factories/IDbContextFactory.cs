namespace Festival.DAL.Factories
{
    public interface IDbContextFactory
    {
        FestivalDbContext Create();
    }
}
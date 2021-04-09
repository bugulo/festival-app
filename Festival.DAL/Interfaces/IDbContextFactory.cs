namespace Festival.DAL.Interfaces
{
    public interface IDbContextFactory
    {
        FestivalDbContext Create();
    }
}
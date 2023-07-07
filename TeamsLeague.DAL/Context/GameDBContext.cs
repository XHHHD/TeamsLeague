using Microsoft.EntityFrameworkCore;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext : DbContext
    {
        public GameDBContext() : base(GetOptions())
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        private static DbContextOptions GetOptions()
        {
            return new DbContextOptionsBuilder().UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=TeamLeagueDB;Integrated Security=True;").Options;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext : DbContext
    {
        public virtual DbSet<User> Users => Set<User>();

        #region TEAM TABLES
        public virtual DbSet<Team> Teams => Set<Team>();
        public virtual DbSet<TeamTrait> TeamTraits => Set<TeamTrait>();
        #endregion

        #region MEMBER TABLES
        public virtual DbSet<Member> Members => Set<Member>();
        public virtual DbSet<MemberTrait> MemberTraits => Set<MemberTrait>();
        public virtual DbSet<Position> Positions => Set<Position>();
        #endregion
    }
}

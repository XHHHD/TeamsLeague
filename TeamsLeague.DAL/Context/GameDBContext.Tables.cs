using Microsoft.EntityFrameworkCore;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MatchParts;
using TeamsLeague.DAL.Entities.MatchParts.KDA;
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

        #region MATCH TABLES
        public virtual DbSet<Match> Matches => Set<Match>();
        public virtual DbSet<MatchSeat> MatchSeats => Set<MatchSeat>();
        public virtual DbSet<MatchEvent> MatchEvents => Set<MatchEvent>();
        public virtual DbSet<MatchEventKill> MatchEventKills => Set<MatchEventKill>();
        public virtual DbSet<MatchEventDeath> MatchEventDeaths => Set<MatchEventDeath>();
        public virtual DbSet<MatchEventAssist> MatchEventAssists => Set<MatchEventAssist>();
        #endregion
    }
}

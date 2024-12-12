using Microsoft.EntityFrameworkCore;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MatchParts;
using TeamsLeague.DAL.Entities.MatchParts.KDA;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasOne(u => u.Team)
                    .WithOne(t => t.User)
                    .HasForeignKey<User>(u => u.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            #region TEAM RELATIONS
            modelBuilder.Entity<Team>(t =>
            {
                t.HasOne(t => t.User)
                    .WithOne(u => u.Team)
                    .HasForeignKey<User>(u => u.TeamId)
                    .OnDelete(DeleteBehavior.NoAction);
                t.HasMany(t => t.Members)
                    .WithOne(m => m.Team)
                    .HasForeignKey(m => m.TeamId)
                    .OnDelete(DeleteBehavior.NoAction);
                t.HasMany(t => t.Traits)
                    .WithOne(tt => tt.Team)
                    .HasForeignKey(tt => tt.TeamId)
                    .OnDelete(DeleteBehavior.Cascade);
                t.HasMany(t => t.History)
                    .WithMany(m => m.Teams);
            });
            modelBuilder.Entity<TeamTrait>(tt =>
            {
                tt.HasOne(tt => tt.Team)
                    .WithMany(t => t.Traits)
                    .HasForeignKey(tt => tt.TeamId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #region MEMBER RELATIONS
            modelBuilder.Entity<Member>(m =>
            {
                m.HasOne(m => m.Team)
                    .WithMany(m => m.Members)
                    .HasForeignKey(m => m.TeamId)
                    .OnDelete(DeleteBehavior.NoAction);
                m.HasMany(m => m.Positions)
                    .WithOne(p => p.Member)
                    .HasForeignKey(p => p.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
                m.HasMany(m => m.Traits)
                    .WithOne(t => t.Member)
                    .HasForeignKey(t => t.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
                m.HasMany(m => m.SeatsHistory)
                    .WithOne(p => p.Member)
                    .HasForeignKey(p => p.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
                m.HasMany(m => m.CharPool)
                    .WithOne(c => c.Member)
                    .HasForeignKey(c => c.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Position>(p =>
            {
                p.HasOne(p => p.Member)
                    .WithMany(m => m.Positions)
                    .HasForeignKey(p => p.MemberId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<MemberTrait>(t =>
            {
                t.HasOne(t => t.Member)
                    .WithMany(m => m.Traits)
                    .HasForeignKey(t => t.MemberId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<GameCharacter>(c =>
            {
                c.HasOne(c => c.Member)
                    .WithMany(m => m.CharPool)
                    .HasForeignKey(c => c.MemberId)
                    .OnDelete(DeleteBehavior.NoAction);
                c.HasMany(c => c.Seats)
                    .WithOne(s => s.GameChar)
                    .HasForeignKey(s => s.GameCharId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #region MATCH
            modelBuilder.Entity<Match>(m =>
            {
                m.HasMany(m => m.Teams)
                    .WithMany(t => t.History);
                m.HasMany(m => m.Seats)
                    .WithOne(p => p.Match)
                    .HasForeignKey(p => p.MatchId)
                    .OnDelete(DeleteBehavior.Cascade);
                m.HasMany(m => m.Events)
                    .WithOne(e => e.Match)
                    .HasForeignKey(e => e.MatchId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<MatchSeat>(s =>
            {
                s.HasOne(s => s.Member)
                    .WithMany(m => m.SeatsHistory)
                    .HasForeignKey(s => s.MemberId)
                    .OnDelete(DeleteBehavior.NoAction);
                s.HasOne(s => s.Match)
                    .WithMany(m => m.Seats)
                    .HasForeignKey(s => s.MatchId)
                    .OnDelete(DeleteBehavior.NoAction);
                s.HasMany(s => s.Kills)
                    .WithOne(k => k.Seat)
                    .HasForeignKey(k => k.SeatId)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(s => s.Dead)
                    .WithOne(d => d.Seat)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(s => s.Assists)
                    .WithOne(a => a.Seat)
                    .HasForeignKey(a => a.SeatId)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasOne(s => s.GameChar)
                    .WithMany(c => c.Seats)
                    .HasForeignKey(c => c.GameCharId)
                    .OnDelete(DeleteBehavior.NoAction);
                s.HasMany(s => s.Destructions)
                    .WithMany(e => e.Destroyers);
            });
            modelBuilder.Entity<MatchEvent>(e =>
            {
                e.HasOne(e => e.Match)
                    .WithMany(m => m.Events)
                    .HasForeignKey(e => e.MatchId)
                    .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Killer)
                    .WithOne(k => k.Event)
                    .HasForeignKey<MatchEventKill>(k => k.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(e => e.Dead)
                    .WithOne(d => d.Event)
                    .HasForeignKey<MatchEventDeath>(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(e => e.Assistants)
                    .WithOne(a => a.Event)
                    .HasForeignKey(a => a.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(e => e.Destroyers)
                    .WithMany(s => s.Destructions);
            });
            modelBuilder.Entity<MatchEventKill>(k =>
            {
                k.HasOne(k => k.Event)
                    .WithOne(e => e.Killer)
                    .HasForeignKey<MatchEventKill>(k => k.EventId)
                    .OnDelete(DeleteBehavior.NoAction);
                k.HasOne(k => k.Seat)
                    .WithMany(s => s.Kills)
                    .HasForeignKey(k => k.SeatId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<MatchEventDeath>(d =>
            {
                d.HasOne(d => d.Event)
                    .WithOne(e => e.Dead)
                    .HasForeignKey<MatchEventDeath>(d => d.EventId)
                    .OnDelete(DeleteBehavior.NoAction);
                d.HasOne(d => d.Seat)
                    .WithMany(s => s.Dead)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<MatchEventAssist>(a =>
            {
                a.HasOne(a => a.Event)
                    .WithMany(e => e.Assistants)
                    .HasForeignKey(a => a.EventId)
                    .OnDelete(DeleteBehavior.NoAction);
                a.HasOne(a => a.Seat)
                    .WithMany(a => a.Assists)
                    .HasForeignKey(a => a.SeatId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion
        }
    }
}

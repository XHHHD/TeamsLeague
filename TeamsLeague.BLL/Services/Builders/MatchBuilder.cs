using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Services.Builders
{
    public class MatchBuilder : IMatchBuilder
    {
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;
        private readonly Random _random;


        private const int rankSearchingDiapason = 100;


        private MatchModel Match { get; set; }


        public MatchBuilder(ITeamService teamService, IMemberService memberService, Random random)
        {
            _teamService = teamService;
            _memberService = memberService;
            _random = random;

            Match = new();
        }


        public IMatchBuilder SetupSoloRank(int memberId)
        {
            Match.Type = TypeOfMatch.SoloRank;

            var member = _memberService.GetMember(memberId);

            var memberMatchPlace = GetMemberPlace(member);

            Match.TeamA.Add(memberMatchPlace);

            BalanceMatch(member.RankPoints);

            return this;
        }

        public IMatchBuilder SetupSoloRank(int memberId, PositionType position)
        {
            Match.Type = TypeOfMatch.SoloRank;

            var member = _memberService.GetMember(memberId);

            var memberMatchPlace = GetMemberPlace(member, position);

            Match.TeamA.Add(memberMatchPlace);

            BalanceMatch(member.RankPoints);

            return this;
        }

        public IMatchBuilder SetupCommonRank(Dictionary<int, PositionType> membersPositions)
        {
            Match.Type = TypeOfMatch.CommonRank;

            var rank = 0;

            foreach (var memberPosition in membersPositions)
            {
                var member = _memberService.GetMember(memberPosition.Key);

                var memberMatchPlace = GetMemberPlace(member, memberPosition.Value);

                Match.TeamA.Add(memberMatchPlace);

                rank += member.RankPoints;
            }

            rank /= membersPositions.Count;

            BalanceMatch(rank);

            return this;
        }

        public IMatchBuilder SetupTeamRank(int teamId)
        {
            Match.Type = TypeOfMatch.TeamRank;

            var teamA = _teamService.GetTeam(teamId);

            foreach (var memberId in teamA.Members.Select(m => m.Id))
            {
                var member = _memberService.GetMember(memberId);

                var memberMatchPlace = GetMemberPlace(member, member.MainPosition);

                Match.TeamA.Add(memberMatchPlace);
            }

            Match.Teams.Add(new TeamShortModel(teamA));

            BalanceTeamMatch(teamA.RankPoints, MatchSide.B);

            return this;
        }

        public IMatchBuilder SetupScrimmages(int teamAId, int teamBId)
        {
            Match.Type = TypeOfMatch.Scrimmages;

            var teamA = _teamService.GetTeam(teamAId);
            var teamB = _teamService.GetTeam(teamBId);

            foreach (var memberId in teamA.Members.Select(m => m.Id))
            {
                var member = _memberService.GetMember(memberId);

                var memberMatchPlace = GetMemberPlace(member, member.MainPosition);

                Match.TeamA.Add(memberMatchPlace);
            }

            foreach (var memberId in teamB.Members.Select(m => m.Id))
            {
                var member = _memberService.GetMember(memberId);

                var memberMatchPlace = GetMemberPlace(member, member.MainPosition, MatchSide.B);

                Match.TeamB.Add(memberMatchPlace);
            }

            Match.Teams.Add(new TeamShortModel(teamA));
            Match.Teams.Add(new TeamShortModel(teamB));

            return this;
        }


        public IMatchBuilder AddResults()
        {
            if (Match.Id == 0)
            {
                SaveMatchPreview();
            }
            //CALCULATE
            //RESULTS
            //HERE
            return this;
        }

        public MatchModel GetMatch()
        {
            if (Match.Id == 0)
            {
                SaveMatchPreview();
            }
            //CHECK
            //ALL
            //HERE
            return Match;
        }


        private MatchSeatModel GetMemberPlace(MemberModel member, MatchSide side = MatchSide.A)
        {
            var result = new MatchSeatModel
            {
                Position = member.MainPosition,
                Side = side,
                Member = member,
                Match = Match,
            };

            return result;
        }

        private MatchSeatModel GetMemberPlace(MemberModel member, PositionType position, MatchSide side = MatchSide.A)
        {
            var result = GetMemberPlace(member, side);

            result.Position = position;

            return result;
        }

        private void BalanceMatch(int rank)
        {
            BalanceTeam(rank, MatchSide.A);
            BalanceTeam(rank, MatchSide.B);
        }

        private void BalanceTeamMatch(int teamRank, MatchSide side)
        {
            if (Match.Teams.Count > 2) throw new Exception("Attempt to add third team in Match!");

            var diapasonFrom = teamRank;
            var diapasonTo = teamRank;
            var teamMembers = side is MatchSide.A
                ? Match.TeamA
                : Match.TeamB;
            while (teamMembers is null)
            {
                var potentialTeams = _teamService.GetTeamsInRank(diapasonFrom, diapasonTo).ToList();

                if (potentialTeams.Any())
                {
                    var teamIndex = _random.Next(potentialTeams.Count - 1);
                    var team = potentialTeams[teamIndex];

                    Match.Teams.Add(new TeamShortModel(team));

                    switch (side)
                    {
                        case MatchSide.A:
                            Match.TeamA = team.Members.Select(m => GetMemberPlace(m, m.MainPosition, side)).ToHashSet();
                            break;
                        case MatchSide.B:
                            Match.TeamB = team.Members.Select(m => GetMemberPlace(m, m.MainPosition, side)).ToHashSet();
                            break;
                    }
                }
            }
        }

        private void BalanceTeam(int rank, MatchSide side)
        {
            var team = side is MatchSide.A
                ? Match.TeamA : Match.TeamB;

            foreach (var type in Enum.GetValues<PositionType>())
            {
                var memberPlace = team.FirstOrDefault(p => p.Position == type);

                var diapasonFrom = rank;
                var diapasonTo = rank;

                while (memberPlace is null)
                {
                    diapasonFrom -= rankSearchingDiapason;
                    diapasonTo += rankSearchingDiapason;

                    var potentialMembers = _memberService
                        .GetMembersInRank(type, diapasonFrom, diapasonTo)
                        .Except(Match.TeamA.Concat(Match.TeamB).Select(p => p.Member))
                        .ToList();

                    if (potentialMembers?.Count > 0)
                    {
                        var memberIndex = _random.Next(potentialMembers.Count - 1);

                        var member = potentialMembers[memberIndex];

                        memberPlace = GetMemberPlace(member, type, side);

                        team.Add(memberPlace);
                    }
                }
            }
        }

        /// <summary>
        /// >>> DO NOT USE AFTER MATCH WAS CALCULATED!!! <<<
        /// </summary>
        private void SaveMatchPreview()
        {
            using var context = new GameDBContext();

            var match = new Match
            {
                Duration = Match.Duration,
                Date = Match.Date,
                Winer = Match.Winer,
                Type = Match.Type,
                IsItEnded = Match.IsItEnded,
                Seats = Match.TeamA.Concat(Match.TeamB).Select(s => new MatchSeat
                {
                    Position = s.Position,
                    Side = s.Side,
                    Member = context.Members.Include(m => m.Team).First(m => m.Id == s.Member.Id),
                }).ToHashSet(),
            };

            foreach (var teamModel in Match.Teams)
            {
                match.Teams.Add(context.Teams.First(t => t.Id == teamModel.Id));
            }

            context.Matches.Add(match);
            context.SaveChanges();

            Match = new MatchModel(match);
        }
    }
}

using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.BLL.Services
{
    public class MatchService : IMatchService
    {
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;
        private readonly Random _random;


        private const int rankSearchingDiapason = 100;


        private MatchModel Match { get; set; }


        public MatchService(ITeamService teamService, IMemberService memberService, Random random)
        {
            _teamService = teamService;
            _memberService = memberService;
            _random = random;

            Match = new();
        }


        public MatchModel GetSoloRank(int memberId)
        {
            var member = _memberService.GetMember(memberId);

            var memberMatchPlace = GetMemberPlace(member);

            Match.TeamA.Add(memberMatchPlace);

            BalanceMatch(member.RankPoints);

            return Match;
        }

        public MatchModel GetSoloRank(int memberId, PositionType position)
        {
            var member = _memberService.GetMember(memberId);

            var memberMatchPlace = GetMemberPlace(member, position);

            Match.TeamA.Add(memberMatchPlace);

            BalanceMatch(member.RankPoints);

            return Match;
        }

        public MatchModel GetCommonRank(Dictionary<int, PositionType> membersPositions)
        {
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

            return Match;
        }

        public MatchModel GetTeamMatch(int teamId)
        {
            var team = _teamService.GetTeam(teamId);

            foreach (var memberId in team.Members.Select(m => m.Id))
            {
                var member = _memberService.GetMember(memberId);

                var memberMatchPlace = GetMemberPlace(member, member.MainPosition);

                Match.TeamA.Add(memberMatchPlace);
            }

            BalanceMatch(team.RankPoints);

            return Match;
        }


        private MatchSeatModel GetMemberPlace(MemberModel member)
        {
            var result = new MatchSeatModel
            {
                Position = member.MainPosition,
                Side = MatchSide.A,
                Member = member,
                Match = Match,
            };

            return result;
        }

        private MatchSeatModel GetMemberPlace(MemberModel member, PositionType position)
        {
            var result = GetMemberPlace(member);

            result.Position = position;

            return result;
        }

        private void BalanceMatch(int rank)
        {
            BalanceTeam(rank, MatchSide.A);
            BalanceTeam(rank, MatchSide.B);
        }

        private void BalanceTeam(int rank, MatchSide side)
        {
            foreach (var type in Enum.GetValues<PositionType>())
            {
                var memberPlace = side is MatchSide.A
                    ? Match.TeamA.FirstOrDefault(p => p.Position == type)
                    : Match.TeamB.FirstOrDefault(p => p.Position == type);

                var diapasonFrom = rank;
                var diapasonTo = rank;

                while (memberPlace is null)
                {
                    diapasonFrom -= rankSearchingDiapason;
                    diapasonTo += rankSearchingDiapason;

                    var potentialMembers = _memberService.GetMembersInRank(type, diapasonFrom, diapasonTo).ToList();

                    if (potentialMembers?.Count > 0)
                    {
                        var memberIndex = _random.Next(potentialMembers.Count - 1);

                        var member = potentialMembers[memberIndex];

                        memberPlace = GetMemberPlace(member, type);
                    }
                }
            }
        }

        private MatchModel SaveMatch(MatchModel matchModel)
        {
            using var dbContext = new GameDBContext();

            var match = new Match
            {

            };

            matchModel = new MatchModel(match);

            return matchModel;
        }
    }
}

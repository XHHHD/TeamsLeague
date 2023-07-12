using TeamsLeague.DAL.Constants.Names;
using TeamsLeague.DAL.Context;

namespace TeamsLeague.BLL.Services.Game
{
    public static class NameGenerator
    {
        public static string GenerateTeamName()
        {
            string result;
            do
            {
                result = GenerateRandomTeamName();
            } while (!result.IsTeamNameIsFree());

            return result;
        }

        public static string GenerateMemberName()
        {
            string memberName;
            do
            {
                memberName = GenerateRandomMemberName();
            } while (!memberName.IsMemberNameIsFree());
            return memberName;
        }

        private static string GenerateRandomTeamName()
        {
            Random random = new();
            var teamName = string.Empty;

            teamName += TeamNames.FirstNames[random.Next(0, TeamNames.FirstNames.Count - 1)];
            teamName += " ";
            teamName += TeamNames.SecondNames[random.Next(0, TeamNames.SecondNames.Count - 1)];

            return teamName;
        }

        private static string GenerateRandomMemberName()
        {
            Random random = new();
            var memberName = MemberNames.Names[random.Next(0, MemberNames.Names.Count - 1)];

            return memberName;
        }

        private static bool IsTeamNameIsFree(this string teamName)
        {
            using GameDBContext dbContext = new();
            var team = dbContext.Teams.SingleOrDefault(t => t.Name == teamName);

            return team is null;
        }

        private static bool IsMemberNameIsFree(this string memberName)
        {
            using GameDBContext dbContext = new();
            var team = dbContext.Members.SingleOrDefault(m => m.Name == memberName);

            return team is null;
        }
    }
}

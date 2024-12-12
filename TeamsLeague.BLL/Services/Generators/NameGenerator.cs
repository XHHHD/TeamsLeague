using TeamsLeague.DAL.Constants.Names;
using TeamsLeague.DAL.Context;

namespace TeamsLeague.BLL.Services.Generators
{
    public static class NameGenerator
    {
        public static string GenerateTeamName()
        {
            string teamName;
            uint randomCounter = 0;
            do
            {
                if (randomCounter > 50)
                {
                    throw new Exception("Extra value of teams!");
                }
                else
                if (randomCounter > 6)
                {
                    Random random = new();

                    teamName = GenerateRandomTeamName();
                    teamName += random.Next(999);
                }
                else
                if (randomCounter > 3)
                {
                    teamName = GenerateMemberName() + " ";
                    teamName += GenerateRandomTeamName();
                }
                else
                {
                    teamName = GenerateRandomTeamName();
                }

                randomCounter++;
            } while (!teamName.IsTeamNameIsFree());

            return teamName;
        }

        public static string GenerateMemberName()
        {
            string memberName;
            uint randomCounter = 0;
            do
            {
                if (randomCounter > 100)
                {
                    throw new Exception("Extra value of members!");
                }
                else
                if (randomCounter > 6)
                {
                    Random random = new();
                    memberName = GenerateRandomMemberName();
                    memberName += random.Next(999);
                }
                else
                if (randomCounter > 3)
                {
                    memberName = GenerateRandomTeamName() + "_";
                    memberName += GenerateRandomMemberName();
                }
                else
                {
                    memberName = GenerateRandomMemberName();
                }

                randomCounter++;
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

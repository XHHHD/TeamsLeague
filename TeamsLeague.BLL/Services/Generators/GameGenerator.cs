using TeamsLeague.BLL.Interfaces;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Services.Generators
{
    public class GameGenerator : IGameGenerator
    {
        private readonly ITeamService _teamService;
        private readonly ITeamBuilder _teamBuilder;

        private const int ExpectedTeamsCount = 100;


        public GameGenerator(ITeamService teamService, ITeamBuilder teamBuilder)
        {
            _teamService = teamService;
            _teamBuilder = teamBuilder;
        }


        public void GenerateEnvironment()
        {
            var currentTeamsCount = _teamService.GetAllTeams().Count();

            while (currentTeamsCount < ExpectedTeamsCount)
            {
                GenerateFullTeam();
                currentTeamsCount++;
            }
        }

        private void GenerateFullTeam()
        {
            var teamName = NameGenerator.GenerateTeamName();

            var team = _teamBuilder
                .GenerateBasicStats(teamName)
                .GenerateMember(PositionType.Top)
                .GenerateMember(PositionType.Jungle)
                .GenerateMember(PositionType.Mid)
                .GenerateMember(PositionType.Bot)
                .GenerateMember(PositionType.Support)
                .Build();

            _teamService.CreateTeam(team);
        }
    }
}

using TeamsLeague.BLL.Interfaces;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Services.Game
{
    public class GameGenerator : IGameGenerator
    {
        private readonly ITeamService _teamService;
        private readonly ITeamBuilder _teamBuilder;

        private const int ExpectedTeamsCount = 10;


        public GameGenerator(ITeamService teamService, ITeamBuilder teamBuilder)
        {
            _teamService = teamService;
            _teamBuilder = teamBuilder;
        }


        public void GenerateEnvironment()
        {
            var currentTeamsCount = _teamService.GetTeams().Count();

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
                .GenerateMember(PositionType.Middle)
                .GenerateMember(PositionType.Bot)
                .GenerateMember(PositionType.Support)
                .Build();

            _teamService.CreateTeam(team);
        }
    }
}

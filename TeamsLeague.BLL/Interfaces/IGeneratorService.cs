namespace TeamsLeague.BLL.Interfaces
{
    public interface IGeneratorService
    {
        void GenerateEnvironment();
        string GenerateTeamName();
        string GenerateMemberName();
    }
}

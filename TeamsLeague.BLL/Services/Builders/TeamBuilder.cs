using System.Linq;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Services.Builders
{
    public class TeamBuilder : ITeamBuilder
    {
        private readonly ITeamService _teamService;
        private readonly IMemberBuilder _memberBuilder;
        private readonly Random _random;

        private const string defaultTeamImage = "/Resources/Img/Default/icons8-ос-free-bsd-100-white.png";
        private const double defaultExperience = 0;
        private const int defaultRankPoints = 0;
        private const double defaultHonor = 0;
        private const double defaultEnergy = 0;
        private const double defaultMaxEnergy = 0;
        private const double defaultHealth = 0;
        private const double defaultMaxHealth = 0;
        private const double defaultTeamplay = 0;

        private TeamModel TeamModel { get; set; }


        public TeamBuilder(ITeamService teamService, IMemberBuilder memberBuilder)
        {
            _teamService = teamService;
            _memberBuilder = memberBuilder;
            _random = new();
            TeamModel = new();
        }


        public ITeamBuilder GenerateBasicStats()
        {
            TeamModel = new TeamModel
            {
                Name = "",
                Image = defaultTeamImage,

                Experience = defaultExperience,
                RankPoints = defaultRankPoints,
                Honor = defaultHonor,

                Energy = defaultEnergy,
                MaxEnergy = defaultMaxEnergy,
                Health = defaultHealth,
                MaxHealth = defaultMaxHealth,
                Teamplay = defaultTeamplay,

                Members = new HashSet<MemberModel>(),
                Traits = new HashSet<TeamTraitModel>(),
            };

            TeamModel = _teamService.CreateTeam(TeamModel);


            return this;
        }

        public ITeamBuilder GenerateBasicStats(string teamName)
        {
            TeamModel = new TeamModel
            {
                Name = teamName,
                Image = defaultTeamImage,

                Experience = defaultExperience,
                RankPoints = defaultRankPoints,
                Honor = defaultHonor,

                Energy = defaultEnergy,
                MaxEnergy = defaultMaxEnergy,
                Health = defaultHealth,
                MaxHealth = defaultMaxHealth,
                Teamplay = defaultTeamplay,

                Members = new HashSet<MemberModel>(),
                Traits = new HashSet<TeamTraitModel>(),
            };

            TeamModel = _teamService.CreateTeam(TeamModel);


            return this;
        }

        /// <summary>
        /// Add random member to the team.
        /// </summary>
        public ITeamBuilder GenerateMember()
        {
            TeamModel.Members ??= new HashSet<MemberModel>();

            var member = _memberBuilder
                .GenerateBasicStats()
                .AddPosition()
                .Build();

            TeamModel.Members.Add(member);

            return this;
        }

        /// <summary>
        /// Add member with specific position to the team.
        /// </summary>
        /// <param name="type">Desired position type of member.</param>
        public ITeamBuilder GenerateMember(PositionType type)
        {
            TeamModel.Members ??= new HashSet<MemberModel>();

            var member = _memberBuilder
                .GenerateBasicStats()
                .AddPosition(type)
                .Build();

            TeamModel.Members.Add(member);

            return this;
        }

        /// <summary>
        /// Add to the team trait with random type, except trait types team already have, or do nothing.
        /// </summary>
        public ITeamBuilder AddTrait()
        {
            TeamModel.Traits ??= new HashSet<TeamTraitModel>();

            var existTypes = Enum.GetValues<TeamTraitType>().ToList();
            var potentialTypes = existTypes;

            foreach (var type in existTypes)
            {
                if (TeamModel.Traits.Select(p => p.Type).Contains(type))
                {
                    potentialTypes.Remove(type);
                }
            }

            if (potentialTypes.Count == 0)
            {
                return this;
            }

            TeamModel.Traits.Add(new TeamTraitModel
            {
                Type = potentialTypes[_random.Next(0, potentialTypes.Count - 1)],
            });

            return this;
        }

        public ITeamBuilder AddTrait(TeamTraitType type)
        {
            TeamModel.Traits ??= new HashSet<TeamTraitModel>();

            if (TeamModel.Traits.Select(p => p.Type).Contains(type))
            {
                throw new Exception($"Can't add trait with type: {type} more then ones!");
            }

            TeamModel.Traits.Add(new TeamTraitModel
            {
                Type = type,
            });

            return this;
        }

        public TeamModel Build()
        {
            if (TeamModel is null)
            {
                throw new ArgumentNullException(nameof(TeamModel)
                    + "was null!");
            }
            else
            if (TeamModel.Traits is null)
            {
                throw new ArgumentNullException(nameof(TeamModel.Traits) + "was null! Unacceptable team model!");
            }

            _teamService.UpdateTeam(TeamModel);


            return TeamModel;
        }
    }
}

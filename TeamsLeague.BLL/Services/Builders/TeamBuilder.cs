using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Services.Builders
{
    public class TeamBuilder : ITeamBuilder
    {
        private readonly IMemberBuilder _memberBuilder;
        private readonly Random _random;

        private const string defaultTeamImage = "/Resources/Img/Default/icons8-ос-free-bsd-100-white.png";
        private const double defaultExperience = 0;
        private const int defaultRankPoints = 0;
        private const double defaultHonor = 0;
        private const double defaultEnergy = 0;
        private const double defaultMaxEnergy = 100;
        private const double defaultHealth = 100;
        private const double defaultMaxHealth = 100;
        private const double defaultTeamplay = 0;

        private TeamModel TeamModel { get; set; }


        public TeamBuilder(IMemberBuilder memberBuilder, Random random)
        {
            _memberBuilder = memberBuilder;
            _random = random;
            TeamModel = new();
        }

        public ITeamBuilder GenerateBasicStats(string teamName)
        {
            string teamImage;
            try
            {
                teamImage = ImagesService.GetTeamImgUrl(_random.Next(38, 99));
            }
            catch
            {
                teamImage = defaultTeamImage;
            }

            TeamModel = new TeamModel
            {
                Name = teamName,
                Image = teamImage,
                LastChanges = DateTime.UtcNow,

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


            return this;
        }

        /// <summary>
        /// Add random member to the team.
        /// </summary>
        public ITeamBuilder GenerateMember()
        {
            TeamModel.Members ??= new HashSet<MemberModel>();

            var memberName = NameGenerator.GenerateMemberName();

            var member = _memberBuilder
                .GenerateBasicStats(memberName)
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

            var memberName = NameGenerator.GenerateMemberName();

            var member = _memberBuilder
                .GenerateBasicStats(memberName)
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

        /// <summary>
        /// Add trait with specific type to the team.
        /// </summary>
        /// <param name="type">Desired trait type.</param>
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


            return TeamModel;
        }
    }
}

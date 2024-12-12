using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Services.Builders
{
    public class TeamBuilder(IMemberBuilder memberBuilder, Random random) : ITeamBuilder
    {
        private const string DEFAULT_TEAMIMAGE = "/Resources/Img/Default/icons8-ос-free-bsd-100-white.png";
        private const double DEFAULT_EXPERIENCE = 0;
        private const int DEFAULT_RANKPOINTS = 0;
        private const double DEFAULT_HONOR = 0;
        private const double DEFAULT_ENERGY = 0;
        private const double DEFAULT_ENERGYMAX = 100;
        private const double DEFAULT_ENERGYREGEN = 0.5;
        private const double DEFAULT_HEALTH = 100;
        private const double DEFAULT_HEALTHMAX = 100;
        private const double DEFAULT_HEALTHREGEN = 0.5;
        private const double DEFAULT_TEAMPLAY = 0;
        private const int IMAGE_NUMBER_RANDOMING_MIN = 38;
        private const int IMAGE_NUMBER_RANDOMING_MAX = 99;

        private readonly IMemberBuilder _memberBuilder = memberBuilder;
        private readonly Random _random = random;

        private TeamModel TeamModel { get; set; } = new();

        public ITeamBuilder GenerateBasicStats(string teamName)
        {
            var teamImage = GetImagePath();
            TeamModel = new TeamModel
            {
                Name = teamName,
                Image = teamImage,
                LastChanges = DateTime.UtcNow,

                Experience = DEFAULT_EXPERIENCE,
                RankPoints = DEFAULT_RANKPOINTS,
                Honor = DEFAULT_HONOR,

                Energy = DEFAULT_ENERGY,
                MaxEnergy = DEFAULT_ENERGYMAX,
                EnergyRegen = DEFAULT_ENERGYREGEN,
                Health = DEFAULT_HEALTH,
                MaxHealth = DEFAULT_HEALTHMAX,
                HealthRegen = DEFAULT_HEALTHREGEN,
                Teamplay = DEFAULT_TEAMPLAY,

                Members = [],
                Traits = [],
            };

            return this;
        }

        /// <summary>
        /// Add random member to the team.
        /// </summary>
        public ITeamBuilder GenerateMember()
        {
            TeamModel.Members ??= [];
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
            TeamModel.Members ??= [];
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
            TeamModel.Traits ??= [];
            var potentialTypes = GetFreeTeamTraitTypes();
            if (!potentialTypes.Any())
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
            TeamModel.Traits ??= [];

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

        private IList<TeamTraitType> GetFreeTeamTraitTypes()
        {
            var existTypes = Enum.GetValues<TeamTraitType>().ToList();
            var potentialTypes = existTypes;
            existTypes.ForEach(t =>
            {
                if (TeamModel.Traits.Select(p => p.Type).Contains(t))
                {
                    potentialTypes.Remove(t);
                }
            });

            return potentialTypes;
        }

        private string GetImagePath()
        {
            try
            {
                return ImagesService.GetTeamImgUrl(_random.Next(IMAGE_NUMBER_RANDOMING_MIN, IMAGE_NUMBER_RANDOMING_MAX));
            }
            catch
            {
                return DEFAULT_TEAMIMAGE;
            }
        }
    }
}

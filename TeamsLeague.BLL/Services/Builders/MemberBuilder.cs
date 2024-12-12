using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Services.Builders
{
    public class MemberBuilder(Random random) : IMemberBuilder
    {
        private const int DEFAULT_AGE = 14;
        private const double DEFAULT_ATTACK = 10;
        private const double DEFAULT_DEFENCE = 10;
        private const double DEFAULT_INTELLIGENCE = 20;
        private const double DEFAULT_REACTIONSPEED = 20;
        private const double DEFAULT_MOBILITY = 0;
        private const double DEFAULT_MENTHALPOWER = 10;
        private const double DEFAULT_MENTHALRESISTANCE = 10;
        private const double DEFAULT_MENTHALHEALTH = 100;
        private const double DEFAULT_MENTHALHEALTHMAX = 100;
        private const double DEFAULT_MENTHALHEALTHREGEN = 1;
        private const double DEFAULT_EXPERIENCE = 0;
        private const double DEFAULT_ENERGY = 100;
        private const double DEFAULT_ENERGYMAX = 100;
        private const double DEFAULT_ENERGYREGEN = 1;
        private const double DEFAULT_TEAMPLAY = 0;
        private const double DEFAULT_TEAMPLAYMIN = 0;
        private const double DEFAULT_TEAMPLAYMAX = 100;
        private const byte DEFAULT_SKILLPOINTS = 0;
        private const int DEFAULT_RANKPOINTS = 0;

        private readonly Random _random = random;

        private MemberModel MemberModel { get; set; } = new();

        public IMemberBuilder GenerateBasicStats()
        {
            var name = NameGenerator.GenerateMemberName();
            GenerateBasicStats(name);

            return this;
        }

        public IMemberBuilder GenerateBasicStats(string memberName)
        {
            MemberModel = new MemberModel
            {
                Name = memberName,
                Age = DEFAULT_AGE + _random.Next(0, 6),
                Attack = DEFAULT_ATTACK + _random.Next(-6, 6),
                Defense = DEFAULT_DEFENCE + _random.Next(-6, 6),
                Intelligence = DEFAULT_INTELLIGENCE + _random.Next(-16, 16),
                ReactionSpeed = DEFAULT_REACTIONSPEED + _random.Next(-16, 16),
                Mobility = DEFAULT_MOBILITY + _random.Next(0, 12),
                MentalPower = DEFAULT_MENTHALPOWER + _random.Next(-8, 8),
                MentalResistance = DEFAULT_MENTHALRESISTANCE + _random.Next(-10, 10),
                CreationDate = DateTime.Now,
                LastChanges = DateTime.Now,
                MainPosition = Enum.GetValues<PositionType>().ToList()[_random.Next(0, Enum.GetValues<PositionType>().ToList().Count - 1)],
                PlayStyle = Enum.GetValues<PlaystyleType>().ToList()[_random.Next(0, Enum.GetValues<PlaystyleType>().ToList().Count - 1)],

                Experience = DEFAULT_EXPERIENCE,
                SkillPoints = DEFAULT_SKILLPOINTS,
                RankPoints = DEFAULT_RANKPOINTS,
                Teamplay = DEFAULT_TEAMPLAY,
                MinTeamplay = DEFAULT_TEAMPLAYMIN,
                MaxTeamplay = DEFAULT_TEAMPLAYMAX,

                Energy = DEFAULT_ENERGY,
                MaxEnergy = DEFAULT_ENERGYMAX + _random.Next(-6, 6),
                EnergyRegen = DEFAULT_ENERGYREGEN + _random.NextDouble(),
                MentalHealth = DEFAULT_MENTHALHEALTH + _random.Next(-16, 16),
                MaxMentalHealth = DEFAULT_MENTHALHEALTHMAX + _random.Next(-16, 16),
                MentalHealthRegen = DEFAULT_MENTHALHEALTHREGEN + _random.NextDouble(),

                Positions = [],
                Traits = [],
            };


            return this;
        }

        public IMemberBuilder ChoosePlaystyle(PlaystyleType playstyle)
        {
            MemberModel.PlayStyle = playstyle;

            return this;
        }

        /// <summary>
        /// Add to the member position with random type, except position types he already have, or do nothing.
        /// </summary>
        public IMemberBuilder AddPosition()
        {
            MemberModel.Positions ??= [];
            var potentialTypes = GetFreePositionTypes();
            if (!potentialTypes.Any())
            {
                return this;
            }

            var position = new PositionModel
            {
                Type = potentialTypes[_random.Next(0, potentialTypes.Count - 1)],
            };

            if (MemberModel.Positions.Count == 0)
            {
                MemberModel.MainPosition = position.Type;
            }

            MemberModel.Positions.Add(position);

            return this;
        }

        /// <summary>
        /// Add to the member position with specific type, except position types he already have.
        /// </summary>
        public IMemberBuilder AddPosition(PositionType type)
        {
            MemberModel.Positions ??= [];

            if (MemberModel.Positions.Select(p => p.Type).Contains(type))
            {
                throw new Exception($"Can't add position with type: {type} more then ones!");
            }

            if (MemberModel.Positions.Count == 0)
            {
                MemberModel.MainPosition = type;
            }

            MemberModel.Positions.Add(new PositionModel
            {
                Type = type,
            });

            return this;
        }

        /// <summary>
        /// Add to the member trait with random type, except trait types member already have, or do nothing.
        /// </summary>
        public IMemberBuilder AddTrait()
        {
            MemberModel.Traits ??= [];
            var potentialTypes = GetFreeMemberTraitTypes();
            if (!potentialTypes.Any())
            {
                return this;
            }

            MemberModel.Traits.Add(new MemberTraitModel
            {
                Type = potentialTypes[_random.Next(0, potentialTypes.Count - 1)],
            });

            return this;
        }

        /// <summary>
        /// Add to the member trait with specific type, except trait types member already have.
        /// </summary>
        public IMemberBuilder AddTrait(MemberTraitType type)
        {
            MemberModel.Traits ??= [];

            if (MemberModel.Traits.Select(p => p.Type).Contains(type))
            {
                throw new Exception($"Can't add trait with type: {type} more then ones!");
            }

            MemberModel.Traits.Add(new MemberTraitModel
            {
                Type = type,
            });

            return this;
        }

        public MemberModel Build()
        {
            if (MemberModel is null)
            {
                throw new ArgumentNullException(nameof(MemberModel)
                    + "was null!");
            }
            else
            if (MemberModel.Positions is null || MemberModel.Positions.Count == 0)
            {
                throw new ArgumentNullException(nameof(MemberModel.Positions)
                    + "was null or empty! Member should have at list one position in collection.");
            }
            else
            if (MemberModel.Traits is null)
            {
                throw new ArgumentNullException(nameof(MemberModel.Traits) + "was null! Unacceptable member model!");
            }

            return MemberModel;
        }

        private IList<PositionType> GetFreePositionTypes()
        {
            var existTypes = Enum.GetValues<PositionType>().ToList();
            var potentialTypes = existTypes;
            existTypes.ForEach(t =>
            {
                if (MemberModel.Positions.Select(p => p.Type).Contains(t))
                {
                    potentialTypes.Remove(t);
                }
            });

            return potentialTypes;
        }

        private IList<MemberTraitType> GetFreeMemberTraitTypes()
        {
            var existTypes = Enum.GetValues<MemberTraitType>().ToList();
            var potentialTypes = existTypes;
            existTypes.ForEach(t =>
            {
                if (MemberModel.Traits.Select(p => p.Type).Contains(t))
                {
                    potentialTypes.Remove(t);
                }
            });

            return potentialTypes;
        }
    }
}

using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.BLL.Services.Builders
{
    public class MemberBuilder : IMemberBuilder
    {
        private readonly Random _random;

        private const int defaultAge = 14;
        private const double defaultAttack = 10;
        private const double defaultDefense = 10;
        private const double defaultIntelligence = 20;
        private const double defaultReactionSpeed = 20;
        private const double defaultMentalPower = 10;
        private const double defaultMentalResistance = 10;
        private const double defaultExperience = 0;
        private const byte defaultSkillPoints = 0;
        private const int defaultRankPoints = 0;
        private const double defaultEnergy = 100;
        private const double defaultMaxEnergy = 100;
        private const double defaultEnergyRegen = 1;
        private const double defaultMentalHealth = 100;
        private const double defaultMaxMentalHealth = 100;
        private const double defaultMentalHealthRegen = 1;
        private const double defaultTeamplay = 0;
        private const double defaultMinTeamplay = 0;
        private const double defaultMaxTeamplay = 100;

        private MemberModel MemberModel { get; set; }


        public MemberBuilder(Random random)
        {
            _random = random;
            MemberModel = new();
        }


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
                Age = defaultAge + _random.Next(0, 6),
                Attack = defaultAttack + _random.Next(-6, 6),
                Defense = defaultDefense + _random.Next(-6, 6),
                Intelligence = defaultIntelligence + _random.Next(-16, 16),
                ReactionSpeed = defaultReactionSpeed + _random.Next(-16, 16),
                MentalPower = defaultMentalPower + _random.Next(-8, 8),
                MentalResistance = defaultMentalResistance + _random.Next(-10, 10),
                CreationDate = DateTime.Now,
                LastChanges = DateTime.Now,
                MainPosition = Enum.GetValues<PositionType>().ToList()[_random.Next(0, Enum.GetValues<PositionType>().ToList().Count - 1)],
                PlayStyle = Enum.GetValues<PlaystyleType>().ToList()[_random.Next(0, Enum.GetValues<PlaystyleType>().ToList().Count - 1)],

                Experience = defaultExperience,
                SkillPoints = defaultSkillPoints,
                RankPoints = defaultRankPoints,
                Teamplay = defaultTeamplay,
                MinTeamplay = defaultMinTeamplay,
                MaxTeamplay = defaultMaxTeamplay,

                Energy = defaultEnergy,
                MaxEnergy = defaultMaxEnergy + _random.Next(-6, 6),
                EnergyRegen = defaultEnergyRegen + _random.NextDouble(),
                MentalHealth = defaultMentalHealth + _random.Next(-16, 16),
                MaxMentalHealth = defaultMaxMentalHealth + _random.Next(-16, 16),
                MentalHealthRegen = defaultMentalHealthRegen + _random.NextDouble(),

                Positions = new HashSet<PositionModel>(),
                Traits = new HashSet<MemberTraitModel>(),
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
            MemberModel.Positions ??= new HashSet<PositionModel>();

            #region GET TYPE OF POSITION, WHICH WASN'T ADDED BEFORE
            var existTypes = Enum.GetValues<PositionType>().ToList();
            var potentialTypes = existTypes;

            foreach (var type in existTypes)
            {
                if (MemberModel.Positions.Select(p => p.Type).Contains(type))
                {
                    potentialTypes.Remove(type);
                }
            }

            if (potentialTypes.Count == 0)
            {
                return this;
            }
            #endregion

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
            MemberModel.Positions ??= new HashSet<PositionModel>();

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
            MemberModel.Traits ??= new HashSet<MemberTraitModel>();

            var existTypes = Enum.GetValues<MemberTraitType>().ToList();
            var potentialTypes = existTypes;

            foreach (var type in existTypes)
            {
                if (MemberModel.Traits.Select(p => p.Type).Contains(type))
                {
                    potentialTypes.Remove(type);
                }
            }

            if (potentialTypes.Count == 0)
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
            MemberModel.Traits ??= new HashSet<MemberTraitModel>();

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
    }
}

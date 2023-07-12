using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Services.Builders
{
    public class MemberBuilder : IMemberBuilder
    {
        private readonly IMemberService _memberService;
        private readonly GameDBContext _context;
        private readonly Random _random;

        private const int defaultAge = 14;
        private const double defaultAttack = 10;
        private const double defaultDefense = 10;
        private const double defaultExperience = 0;
        private const byte defaultSkillPoints = 0;
        private const int defaultRankPoints = 0;
        private const double defaultEnergy = 100;
        private const double defaultMaxEnergy = 100;
        private const double defaultMentalPower = 100;
        private const double defaultMaxMentalPower = 100;
        private const double defaultMentalHealth = 100;
        private const double defaultMaxMentalHealth = 100;

        private MemberModel MemberModel { get; set; }


        public MemberBuilder(IMemberService memberService)
        {
            _memberService = memberService;
            _context = new();
            _random = new();
            MemberModel = new();
        }


        public IMemberBuilder GenerateBasicStats()
        {
            MemberModel = new MemberModel
            {
                Name = "",
                Age = defaultAge,
                Attack = defaultAttack,
                Defense = defaultDefense,
                CreationDate = DateTime.Now,
                LastChanges = DateTime.Now,

                Experience = defaultExperience,
                SkillPoints = defaultSkillPoints,
                RankPoints = defaultRankPoints,

                Energy = defaultEnergy,
                MaxEnergy = defaultMaxEnergy,
                MentalPower = defaultMentalPower,
                MaxMentalPower = defaultMaxMentalPower,
                MentalHealth = defaultMentalHealth,
                MaxMentalHealth = defaultMaxMentalHealth,

                Positions = new HashSet<PositionModel>(),
                Traits = new HashSet<MemberTraitModel>(),
            };

            MemberModel = _memberService.CreateMember(MemberModel);

            return this;
        }

        public IMemberBuilder AddPosition()
        {
            MemberModel.Positions ??= new HashSet<PositionModel>();

            MemberModel.Positions.Add(new PositionModel
            {
                //ADD HERE GENERATED
                //POSITION PARAMETERS
            });

            return this;
        }

        public IMemberBuilder AddPosition(PositionType type)
        {
            MemberModel.Positions ??= new HashSet<PositionModel>();

            MemberModel.Positions.Add(new PositionModel
            {
                //ADD HERE GENERATED
                //POSITION PARAMETERS
            });

            return this;
        }

        public IMemberBuilder AddTrait()
        {
            MemberModel.Traits ??= new HashSet<MemberTraitModel>();

            MemberModel.Traits.Add(new MemberTraitModel
            {
                //ADD HERE GENERATED
                //TRAIT PARAMETERS
            });

            return this;
        }

        public IMemberBuilder AddTrait(MemberTraitType type)
        {
            MemberModel.Traits ??= new HashSet<MemberTraitModel>();

            MemberModel.Traits.Add(new MemberTraitModel
            {
                //ADD HERE GENERATED
                //TRAIT PARAMETERS
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

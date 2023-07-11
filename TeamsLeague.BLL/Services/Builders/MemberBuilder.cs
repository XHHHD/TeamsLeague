using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.BLL.Services.Builders
{
    public class MemberBuilder : IMemberBuilder
    {
        private readonly IMemberServices _memberServices;

        private MemberModel MemberModel { get; set; }


        public MemberBuilder(IMemberServices memberServices)
        {
            _memberServices = memberServices;
            MemberModel = new();
        }


        public IMemberBuilder GenerateBasicStats()
        {
            MemberModel = new MemberModel
            {
                Name = "",
                Age = 0,
                MovePoints = 0,
                Experience = 0,
                MentalHealth = 0,
                Positions = new HashSet<PositionModel>(),
                MemberTraits = new HashSet<MemberTrait>(),
            };

            //MemberModel = _memberServices.CreateMember(MemberModel);

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
            MemberModel.MemberTraits ??= new HashSet<MemberTrait>();

            MemberModel.MemberTraits.Add(new MemberTrait
            {
                //ADD HERE GENERATED
                //POSITION PARAMETERS
            });

            return this;
        }

        public IMemberBuilder AddTrait(MemberTraitType type)
        {
            MemberModel.MemberTraits ??= new HashSet<MemberTrait>();

            MemberModel.MemberTraits.Add(new MemberTrait
            {
                //ADD HERE GENERATED
                //POSITION PARAMETERS
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
            if (MemberModel.MemberTraits is null)
            {
                throw new ArgumentNullException(nameof(MemberModel.MemberTraits) + "was null! Unacceptable member model!");
            }
            return MemberModel;
        }
    }
}

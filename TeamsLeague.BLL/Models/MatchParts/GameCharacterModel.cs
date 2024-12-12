using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.DAL.Constants.Match.GamePerson;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Models.MatchParts
{
    public class GameCharacterModel
    {
        public int Id { get; set; }
        public double Experience { get; set; }
        public GameCharType Type { get; set; }
        public MemberModel Member { get; set; }


        public GameCharacterModel() { }

        public GameCharacterModel(GameCharacter character)
        {
            Id = character.Id;
            Experience = character.Experience;
            Type = character.Type;
            Member = new MemberModel
            {
                Id = character.Member.Id,
                Name = character.Member.Name,
            };
        }
    }
}

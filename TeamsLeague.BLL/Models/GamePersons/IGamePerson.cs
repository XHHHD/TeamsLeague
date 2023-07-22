using TeamsLeague.BLL.Models.GamePersons;
using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.DAL.Constants.GamePersons
{
    public interface IGamePerson
    {
        GamePersonType Type { get; }
        HashSet<GamePersonAbility> Abilities { get; }
        string Name { get; }
        string Description { get; }
        int BasicAttackDamage { get; set; }
        double BasicAttackAnimationTime { get; set; }
        int DamageResistance { get; set; }
        int Health { get; set; }
        int HealthRegeneration { get; set; }
        int Mana { get; set; }
        int ManaRegeneration { get; set; }
    }
}

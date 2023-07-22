using TeamsLeague.BLL.Models.GamePersons;
using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.DAL.Constants.GamePersons
{
    public class GamePerson : IGamePerson
    {
        public GamePersonType Type { get; }
        public HashSet<GamePersonAbility> Abilities { get; }

        public string Name { get; }
        public string Description { get; }

        public int BasicAttackDamage { get; set; }
        public double BasicAttackAnimationTime { get; set; }
        public int DamageResistance { get; set; }
        public int Health { get; set; }
        public int HealthRegeneration { get; set; }
        public int Mana { get; set; }
        public int ManaRegeneration { get; set; }


        public GamePerson(GamePersonType type)
        {
            Type = type;
            Name = Type.ToString();
            Description = PersonDescriptions.GetDescription(type);

            switch (type)
            {
                case GamePersonType.Aurelia:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.GlowOfWings),
                        new GamePersonAbility(GamePersonAbilitiesType.ButterflySplash),
                        new GamePersonAbility(GamePersonAbilitiesType.EtherealWandering),
                        new GamePersonAbility(GamePersonAbilitiesType.WingDust),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
                case GamePersonType.Ragnus:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.FireCall),
                        new GamePersonAbility(GamePersonAbilitiesType.Arson),
                        new GamePersonAbility(GamePersonAbilitiesType.TanningTactics),
                        new GamePersonAbility(GamePersonAbilitiesType.FireShield),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
                case GamePersonType.Ivy:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.NatureWeaving),
                        new GamePersonAbility(GamePersonAbilitiesType.ConfusingForest),
                        new GamePersonAbility(GamePersonAbilitiesType.HealingBalm),
                        new GamePersonAbility(GamePersonAbilitiesType.EmergenceOfLife),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
                case GamePersonType.Cogsworth:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.CannonCannon),
                        new GamePersonAbility(GamePersonAbilitiesType.ProtectiveEnergyShield),
                        new GamePersonAbility(GamePersonAbilitiesType.MechanicalKaizen),
                        new GamePersonAbility(GamePersonAbilitiesType.ReprogrammingMagic),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
                case GamePersonType.Elara:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.IceShot),
                        new GamePersonAbility(GamePersonAbilitiesType.IceFreeze),
                        new GamePersonAbility(GamePersonAbilitiesType.NaturalResilience),
                        new GamePersonAbility(GamePersonAbilitiesType.ButterflyDance),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
                case GamePersonType.Thundar:
                    Abilities = new HashSet<GamePersonAbility>
                    {
                        new GamePersonAbility(GamePersonAbilitiesType.ThunderStrike),
                        new GamePersonAbility(GamePersonAbilitiesType.Electrolayer),
                        new GamePersonAbility(GamePersonAbilitiesType.ElectricShield),
                        new GamePersonAbility(GamePersonAbilitiesType.ElectrostaticDischarge),
                    };
                    BasicAttackDamage = 10;
                    BasicAttackAnimationTime = 10;
                    DamageResistance = 10;
                    Health = 10;
                    HealthRegeneration = 10;
                    Mana = 10;
                    ManaRegeneration = 10;
                    break;
            }
        }
    }
}

using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.BLL.Models.GamePersons
{
    public class GamePersonAbility
    {
        public GamePersonAbilitiesType Type { get; }
        public string Name { get; }
        public string Description { get; }

        public int ManaCost { get; }
        //Healing
        public double AllyHealing { get; }
        public double SelfHealing { get; }
        //Damage
        public double DamageInUse { get; }
        public double DamageInTime { get; }
        public double DamageDuration { get; }
        //Slow And Stun
        public double SlowPower { get; }
        public double SlowDuration { get; }
        public double StunDuration { get; }
        //Speed
        public double AllySpeedAcceleration { get; }
        public double AllySpeedAccelerationDuration { get; }
        public double SelfSpeedAcceleration { get; }
        public double SelfSpeedAccelerationDuration { get; }
        //Power
        public double AllyPowerAcceleration { get; }
        public double AllyPowerAccelerationDuration { get; }
        public double SelfPowerAcceleration { get; }
        public double SelfPowerAccelerationDuration { get; }
        //Defense
        public double AllyDefenseAcceleration { get; }
        public double AllyDefenseAccelerationDuration { get; }
        public double SelfDefenseAcceleration { get; }
        public double SelfDefenseAccelerationDuration { get; }


        public GamePersonAbility(GamePersonAbilitiesType type)
        {
            Type = type;
            Name = type.ToString();
            Description = AbilitiesDescriptions.GetDescription(type);

            switch (type)
            {
                case GamePersonAbilitiesType.GlowOfWings:
                    ManaCost = 0;

                    AllyHealing = 0;
                    SelfHealing = 0;

                    DamageInUse = 0;
                    DamageInTime = 0;
                    DamageDuration = 0;

                    SlowPower = 0;
                    SlowDuration = 0;
                    StunDuration = 0;

                    AllySpeedAcceleration = 0;
                    AllySpeedAccelerationDuration = 0;
                    SelfSpeedAcceleration = 0;
                    SelfSpeedAccelerationDuration = 0;

                    AllyPowerAcceleration = 0;
                    AllyPowerAccelerationDuration = 0;
                    SelfPowerAcceleration = 0;
                    SelfPowerAccelerationDuration = 0;

                    AllyDefenseAcceleration = 0;
                    AllyDefenseAccelerationDuration = 0;
                    SelfDefenseAcceleration = 0;
                    SelfDefenseAccelerationDuration = 0;
                    break;
                case GamePersonAbilitiesType.ButterflySplash:
                    break;
                case GamePersonAbilitiesType.EtherealWandering:
                    break;
                case GamePersonAbilitiesType.WingDust:
                    break;
                case GamePersonAbilitiesType.FireCall:
                    break;
                case GamePersonAbilitiesType.Arson:
                    break;
                case GamePersonAbilitiesType.TanningTactics:
                    break;
                case GamePersonAbilitiesType.FireShield:
                    break;
                case GamePersonAbilitiesType.NatureWeaving:
                    break;
                case GamePersonAbilitiesType.ConfusingForest:
                    break;
                case GamePersonAbilitiesType.HealingBalm:
                    break;
                case GamePersonAbilitiesType.EmergenceOfLife:
                    break;
                case GamePersonAbilitiesType.CannonCannon:
                    break;
                case GamePersonAbilitiesType.ProtectiveEnergyShield:
                    break;
                case GamePersonAbilitiesType.MechanicalKaizen:
                    break;
                case GamePersonAbilitiesType.ReprogrammingMagic:
                    break;
                case GamePersonAbilitiesType.IceShot:
                    break;
                case GamePersonAbilitiesType.IceFreeze:
                    break;
                case GamePersonAbilitiesType.NaturalResilience:
                    break;
                case GamePersonAbilitiesType.ButterflyDance:
                    break;
                case GamePersonAbilitiesType.ThunderStrike:
                    break;
                case GamePersonAbilitiesType.Electrolayer:
                    break;
                case GamePersonAbilitiesType.ElectricShield:
                    break;
                case GamePersonAbilitiesType.ElectrostaticDischarge:
                    break;
                case GamePersonAbilitiesType.StoneFist:
                    break;
                case GamePersonAbilitiesType.Earthquake:
                    break;
                case GamePersonAbilitiesType.RunicBarrier:
                    break;
                case GamePersonAbilitiesType.StatueBaby:
                    break;
                case GamePersonAbilitiesType.RealityDimension:
                    break;
                case GamePersonAbilitiesType.Abracadabra:
                    break;
                case GamePersonAbilitiesType.CupidsArrow:
                    break;
                case GamePersonAbilitiesType.UmbrellaOfSurprises:
                    break;
                case GamePersonAbilitiesType.DeadlyClaws:
                    break;
                case GamePersonAbilitiesType.ShadowJump:
                    break;
                case GamePersonAbilitiesType.BeastRoar:
                    break;
                case GamePersonAbilitiesType.CurseOfTheTiger:
                    break;
                case GamePersonAbilitiesType.UnderwaterCharm:
                    break;
                case GamePersonAbilitiesType.Seaport:
                    break;
                case GamePersonAbilitiesType.NaturalDefense:
                    break;
                case GamePersonAbilitiesType.WaterWhirlwind:
                    break;
                case GamePersonAbilitiesType.FlamingWhirlwind:
                    break;
                case GamePersonAbilitiesType.IncendiarySphere:
                    break;
                case GamePersonAbilitiesType.DemonicMask:
                    break;
                case GamePersonAbilitiesType.FieryInferno:
                    break;
                case GamePersonAbilitiesType.WindArrow:
                    break;
                case GamePersonAbilitiesType.AbsorbingSpiral:
                    break;
                case GamePersonAbilitiesType.FogBarrage:
                    break;
                case GamePersonAbilitiesType.WindPower:
                    break;
                case GamePersonAbilitiesType.WindHurricane:
                    break;
                case GamePersonAbilitiesType.FluffyEmbrace:
                    break;
                case GamePersonAbilitiesType.BeastLeap:
                    break;
                case GamePersonAbilitiesType.SandWhirlwind:
                    break;
                case GamePersonAbilitiesType.AstralBeam:
                    break;
                case GamePersonAbilitiesType.FlurryOfLight:
                    break;
                case GamePersonAbilitiesType.EtherealProjection:
                    break;
                case GamePersonAbilitiesType.StellarWisdom:
                    break;
                case GamePersonAbilitiesType.StonePillar:
                    break;
                case GamePersonAbilitiesType.MetalShield:
                    break;
                case GamePersonAbilitiesType.GroundCrack:
                    break;
                case GamePersonAbilitiesType.ThunderFist:
                    break;
                case GamePersonAbilitiesType.DarkEnchantment:
                    break;
                case GamePersonAbilitiesType.GlowOfTheNight:
                    break;
                case GamePersonAbilitiesType.ShadowTransformation:
                    break;
                case GamePersonAbilitiesType.EchoesOfDreams:
                    break;
                case GamePersonAbilitiesType.NaturalMarksmanship:
                    break;
                case GamePersonAbilitiesType.ForestShadow:
                    break;
                case GamePersonAbilitiesType.ElvenDexterity:
                    break;
                case GamePersonAbilitiesType.QuiverOfEnergy:
                    break;
                case GamePersonAbilitiesType.RamAttack:
                    break;
                case GamePersonAbilitiesType.IceSweep:
                    break;
                case GamePersonAbilitiesType.WarAssault:
                    break;
                case GamePersonAbilitiesType.SolidDefense:
                    break;
                case GamePersonAbilitiesType.LunarPierce:
                    break;
                case GamePersonAbilitiesType.MagicStealth:
                    break;
                case GamePersonAbilitiesType.DarkSpell:
                    break;
                case GamePersonAbilitiesType.LunarReflection:
                    break;
                case GamePersonAbilitiesType.ShacklesOfDarkness:
                    break;
                case GamePersonAbilitiesType.MagicalHypnosis:
                    break;
                case GamePersonAbilitiesType.FierceClaw:
                    break;
                case GamePersonAbilitiesType.DarkBarrage:
                    break;
                case GamePersonAbilitiesType.FireStream:
                    break;
                case GamePersonAbilitiesType.GoldenArmor:
                    break;
                case GamePersonAbilitiesType.DraconicRegeneration:
                    break;
                case GamePersonAbilitiesType.DragonBanner:
                    break;
                case GamePersonAbilitiesType.TrapWeb:
                    break;
                case GamePersonAbilitiesType.ClawShackles:
                    break;
                case GamePersonAbilitiesType.SilverVenom:
                    break;
                case GamePersonAbilitiesType.OpenBridge:
                    break;
                case GamePersonAbilitiesType.DarkSerpent:
                    break;
                case GamePersonAbilitiesType.SerpentDefense:
                    break;
                case GamePersonAbilitiesType.DraconicRoar:
                    break;
                case GamePersonAbilitiesType.MysticFlight:
                    break;
                case GamePersonAbilitiesType.SymbolicMagic:
                    break;
                case GamePersonAbilitiesType.MagicPassion:
                    break;
                case GamePersonAbilitiesType.SealedProtection:
                    break;
                case GamePersonAbilitiesType.RealityMystification:
                    break;
            }
        }
    }
}

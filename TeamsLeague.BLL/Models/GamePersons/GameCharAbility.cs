using TeamsLeague.DAL.Constants.Match.GamePerson;

namespace TeamsLeague.BLL.Models.GamePersons
{
    public class GameCharAbility
    {
        public GameCharAbilitiesType Type { get; }
        public string Name { get; }
        public string Description { get; }

        //Usability
        public GameCharAbilitiesUsabilityType Usability { get; set; }

        public int ManaCost { get; } = 0;
        public double Recharge { get; } = 0;
        public double RechargeProgress { get; set; } = 0;        //SEC
        //Healing
        public double Healing { get; } = 0;             //PERMANENT HEALING
        //Damage
        public double DamageInUse { get; set; } = 0;     //PERMANENT DAMAGE
        public double DamageInTime { get; set; } = 0;    //TOTAL DAMAGE IN TIME (EXCEPT PERMANENT DAMAGE)
        public double DamageInTimeDuration { get; set; } = 0;  //SEC
        //Speed
        public double SpeedCoefficient { get; set; } = 1;           //0.5 = 50%, 0 = STUN
        public double SpeedCoefficientDuration { get; set; } = 0;   //SEC
        //Power
        public double PowerCoefficient { get; set; } = 1;           //0.5 = 50%
        public double PowerCoefficientDuration { get; set; } = 0;   //SEC
        //Defense
        public double DefenseCoefficient { get; set; } = 1;         //2 = 50%, 999 = IMMORTALITY
        public double DefenseCoefficientDuration { get; set; } = 0; //SEC


        public GameCharAbility(GameCharAbilitiesType type)
        {
            Type = type;
            Name = type.ToString();
            Description = AbilitiesDescriptions.GetDescription(type);

            switch (type)
            {
                //Aurelia
                case GameCharAbilitiesType.GlowOfWings:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 60;
                    SpeedCoefficient = 1.25;
                    SpeedCoefficientDuration = 2;
                    DefenseCoefficient = 999;
                    DefenseCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.ButterflySplash:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 45;
                    Healing = 100;
                    SpeedCoefficient = 1.5;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.EtherealWandering:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 60;
                    Recharge = 30;
                    SpeedCoefficient = 10;
                    SpeedCoefficientDuration = 1;

                    break;
                case GameCharAbilitiesType.WingDust:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 80;
                    Recharge = 36;
                    DamageInTime = 100;
                    DamageInTimeDuration = 6;
                    SpeedCoefficient = 0.25;
                    SpeedCoefficientDuration = 6;
                    break;

                //Ragnus
                case GameCharAbilitiesType.FireCall:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 80;
                    Recharge = 20;
                    DamageInUse = 80;
                    DamageInTime = 80;
                    DamageInTimeDuration = 6;

                    break;
                case GameCharAbilitiesType.Arson:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 40;
                    Recharge = 24;
                    PowerCoefficient = 1.5;
                    PowerCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.TanningTactics:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 40;
                    Recharge = 24;
                    PowerCoefficient = 1.5;
                    PowerCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.FireShield:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 80;
                    Recharge = 36;
                    DefenseCoefficient = 1.75;
                    DefenseCoefficientDuration = 6;
                    break;

                //Ivy
                case GameCharAbilitiesType.NatureWeaving:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 90;
                    Recharge = 27;
                    DamageInUse = 50;
                    SpeedCoefficient = 0.75;
                    SpeedCoefficientDuration = 6;
                    break;
                case GameCharAbilitiesType.ConfusingForest:

                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 33;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.HealingBalm:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 80;
                    Recharge = 24;
                    Healing = 80;
                    Healing = 80;
                    PowerCoefficient = 1.5;
                    PowerCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.EmergenceOfLife:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 250;
                    Recharge = 90;
                    DamageInTime = 500;
                    DamageInTimeDuration = 20;
                    break;

                //Cogsworth
                case GameCharAbilitiesType.CannonCannon:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 45;
                    DamageInUse = 50;
                    DamageInTime = 150;
                    DamageInTimeDuration = 4;
                    break;
                case GameCharAbilitiesType.ProtectiveEnergyShield:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 80;
                    Recharge = 36;
                    DefenseCoefficient = 1.7;
                    DefenseCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.MechanicalKaizen:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 50;
                    Recharge = 20;
                    SpeedCoefficient = 1.4;
                    SpeedCoefficientDuration = 2;
                    DefenseCoefficient = 1.2;
                    DefenseCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.ReprogrammingMagic:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 160;
                    Recharge = 120;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 3;
                    break;

                //Elara
                case GameCharAbilitiesType.IceShot:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 80;
                    Recharge = 30;
                    DamageInUse = 150;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.IceFreeze:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 36;
                    DamageInUse = 60;
                    DamageInTime = 120;
                    DamageInTimeDuration = 3;

                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.NaturalResilience:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 120;
                    Recharge = 90;
                    DefenseCoefficient = 999;
                    DefenseCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.ButterflyDance:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 80;
                    Recharge = 30;

                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 3;
                    break;

                //Thundar
                case GameCharAbilitiesType.ThunderStrike:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 24;

                    DamageInUse = 180;
                    SpeedCoefficient = 0.8;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.Electrolayer:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 36;
                    DamageInUse = 120;
                    DamageInTime = 120;
                    DamageInTimeDuration = 2;
                    break;
                case GameCharAbilitiesType.ElectricShield:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 45;
                    DefenseCoefficient = 2;
                    DefenseCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.ElectrostaticDischarge:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 140;
                    Recharge = 96;
                    DamageInUse = 100;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 2;
                    break;

                //Morok
                case GameCharAbilitiesType.StoneFist:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 30;
                    DamageInUse = 120;
                    break;
                case GameCharAbilitiesType.Earthquake:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 36;
                    DamageInUse = 80;
                    SpeedCoefficient = 0.8;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.RunicBarrier:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 90;
                    Recharge = 37;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.StatueBaby:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 150;
                    Recharge = 60;
                    DamageInTime = 350;
                    DamageInTimeDuration = 20;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 20;
                    break;

                //Lyra
                case GameCharAbilitiesType.RealityDimension:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 36;
                    SpeedCoefficient = 0.3;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.Abracadabra:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 60;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.CupidsArrow:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 140;
                    Recharge = 45;
                    DamageInUse = 100;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.UmbrellaOfSurprises:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 30;
                    DefenseCoefficient = 999;
                    DefenseCoefficientDuration = 3;
                    break;

                //Grimclaw
                case GameCharAbilitiesType.DeadlyClaws:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 32;
                    DamageInUse = 100;
                    DamageInTime = 200;
                    DamageInTimeDuration = 5;
                    break;
                case GameCharAbilitiesType.ShadowJump:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 80;
                    Recharge = 45;
                    SpeedCoefficient = 2;
                    SpeedCoefficientDuration = 1;
                    DefenseCoefficient = 999;
                    DefenseCoefficientDuration = 1;
                    break;
                case GameCharAbilitiesType.BeastRoar:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 120;
                    Recharge = 70;
                    PowerCoefficient = 1.3;
                    PowerCoefficientDuration = 5;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.CurseOfTheTiger:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 30;
                    Recharge = 10;
                    SpeedCoefficient = 1.2;
                    SpeedCoefficientDuration = 9;
                    PowerCoefficient = 1.2;
                    PowerCoefficientDuration = 9;
                    DefenseCoefficient = 1.2;
                    DefenseCoefficientDuration = 9;
                    break;

                //Nymphia
                case GameCharAbilitiesType.UnderwaterCharm:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 70;
                    Recharge = 30;
                    DamageInUse = 70;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 1;
                    break;
                case GameCharAbilitiesType.Seaport:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 120;
                    Recharge = 38;
                    Healing = 120;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.NaturalDefense:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 150;
                    Recharge = 115;
                    DefenseCoefficient = 999;
                    DefenseCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.WaterWhirlwind:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 180;
                    Recharge = 130;
                    DamageInUse = 300;
                    DamageInTime = 300;
                    DamageInTimeDuration = 3;
                    break;

                //Ignis
                case GameCharAbilitiesType.FlamingWhirlwind:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 30;
                    DamageInUse = 100;
                    DamageInTime = 180;
                    DamageInTimeDuration = 3;
                    break;
                case GameCharAbilitiesType.IncendiarySphere:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 40;
                    DamageInUse = 80;
                    DamageInTime = 250;
                    DamageInTimeDuration = 3;
                    break;
                case GameCharAbilitiesType.DemonicMask:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 45;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.FieryInferno:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 160;
                    Recharge = 120;
                    DamageInUse = 100;
                    DamageInTime = 350;
                    DamageInTimeDuration = 3;
                    break;

                //Vera
                case GameCharAbilitiesType.WindArrow:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 150;
                    Recharge = 70;
                    DamageInUse = 200;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.AbsorbingSpiral:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 140;
                    Recharge = 60;
                    DamageInUse = 100;
                    SpeedCoefficient = 0.7;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.FogBarrage:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 160;
                    Recharge = 80;
                    DamageInUse = 180;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.WindPower:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 180;
                    Recharge = 90;
                    DamageInUse = 120;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 4;
                    break;

                //Zephyr
                case GameCharAbilitiesType.WindHurricane:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 200;
                    Recharge = 120;
                    DamageInUse = 100;
                    SpeedCoefficient = 0.8;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.FluffyEmbrace:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 60;
                    DamageInUse = 80;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.BeastLeap:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 80;
                    Recharge = 24;
                    DamageInUse = 100;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.SandWhirlwind:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 50;
                    DamageInUse = 120;
                    SpeedCoefficient = 0.4;
                    SpeedCoefficientDuration = 4;
                    break;

                //Solus
                case GameCharAbilitiesType.AstralBeam:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 180;
                    Recharge = 160;
                    DamageInUse = 250;
                    SpeedCoefficient = 0.7;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.FlurryOfLight:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 140;
                    Recharge = 80;
                    DamageInUse = 120;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.EtherealProjection:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 60;
                    SpeedCoefficient = 2;
                    SpeedCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.StellarWisdom:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 120;
                    Recharge = 90;
                    SpeedCoefficient = 1.5;
                    SpeedCoefficientDuration = 5;
                    break;

                //Grom
                case GameCharAbilitiesType.StonePillar:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 40;
                    DamageInUse = 130;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.MetalShield:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 80;
                    Recharge = 45;
                    DefenseCoefficient = 1.6;
                    DefenseCoefficientDuration = 6;
                    break;
                case GameCharAbilitiesType.GroundCrack:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 36;
                    DamageInUse = 150;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.ThunderFist:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 30;
                    DamageInUse = 180;
                    break;

                //Vespera
                case GameCharAbilitiesType.DarkEnchantment:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 40;
                    DamageInUse = 140;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.GlowOfTheNight:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 36;
                    SpeedCoefficient = 1.4;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.ShadowTransformation:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 45;
                    SpeedCoefficient = 1.4;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.EchoesOfDreams:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 130;
                    Recharge = 32;
                    DamageInUse = 170;
                    break;

                //Zarael
                case GameCharAbilitiesType.NaturalMarksmanship:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 190;
                    break;
                case GameCharAbilitiesType.ForestShadow:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 30;
                    SpeedCoefficient = 1.5;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.ElvenDexterity:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 110;
                    Recharge = 40;
                    SpeedCoefficient = 1.6;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.QuiverOfEnergy:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 130;
                    Recharge = 45;
                    DamageInUse = 160;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 3;
                    break;

                //Tusk
                case GameCharAbilitiesType.RamAttack:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 200;
                    break;
                case GameCharAbilitiesType.IceSweep:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 35;
                    SpeedCoefficient = 0.7;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.WarAssault:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 130;
                    Recharge = 30;
                    DamageInUse = 210;
                    break;
                case GameCharAbilitiesType.SolidDefense:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 40;
                    DefenseCoefficient = 1.4;
                    DefenseCoefficientDuration = 5;
                    break;

                //Lunara
                case GameCharAbilitiesType.LunarPierce:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 190;
                    break;
                case GameCharAbilitiesType.MagicStealth:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 35;
                    SpeedCoefficient = 1.5;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.DarkSpell:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 30;
                    SpeedCoefficient = 0.5;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.LunarReflection:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 30;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 4;
                    break;

                //Hex
                case GameCharAbilitiesType.ShacklesOfDarkness:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 32;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.MagicalHypnosis:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 35;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.FierceClaw:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 200;
                    break;
                case GameCharAbilitiesType.DarkBarrage:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 130;
                    Recharge = 30;
                    DamageInUse = 140;
                    break;

                //Dracor
                case GameCharAbilitiesType.FireStream:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 190;
                    break;
                case GameCharAbilitiesType.GoldenArmor:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 140;
                    Recharge = 40;
                    DefenseCoefficient = 1.6;
                    DefenseCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.DraconicRegeneration:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 110;
                    Recharge = 35;
                    Healing = 230;
                    break;
                case GameCharAbilitiesType.DragonBanner:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 30;
                    PowerCoefficient = 50;
                    PowerCoefficientDuration = 5;
                    break;

                //Arachna
                case GameCharAbilitiesType.TrapWeb:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 100;
                    Recharge = 30;
                    SpeedCoefficient = 0;
                    SpeedCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.ClawShackles:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 110;
                    Recharge = 35;
                    SpeedCoefficient = 0.6;
                    SpeedCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.SilverVenom:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInTime = 50;
                    DamageInTimeDuration = 6;
                    break;
                case GameCharAbilitiesType.OpenBridge:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 40;
                    SpeedCoefficient = 1.6;
                    SpeedCoefficientDuration = 4;
                    break;

                //Zilon
                case GameCharAbilitiesType.DarkSerpent:
                    Usability = GameCharAbilitiesUsabilityType.Enemy;

                    ManaCost = 120;
                    Recharge = 28;
                    DamageInUse = 190;
                    break;
                case GameCharAbilitiesType.SerpentDefense:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 110;
                    Recharge = 35;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 4;
                    break;
                case GameCharAbilitiesType.DraconicRoar:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 130;
                    Recharge = 30;
                    PowerCoefficient = 1.2;
                    PowerCoefficientDuration = 3;
                    DefenseCoefficient = 1.3;
                    DefenseCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.MysticFlight:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 100;
                    Recharge = 40;
                    SpeedCoefficient = 1.6;
                    SpeedCoefficientDuration = 4;
                    break;

                //Lyth
                case GameCharAbilitiesType.SymbolicMagic:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 120;
                    Recharge = 28;
                    SpeedCoefficient = 3;
                    PowerCoefficientDuration = 2;
                    break;
                case GameCharAbilitiesType.MagicPassion:
                    Usability = GameCharAbilitiesUsabilityType.Ally;

                    ManaCost = 130;
                    Recharge = 30;
                    SpeedCoefficient = 1.8;
                    SpeedCoefficientDuration = 3;
                    PowerCoefficient = 1.8;
                    PowerCoefficientDuration = 3;
                    break;
                case GameCharAbilitiesType.SealedProtection:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 100;
                    Recharge = 40;
                    DefenseCoefficient = 2;
                    DefenseCoefficientDuration = 5;
                    break;
                case GameCharAbilitiesType.RealityMystification:
                    Usability = GameCharAbilitiesUsabilityType.Self;

                    ManaCost = 120;
                    Recharge = 32;
                    DefenseCoefficient = 1.8;
                    DefenseCoefficientDuration = 4;
                    break;
            }
        }

        public GameCharAbility(GameCharAbility reference)
        {
            Type = reference.Type;
            Name = reference.Name;
            Usability = reference.Usability;
            Healing = reference.Healing;
            DamageInTime = reference.DamageInTime;
            DamageInTimeDuration = reference.DamageInTimeDuration;
            SpeedCoefficient = reference.SpeedCoefficient;
            SpeedCoefficientDuration = reference.SpeedCoefficientDuration;
            PowerCoefficient = reference.PowerCoefficient;
            PowerCoefficientDuration = reference.PowerCoefficientDuration;
            DefenseCoefficient = reference.DefenseCoefficient;
            DefenseCoefficientDuration = reference.DefenseCoefficientDuration;
        }


        public bool EffectCanBeDeleted() =>
            DamageInTimeDuration <= 0 &&
            SpeedCoefficientDuration <= 0 &&
            PowerCoefficientDuration <= 0 &&
            DefenseCoefficientDuration <= 0;

        /// <returns>True is effect can be removed.</returns>
        public bool AddSecondToEffect()
        {
            --DamageInTimeDuration;
            DamageInTime = DamageInTimeDuration >= 0 ? 0 : DamageInTimeDuration >= 1 ? DamageInTime *= DamageInTimeDuration : DamageInTime;

            --SpeedCoefficientDuration;
            SpeedCoefficient = SpeedCoefficientDuration >= 0 ? 0 : SpeedCoefficientDuration >= 1 ? SpeedCoefficient *= SpeedCoefficientDuration : SpeedCoefficient;

            --PowerCoefficientDuration;
            PowerCoefficient = PowerCoefficientDuration >= 0 ? 0 : PowerCoefficientDuration >= 1 ? PowerCoefficient *= PowerCoefficientDuration : PowerCoefficient;

            --DefenseCoefficientDuration;
            DefenseCoefficient = DefenseCoefficientDuration >= 0 ? 0 : DefenseCoefficientDuration >= 1 ? DefenseCoefficient *= DefenseCoefficientDuration : DefenseCoefficient;


            if (DamageInTimeDuration <= 0 && SpeedCoefficientDuration <= 0 && PowerCoefficientDuration <= 0 && DefenseCoefficientDuration <= 0)
            {
                return true;
            }

            return false;
        }
    }
}

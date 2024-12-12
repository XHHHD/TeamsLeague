using TeamsLeague.BLL.Models.GamePersons;
using TeamsLeague.BLL.Models.MatchParts.MatchMap;
using TeamsLeague.BLL.Services;
using TeamsLeague.DAL.Constants.Match.GamePerson;
using TeamsLeague.DAL.Entities.MatchParts;

namespace TeamsLeague.DAL.Constants.GamePersons
{
    public class GameChar
    {
        private readonly List<double> Levels = new() { 0, 1000, 2200, 3500, 4900, 6400, 8000, 10000, 12500, 15500, 19000, 23000, 27500, 32500, 38000, 44000, 50500, 57500 };


        private double basicDamage;
        private double basicResistance;
        private double basicMobility;
        private double currentHealth;
        private double maxHealth;


        public MatchSeat MatchSeat { get; set; }

        public GameCharType Type { get; }
        public HashSet<GameCharAbility> Abilities { get; }
        public HashSet<GameCharAbility> Effects { get; set; }

        public string Name { get; }
        public string Description { get; }
        public string Image { get; }
        public double Experience { get; set; }
        public int Level { get; set; }
        public double Tension { get; set; }
        public Decision CurrentDecision { get; set; }
        public Location CurrentLocation { get; set; }
        public bool IsActed { get; set; }
        public bool IsInCombat { get; set; }

        //Attack
        public double CurrentDamage => GetCurrentDamage();
        public double BasicDamage { get => basicDamage; }
        public double CurrentResistance => GetCurrentResistance();
        public double BasicResistance { get => basicResistance; }

        public double CurrentMobility => GetCurrentMobility();
        public double BasicMobility { get => basicMobility; }

        //Health
        public double CurrentHealth { get => currentHealth;}
        public double MaxHealth { get => maxHealth; }
        public double HealthRegeneration { get; }

        //Mana
        public double CurrentMana { get; set; }
        public double MaxMana { get; set; }
        public double ManaRegeneration { get; set; }


        public GameChar(GameCharType type)
        {
            Type = type;
            Effects = new();
            Name = Type.ToString();
            Description = CharDescriptions.GetDescription(type);
            Image = ImagesService.GetCharImgUrl(type);
            Experience = 0;
            Level = 1;
            Tension = 0;
            CurrentDecision = Decision.ToFarm;
            IsActed = false;

            switch (type)
            {
                case GameCharType.Aurelia:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.GlowOfWings),
                        new GameCharAbility(GameCharAbilitiesType.ButterflySplash),
                        new GameCharAbility(GameCharAbilitiesType.EtherealWandering),
                        new GameCharAbility(GameCharAbilitiesType.WingDust),
                    };
                    basicDamage = 50;
                    basicResistance = 5;
                    basicMobility = 2;
                    maxHealth = 310;
                    HealthRegeneration = 1;
                    MaxMana = 130;
                    ManaRegeneration = 2;
                    break;
                case GameCharType.Ragnus:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.FireCall),
                        new GameCharAbility(GameCharAbilitiesType.Arson),
                        new GameCharAbility(GameCharAbilitiesType.TanningTactics),
                        new GameCharAbility(GameCharAbilitiesType.FireShield),
                    };
                    basicDamage = 65;
                    basicResistance = 10;
                    basicMobility = 1.1;
                    maxHealth = 400;
                    HealthRegeneration = 1.5;
                    MaxMana = 100;
                    ManaRegeneration = 1.5;
                    break;
                case GameCharType.Ivy:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.NatureWeaving),
                        new GameCharAbility(GameCharAbilitiesType.ConfusingForest),
                        new GameCharAbility(GameCharAbilitiesType.HealingBalm),
                        new GameCharAbility(GameCharAbilitiesType.EmergenceOfLife),
                    };
                    basicDamage = 55;
                    basicResistance = 8;
                    basicMobility = 1.4;
                    maxHealth = 280;
                    HealthRegeneration = 1.1;
                    MaxMana = 120;
                    ManaRegeneration = 1.6;
                    break;
                case GameCharType.Cogsworth:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.CannonCannon),
                        new GameCharAbility(GameCharAbilitiesType.ProtectiveEnergyShield),
                        new GameCharAbility(GameCharAbilitiesType.MechanicalKaizen),
                        new GameCharAbility(GameCharAbilitiesType.ReprogrammingMagic),
                    };
                    basicDamage = 75;
                    basicResistance = 14;
                    basicMobility = 1.5;
                    maxHealth = 225;
                    HealthRegeneration = 0.7;
                    MaxMana = 110;
                    ManaRegeneration = 0.4;
                    break;
                case GameCharType.Elara:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.IceShot),
                        new GameCharAbility(GameCharAbilitiesType.IceFreeze),
                        new GameCharAbility(GameCharAbilitiesType.NaturalResilience),
                        new GameCharAbility(GameCharAbilitiesType.ButterflyDance),
                    };
                    basicDamage = 67;
                    basicResistance = 8;
                    basicMobility = 1;
                    maxHealth = 260;
                    HealthRegeneration = 1.2;
                    MaxMana = 180;
                    ManaRegeneration = 1.6;
                    break;
                case GameCharType.Thundar:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.ThunderStrike),
                        new GameCharAbility(GameCharAbilitiesType.Electrolayer),
                        new GameCharAbility(GameCharAbilitiesType.ElectricShield),
                        new GameCharAbility(GameCharAbilitiesType.ElectrostaticDischarge),
                    };
                    basicDamage = 63;
                    basicResistance = 12;
                    basicMobility = 1;
                    maxHealth = 320;
                    HealthRegeneration = 1.7;
                    MaxMana = 140;
                    ManaRegeneration = 1;
                    break;
                case GameCharType.Morok:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.StoneFist),
                        new GameCharAbility(GameCharAbilitiesType.Earthquake),
                        new GameCharAbility(GameCharAbilitiesType.RunicBarrier),
                        new GameCharAbility(GameCharAbilitiesType.StatueBaby),
                    };
                    basicDamage = 54;
                    basicResistance = 18;
                    basicMobility = 0.8;
                    maxHealth = 610;
                    HealthRegeneration = 1.8;
                    MaxMana = 90;
                    ManaRegeneration = 1;
                    break;
                case GameCharType.Lyra:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.RealityDimension),
                        new GameCharAbility(GameCharAbilitiesType.Abracadabra),
                        new GameCharAbility(GameCharAbilitiesType.CupidsArrow),
                        new GameCharAbility(GameCharAbilitiesType.UmbrellaOfSurprises),
                    };
                    basicDamage = 57;
                    basicResistance = 4;
                    basicMobility = 1.3;
                    maxHealth = 290;
                    HealthRegeneration = 1.6;
                    MaxMana = 190;
                    ManaRegeneration = 2;
                    break;
                case GameCharType.Grimclaw:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.DeadlyClaws),
                        new GameCharAbility(GameCharAbilitiesType.ShadowJump),
                        new GameCharAbility(GameCharAbilitiesType.BeastRoar),
                        new GameCharAbility(GameCharAbilitiesType.CurseOfTheTiger),
                    };
                    basicDamage = 73;
                    basicResistance = 15;
                    basicMobility = 1.3;
                    maxHealth = 490;
                    HealthRegeneration = 2.1;
                    MaxMana = 110;
                    ManaRegeneration = 1.4;
                    break;
                case GameCharType.Nymphia:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.UnderwaterCharm),
                        new GameCharAbility(GameCharAbilitiesType.Seaport),
                        new GameCharAbility(GameCharAbilitiesType.NaturalDefense),
                        new GameCharAbility(GameCharAbilitiesType.WaterWhirlwind),
                    };
                    basicDamage = 58;
                    basicResistance = 6;
                    basicMobility = 1.2;
                    maxHealth = 320;
                    HealthRegeneration = 2.2;
                    MaxMana = 140;
                    ManaRegeneration = 1.1;
                    break;
                case GameCharType.Ignis:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.FlamingWhirlwind),
                        new GameCharAbility(GameCharAbilitiesType.IncendiarySphere),
                        new GameCharAbility(GameCharAbilitiesType.DemonicMask),
                        new GameCharAbility(GameCharAbilitiesType.FieryInferno),
                    };
                    basicDamage = 63;
                    basicResistance = 11;
                    basicMobility = 0.9;
                    maxHealth = 360;
                    HealthRegeneration = 1.1;
                    MaxMana = 150;
                    ManaRegeneration = 1.1;
                    break;
                case GameCharType.Vera:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.WindArrow),
                        new GameCharAbility(GameCharAbilitiesType.AbsorbingSpiral),
                        new GameCharAbility(GameCharAbilitiesType.FogBarrage),
                        new GameCharAbility(GameCharAbilitiesType.WindPower),
                    };
                    basicDamage = 61;
                    basicResistance = 10;
                    basicMobility = 1;
                    maxHealth = 320;
                    HealthRegeneration = 1.2;
                    MaxMana = 130;
                    ManaRegeneration = 1;
                    break;
                case GameCharType.Zephyr:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.WindHurricane),
                        new GameCharAbility(GameCharAbilitiesType.FluffyEmbrace),
                        new GameCharAbility(GameCharAbilitiesType.BeastLeap),
                        new GameCharAbility(GameCharAbilitiesType.SandWhirlwind),
                    };
                    basicDamage = 64;
                    basicResistance = 12;
                    basicMobility = 1.8;
                    maxHealth = 340;
                    HealthRegeneration = 1.6;
                    MaxMana = 135;
                    ManaRegeneration = 1.4;
                    break;
                case GameCharType.Solus:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.AstralBeam),
                        new GameCharAbility(GameCharAbilitiesType.FlurryOfLight),
                        new GameCharAbility(GameCharAbilitiesType.EtherealProjection),
                        new GameCharAbility(GameCharAbilitiesType.StellarWisdom),
                    };
                    basicDamage = 66;
                    basicResistance = 9;
                    basicMobility = 1.1;
                    maxHealth = 315;
                    HealthRegeneration = 1.3;
                    MaxMana = 210;
                    ManaRegeneration = 1.4;
                    break;
                case GameCharType.Grom:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.StonePillar),
                        new GameCharAbility(GameCharAbilitiesType.MetalShield),
                        new GameCharAbility(GameCharAbilitiesType.GroundCrack),
                        new GameCharAbility(GameCharAbilitiesType.ThunderFist),
                    };
                    basicDamage = 69;
                    basicResistance = 16;
                    basicMobility = 0.9;
                    maxHealth = 550;
                    HealthRegeneration = 1.9;
                    MaxMana = 110;
                    ManaRegeneration = 1.2;
                    break;
                case GameCharType.Vespera:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.DarkEnchantment),
                        new GameCharAbility(GameCharAbilitiesType.GlowOfTheNight),
                        new GameCharAbility(GameCharAbilitiesType.ShadowTransformation),
                        new GameCharAbility(GameCharAbilitiesType.EchoesOfDreams),
                    };
                    basicDamage = 62;
                    basicResistance = 5;
                    basicMobility = 2.1;
                    maxHealth = 245;
                    HealthRegeneration = 2.3;
                    MaxMana = 195;
                    ManaRegeneration = 2.1;
                    break;
                case GameCharType.Zarael:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.NaturalMarksmanship),
                        new GameCharAbility(GameCharAbilitiesType.ForestShadow),
                        new GameCharAbility(GameCharAbilitiesType.ElvenDexterity),
                        new GameCharAbility(GameCharAbilitiesType.QuiverOfEnergy),
                    };
                    basicDamage = 78;
                    basicResistance = 11;
                    basicMobility = 1.4;
                    maxHealth = 305;
                    HealthRegeneration = 1.2;
                    MaxMana = 125;
                    ManaRegeneration = 1.3;
                    break;
                case GameCharType.Tusk:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.RamAttack),
                        new GameCharAbility(GameCharAbilitiesType.IceSweep),
                        new GameCharAbility(GameCharAbilitiesType.WarAssault),
                        new GameCharAbility(GameCharAbilitiesType.SolidDefense),
                    };
                    basicDamage = 71;
                    basicResistance = 13;
                    basicMobility = 1;
                    maxHealth = 400;
                    HealthRegeneration = 1.8;
                    MaxMana = 110;
                    ManaRegeneration = 1;
                    break;
                case GameCharType.Lunara:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.LunarPierce),
                        new GameCharAbility(GameCharAbilitiesType.MagicStealth),
                        new GameCharAbility(GameCharAbilitiesType.DarkSpell),
                        new GameCharAbility(GameCharAbilitiesType.LunarReflection),
                    };
                    basicDamage = 59;
                    basicResistance = 9;
                    basicMobility = 1.1;
                    maxHealth = 295;
                    HealthRegeneration = 1.8;
                    MaxMana = 140;
                    ManaRegeneration = 1.8;
                    break;
                case GameCharType.Hex:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.ShacklesOfDarkness),
                        new GameCharAbility(GameCharAbilitiesType.MagicalHypnosis),
                        new GameCharAbility(GameCharAbilitiesType.FierceClaw),
                        new GameCharAbility(GameCharAbilitiesType.DarkBarrage),
                    };
                    basicDamage = 72;
                    basicResistance = 14;
                    basicMobility = 1.6;
                    maxHealth = 385;
                    HealthRegeneration = 1.7;
                    MaxMana = 115;
                    ManaRegeneration = 1.2;
                    break;
                case GameCharType.Dracor:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.FireStream),
                        new GameCharAbility(GameCharAbilitiesType.GoldenArmor),
                        new GameCharAbility(GameCharAbilitiesType.DraconicRegeneration),
                        new GameCharAbility(GameCharAbilitiesType.DragonBanner),
                    };
                    basicDamage = 68;
                    basicResistance = 15;
                    basicMobility = 1.9;
                    maxHealth = 380;
                    HealthRegeneration = 1.5;
                    MaxMana = 140;
                    ManaRegeneration = 1.6;
                    break;
                case GameCharType.Arachna:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.TrapWeb),
                        new GameCharAbility(GameCharAbilitiesType.ClawShackles),
                        new GameCharAbility(GameCharAbilitiesType.SilverVenom),
                        new GameCharAbility(GameCharAbilitiesType.OpenBridge),
                    };
                    basicDamage = 72;
                    basicResistance = 11;
                    basicMobility = 1.8;
                    maxHealth = 335;
                    HealthRegeneration = 1.2;
                    MaxMana = 140;
                    ManaRegeneration = 1.2;
                    break;
                case GameCharType.Zilon:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.DarkSerpent),
                        new GameCharAbility(GameCharAbilitiesType.SerpentDefense),
                        new GameCharAbility(GameCharAbilitiesType.DraconicRoar),
                        new GameCharAbility(GameCharAbilitiesType.MysticFlight),
                    };
                    basicDamage = 70;
                    basicResistance = 16;
                    basicMobility = 1.7;
                    maxHealth = 475;
                    HealthRegeneration = 1.5;
                    MaxMana = 120;
                    ManaRegeneration = 1;
                    break;
                case GameCharType.Lyth:
                    Abilities = new HashSet<GameCharAbility>
                    {
                        new GameCharAbility(GameCharAbilitiesType.SymbolicMagic),
                        new GameCharAbility(GameCharAbilitiesType.MagicPassion),
                        new GameCharAbility(GameCharAbilitiesType.SealedProtection),
                        new GameCharAbility(GameCharAbilitiesType.RealityMystification),
                    };
                    basicDamage = 60;
                    basicResistance = 13;
                    basicMobility = 1;
                    maxHealth = 315;
                    HealthRegeneration = 1.4;
                    MaxMana = 130;
                    ManaRegeneration = 1.3;
                    break;
            }

            GoToBase(0);
        }


        public void GoToBase(double mapTime)
        {
            var timeCoefficient = -1 * (mapTime / 60);

            currentHealth = MaxHealth;
            CurrentMana = MaxMana;
            Tension = timeCoefficient < 40 ? -15 + timeCoefficient : -55;

            Effects.Clear();
            IsInCombat = false;
        }

        public void SetUsingLevel(double level)
        {
            basicDamage *= level;
            basicResistance *= level;
            basicMobility *= level;
        }

        public double GetPassiveIncomeCoefficient()
        {
            const double minIncome = 0.0;
            const double maxIncome = 1.0;

            double a = 0.1;
            double b = 0.03;

            double passiveIncome = minIncome + (maxIncome - minIncome) / (1 + a * Math.Pow(Math.E, -b * CurrentDamage));

            return passiveIncome;
        }


        public void AddExp(double addExp)
        {
            Experience += addExp;

            if (Level < Levels.Count - 1)
            {
                if (Experience > Levels[Level])
                {
                    LevelUp();
                }
            }
        }

        private void LevelUp()
        {
            Level++;
            basicDamage *= 1.5;
            basicResistance *= 1.5;
            basicMobility *= 1.5;
            maxHealth *= 1.5;
            MaxMana *= 1.5;
        }

        public void ChampMadeAMove()
        {
            IsActed = true;

            foreach (var ability in Abilities)
            {
                ability.RechargeProgress--;
                if (ability.RechargeProgress < 0) ability.RechargeProgress = 0;
            }

            foreach (var effect in Effects.ToArray())
            {
                if (effect.AddSecondToEffect())
                {
                    Effects.Remove(effect);
                }
            }
        }

        public void GetDamage(double damage)
        {
            currentHealth -= damage;
            
            if (currentHealth < 0) currentHealth = 0;
        }

        public void GetHeal(double heal)
        {
            currentHealth += heal;

            if(currentHealth > MaxHealth) currentHealth = MaxHealth;
        }

        public void Hit(GameChar enemy)
        {
            var damage = CurrentDamage - enemy.CurrentResistance;

            enemy.GetDamage(damage);
        }

        public void Hit(MapObject mapObject)
        {
            mapObject.Health -= CurrentDamage - mapObject.Defense;

            if (mapObject.Health > 0)
            {
                currentHealth -= mapObject.Attack - CurrentResistance;
            }
            else
            {
                Tension -= 15;
            }
        }


        #region ABILITIES
        /// <summary>
        /// Champ get own ability effect on self.
        /// </summary>
        /// <param name="ability">Own ability.</param>
        public void UseSelfAbility(GameCharAbility ability)
        {
            CheckUsability(ability.Usability, GameCharAbilitiesUsabilityType.Self);
            TryToUseAbility(ability);

            GetHeal(ability.Healing);

            Effects.Add(new GameCharAbility(ability));
        }

        /// <summary>
        /// Champ get ally ability effect on self.
        /// </summary>
        /// <param name="ability">Ability of ally.</param>
        public void UseAllyAbility(GameCharAbility ability, IEnumerable<GameChar> allies)
        {
            CheckUsability(ability.Usability, GameCharAbilitiesUsabilityType.Ally);
            TryToUseAbility(ability);

            foreach (var champ in allies)
            {
                champ.GetHeal(ability.Healing);

                champ.Effects.Add(new GameCharAbility(ability));
            }
        }

        /// <summary>
        /// Champ send ability effect on enemy.
        /// </summary>
        /// <param name="ability">Ability of champ.</param>
        public void UseEnemyAbility(GameCharAbility ability, GameChar enemy)
        {
            CheckUsability(ability.Usability, GameCharAbilitiesUsabilityType.Enemy);
            TryToUseAbility(ability);

            enemy.GetDamage(ability.DamageInUse);
            enemy.GetDamage(ability.DamageInTime);

            enemy.Effects.Add(new GameCharAbility(ability));
        }

        private void CheckUsability(GameCharAbilitiesUsabilityType abilityType, GameCharAbilitiesUsabilityType targetIsType)
        {
            if (abilityType != targetIsType)
            {
                throw new Exception($"This is for {abilityType} using only!");
            }
        }

        private bool TryToUseAbility(GameCharAbility ability)
        {
            if (CurrentMana >= ability.ManaCost)
            {
                CurrentMana -= ability.ManaCost;
                ability.RechargeProgress = ability.Recharge;

                return true;
            }
            return false;
        }
        #endregion


        #region GET STATS
        public double GetCurrentDamage()
        {
            var damage = BasicDamage;

            foreach (var ef in Effects)
            {
                damage *= ef.PowerCoefficient;
            }

            return damage;
        }

        public double GetCurrentResistance()
        {
            var resistance = BasicResistance;

            foreach (var ef in Effects)
            {
                resistance *= ef.DefenseCoefficient;
            }

            return resistance;
        }

        public double GetCurrentMobility()
        {
            var mobility = BasicResistance;

            foreach (var ef in Effects)
            {
                mobility *= ef.SpeedCoefficient;
            }

            return mobility;
        }



        public double GetPower() => (CurrentHealth / MaxHealth) * CurrentDamage;

        public double GetMentalPowerDecision() => (CurrentHealth / MaxHealth) * CurrentDamage + (MatchSeat.Member.MentalPower * (MatchSeat.Member.MentalHealth / MatchSeat.Member.MaxMentalHealth));

        public double GetMobilityPower() => (CurrentHealth / MaxHealth) * CurrentDamage + (BasicMobility * MatchSeat.Member.Defense * (MatchSeat.Member.MentalHealth / MatchSeat.Member.MaxMentalHealth));
        #endregion
    }
}

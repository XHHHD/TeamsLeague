using Microsoft.EntityFrameworkCore;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MatchParts;
using TeamsLeague.BLL.Models.MatchParts.MatchMap;
using TeamsLeague.BLL.Services.Generators;
using TeamsLeague.DAL.Constants.GamePersons;
using TeamsLeague.DAL.Constants.Match;
using TeamsLeague.DAL.Constants.Match.GamePerson;
using TeamsLeague.DAL.Constants.Member;
using TeamsLeague.DAL.Context;
using TeamsLeague.DAL.Entities.MatchParts;
using TeamsLeague.DAL.Entities.MatchParts.KDA;
using TeamsLeague.DAL.Entities.MemberParts;

namespace TeamsLeague.BLL.Services
{
    public class MatchService : IMatchService
    {
        public const double maximumGoldIncome = 10.0;  //IN SEC
        public const double maximumExpIncome = 10.0;   //IM SEC
        public const double aggressionCoefficient = 0.6;


        private readonly GameDBContext _context;
        private readonly Random _random;
        private LogGenerator _logger;


        public Match CurrentMatch { get; set; }
        public MatchMapVM Map { get; set; }
        private HashSet<MapObject> DestroyedTurrets { get; set; }
        private HashSet<GameCharType> Picks { get; set; } = new();
        private List<GameChar> Characters { get; set; }


        public MatchService(GameDBContext context, Random random)
        {
            _context = context;
            _random = random;
        }


        public MatchModel GetMatch(int matchId)
        {
            var match = _context.Matches.FirstOrDefault(m => m.Id == matchId)
                ?? throw new Exception("Match wasn't found!");

            var result = new MatchModel(match);

            return result;
        }


        //  ╔═════════════════════════════════════════════════════════════════════════════════════════════════╗
        //  ║  |\\\V///|    ╔═══╗╔═══╗╔═══╗    ╔╗  ╔╗╔══╗╔════╗╔══╗╔╗╔╗   ╔╗  ╔══╗╔═══╗╔══╗╔══╗     |\\\V///| ║
        //  ║  |\\\V///|    ║╔═╗║║╔═╗║║╔══╝    ║║  ║║║╔╗║╚═╗╔═╝║╔═╝║║║║   ║║  ║╔╗║║╔══╝╚╗╔╝║╔═╝     |\\\V///| ║
        //  ║  |\\\V///|    ║╚═╝║║╚═╝║║╚══╗ ╔╗ ║╚╗╔╝║║╚╝║  ║║  ║║  ║╚╝║   ║║  ║║║║║║╔═╗ ║║ ║║       |\\\V///| ║
        //  ║  |\\\V///|    ║╔══╝║╔╗╔╝║╔══╝ ╚╝ ║╔╗╔╗║║╔╗║  ║║  ║║  ║╔╗║   ║║  ║║║║║║╚╗║ ║║ ║║       |\\\V///| ║
        //  ║  |\\\V///|    ║║   ║║║║ ║╚══╗    ║║╚╝║║║║║║  ║║  ║╚═╗║║║║   ║╚═╗║╚╝║║╚═╝║╔╝╚╗║╚═╗     |\\\V///| ║
        //  ║  |\\\V///|    ╚╝   ╚╝╚╝ ╚═══╝    ╚╝  ╚╝╚╝╚╝  ╚╝  ╚══╝╚╝╚╝   ╚══╝╚══╝╚═══╝╚══╝╚══╝     |\\\V///| ║
        //  ╚═════════════════════════════════════════════════════════════════════════════════════════════════╝
        private void GetAllOnPlaces()
        {
            Map = new();
            DestroyedTurrets = new();


            var aTop = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.A && s.Position == PositionType.Top).Member);
            aTop.CurrentLocation = Location.TopLine;
            var aJun = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.A && s.Position == PositionType.Jungle).Member);
            aJun.CurrentLocation = Location.Jungle;
            var aMid = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.A && s.Position == PositionType.Mid).Member);
            aMid.CurrentLocation = Location.MidLine;
            var aBot = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.A && s.Position == PositionType.Bot).Member);
            aBot.CurrentLocation = Location.BotLine;
            var aSup = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.A && s.Position == PositionType.Support).Member);
            aSup.CurrentLocation = Location.BotLine;

            var bTop = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.B && s.Position == PositionType.Top).Member);
            bTop.CurrentLocation = Location.TopLine;
            var bJun = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.B && s.Position == PositionType.Jungle).Member);
            bJun.CurrentLocation = Location.Jungle;
            var bMid = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.B && s.Position == PositionType.Mid).Member);
            bMid.CurrentLocation = Location.MidLine;
            var bBot = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.B && s.Position == PositionType.Bot).Member);
            bBot.CurrentLocation = Location.BotLine;
            var bSup = GetPick(CurrentMatch.Seats.First(s => s.Side == MatchSide.B && s.Position == PositionType.Support).Member);
            bSup.CurrentLocation = Location.BotLine;

            aJun.Tension += 20;
            bJun.Tension += 20;
            aSup.Tension += 10;
            bSup.Tension += 10;
            aMid.Tension += 5;
            bMid.Tension += 5;

            Characters = new List<GameChar> { aTop, aJun, aMid, aBot, aSup, bTop, bJun, bMid, bBot, bSup };
        }

        private GameChar GetPick(Member member)
        {
            var type = CharPick(member);

            var result = new GameChar(type)
            {
                MatchSeat = CurrentMatch.Seats.First(s => s.MemberId == member.Id)
            };

            result.SetUsingLevel(GetUsingLevel(member, type));

            return result;
        }

        private GameCharType CharPick(Member member)
        {
            var pickPool = member.CharPool;
            if (pickPool != null && pickPool.Any())
            {
                foreach (var pick in pickPool)
                {
                    if (IsCharIsFree(pick.Type))
                    {
                        return pick.Type;
                    }
                }
            }


            foreach (var type in Enum.GetValues<GameCharType>())
            {
                if (IsCharIsFree(type))
                {
                    SaveNewPick(member, type);

                    return type;
                }
            }

            throw new Exception("Pick was denied!");
        }

        private bool IsCharIsFree(GameCharType type)
        {
            if (Picks.Contains(type))
            {
                return false;
            }
            else
            {
                Picks.Add(type);
                return true;
            }
        }

        private void SaveNewPick(Member member, GameCharType pick)
        {
            var newPick = new GameCharacter
            {
                Type = pick,
                Member = member,
            };

            member.CharPool.Add(newPick);
            _context.GameCharacters.Add(newPick);
            _context.SaveChanges();
        }

        private double GetUsingLevel(Member member, GameCharType pick)
        {
            var exp = member.Experience + 1;
            var expLevel = CalculateExpLevel(exp);

            var expOnChamp = member.CharPool.FirstOrDefault(c => c.Type == pick)?.Experience ?? 1;
            var result = CalculateUsingLevel(expOnChamp, expLevel);

            return result;
        }

        public static double CalculateExpLevel(double exp)
        {
            double maxExpLevel = 0.5;

            double expLevel = maxExpLevel * (1 - Math.Pow(Math.E, -exp));

            return expLevel;
        }

        public static double CalculateUsingLevel(double expOnChamp, double expLevel)
        {
            double minExpLevel = expLevel;
            double maxResult = 1.0;

            double result = minExpLevel + (maxResult - minExpLevel) * (1 - Math.Pow(Math.E, -expOnChamp));

            return result;
        }

        private void UpdateMembersExp()
        {
            var membersToUpdate = new List<Member>();
            var seatsToAdd = new List<MatchSeat>();
            foreach (var champ in Characters)
            {
                var member = champ.MatchSeat.Member;
                var exp = champ.Experience / 1000;
                champ.MatchSeat.GainedExperience = exp;
                AddExp(member, exp);
                seatsToAdd.Add(champ.MatchSeat);
                membersToUpdate.Add(member);
            }
            //_context.MatchSeats.AddRange(seatsToAdd);
            _context.Members.UpdateRange(membersToUpdate);
        }

        private void AddExp(Member member, double exp)
        {
            var expBefore = member.Experience;
            member.Experience += exp;
            if ((expBefore % 1000) < (member.Experience % 1000))
            {
                member.SkillPoints++;
            }
        }


        //  ╔═════════════════════════════════════════════════════════════════════════════════════════════════╗
        //  ║  |\\\V///|            ╔╗  ╔╗╔══╗╔════╗╔══╗╔╗╔╗       ╔╗  ╔══╗╔═══╗╔══╗╔══╗            |\\\V///| ║
        //  ║  |\\\V///|            ║║  ║║║╔╗║╚═╗╔═╝║╔═╝║║║║       ║║  ║╔╗║║╔══╝╚╗╔╝║╔═╝            |\\\V///| ║
        //  ║  |\\\V///|            ║╚╗╔╝║║╚╝║  ║║  ║║  ║╚╝║       ║║  ║║║║║║╔═╗ ║║ ║║              |\\\V///| ║
        //  ║  |\\\V///|            ║╔╗╔╗║║╔╗║  ║║  ║║  ║╔╗║       ║║  ║║║║║║╚╗║ ║║ ║║              |\\\V///| ║
        //  ║  |\\\V///|            ║║╚╝║║║║║║  ║║  ║╚═╗║║║║       ║╚═╗║╚╝║║╚═╝║╔╝╚╗║╚═╗            |\\\V///| ║
        //  ║  |\\\V///|            ╚╝  ╚╝╚╝╚╝  ╚╝  ╚══╝╚╝╚╝       ╚══╝╚══╝╚═══╝╚══╝╚══╝            |\\\V///| ║
        //  ╚═════════════════════════════════════════════════════════════════════════════════════════════════╝
        public MatchModel InitiateMatch(int matchId)
        {
            CurrentMatch = _context.Matches
                .Include(m => m.Teams)
                .Include(m => m.Seats)
                .ThenInclude(s => s.Member)
                .Include(m => m.Events)
                .FirstOrDefault(m => m.Id == matchId)
                ?? throw new Exception("Match wasn't found!");

            if (CurrentMatch.IsItEnded) { throw new Exception("Match is already ended!"); }

            _logger = new(CurrentMatch, Map);

            GetAllOnPlaces();
            StartMatch();

            CurrentMatch.IsItEnded = true;
            CurrentMatch.Date = DateTime.Now;
            CurrentMatch.Winer = Map.LeftAncientNest.Health > 0 ? MatchSide.A : MatchSide.B;
            UpdateMembersExp();

            _context.Matches.Update(CurrentMatch);
            _context.SaveChanges();

            return new MatchModel(CurrentMatch);
        }

        private void StartMatch()
        {
            _logger.MatchStart();

            EarlyGame();

            if (!IsGameIsFinished())
            {
                MidGame();
            }

            if (!IsGameIsFinished())
            {
                LateGame();
            }
        }

        private void EarlyGame()
        {
            int tension = -25;
            var gameFinished = IsGameIsFinished();

            do
            {
                OneSecond(tension);

                tension++;

                gameFinished = IsGameIsFinished();
            } while (DestroyedTurrets.Where(t => t.Type == MapObjectType.Tier1).Count() < 3 && !gameFinished);
        }

        private void MidGame()
        {
            int mapTension = -5;
            do
            {
                OneSecond(mapTension);

                mapTension++;
            } while (DestroyedTurrets.Where(t => t.Type == MapObjectType.Tier3).Count() < 3 && !IsGameIsFinished());
        }

        private void LateGame()
        {
            Map.IsLateGameIsStarted = true;

            int mapTension = 25;
            do
            {
                OneSecond(mapTension);

                mapTension++;
            } while (!IsGameIsFinished());
        }

        private bool IsGameIsFinished() => Map.LeftAncientNest.IsDestroyed || Map.RightAncientNest.IsDestroyed;

        private double GetTension(GameChar champ)
        {
            double powerOfEnemies = 0;
            foreach (var enemy in GetOpponents(champ))
            {
                powerOfEnemies += enemy.GetPower();
            }

            var champPower = champ.GetMentalPowerDecision();
            var champTension = champPower + champPower * 0.5 * GetPartners(champ).Count;

            return champTension - powerOfEnemies;
        }


        //  ┌----------------------┐
        //  |   SCENARIOS OF SEC   |
        //  └----------------------┘

        /// <summary>
        /// CHAMPS CHOISE WHAT TO DO
        /// </summary>
        private void OneSecond(int tension)
        {
            foreach (var champ in Characters)
            {
                champ.IsActed = false;
                champ.Tension++;
            }

            //MAKE DECISION
            foreach (var champ in Characters.Where(c => c.MatchSeat.Position == PositionType.Support && c.MatchSeat.Position == PositionType.Jungle).OrderBy(c => c.Tension))
            {
                MakeDecision(champ, tension);
            }
            //MAKE DECISION
            foreach (var champ in Characters.Where(c => c.MatchSeat.Position != PositionType.Support && c.MatchSeat.Position != PositionType.Jungle).OrderBy(c => c.Tension))
            {
                MakeDecision(champ, tension);
            }

            //ACT
            foreach (var champ in Characters)
            {
                if (!champ.IsActed)
                {
                    switch (champ.CurrentDecision)
                    {
                        case Decision.ToFarm:
                            if (champ.Tension > 0)
                            {
                                TryToFarm(champ);
                            }
                            break;
                        case Decision.ToAttack:
                            if (Map.IsLateGameIsStarted && Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && (c.IsInCombat || c.Tension > 10)))
                            {
                                TryToKill(champ);
                            }
                            else
                            {
                                TryToPush(champ);
                            }
                            break;
                        case Decision.ToGang:
                            if (Map.IsLateGameIsStarted && Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && (c.IsInCombat || c.Tension > 10)))
                            {
                                TryToKill(champ);
                            }
                            else
                            {
                                TryToMove(champ);
                            }
                            break;
                        case Decision.ToSkip:
                            TryToSkip(champ);
                            break;
                    }

                    champ.IsActed = true;
                }
            }

            CurrentMatch.Duration++;
        }

        private void MakeDecision(GameChar champ, int tension)
        {
            if (!champ.IsActed)
            {
                if (champ.IsInCombat)
                {
                    if (champ.CurrentHealth < champ.MaxHealth * 0.1)
                    {
                        champ.CurrentDecision = Decision.ToSkip;
                    }
                    else
                    {
                        if (Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && (c.IsInCombat || c.Tension > 10)))
                        {
                            champ.CurrentDecision = Decision.ToAttack;
                        }
                        else
                        {
                            champ.CurrentDecision = Decision.ToFarm;
                        }
                    }
                }
                else
                {
                    if (champ.CurrentHealth < champ.MaxHealth * aggressionCoefficient || GetTension(champ) + tension < 45)
                    {
                        champ.CurrentDecision = Decision.ToFarm;
                    }
                    else
                    {
                        if (champ.CurrentLocation == Location.Jungle && !Map.IsLateGameIsStarted)
                        {
                            champ.CurrentDecision = Decision.ToGang;
                        }
                        else
                        {
                            if (Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && (c.IsInCombat || c.Tension > 10)))
                            {
                                champ.CurrentDecision = Decision.ToAttack;
                            }
                            else
                            {
                                if (champ.MatchSeat.Position == PositionType.Support && champ.MatchSeat.Position == PositionType.Jungle)
                                {
                                    if (Characters.Any(c => c.MatchSeat.Side != champ.MatchSeat.Side && (c.CurrentLocation == Location.TopLine || c.CurrentLocation == Location.MidLine || c.CurrentLocation == Location.BotLine)))
                                    {
                                        champ.CurrentDecision = Decision.ToGang;
                                    }
                                    else
                                    {
                                        champ.CurrentDecision = Decision.BackToLine;
                                    }
                                }
                                else
                                {
                                    if (IsChampOnHisOwnLocation(champ))
                                    {
                                        champ.CurrentDecision = Decision.ToFarm;
                                    }
                                    else
                                    {
                                        champ.CurrentDecision = Decision.BackToLine;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsChampOnHisOwnLocation(GameChar champ)
        {
            if (Map.IsLateGameIsStarted)
            {
                return true;
            }
            else
            {
                return champ.MatchSeat.Position switch
                {
                    PositionType.Top => champ.CurrentLocation == Location.TopLine,
                    PositionType.Jungle => champ.CurrentLocation == Location.Jungle,
                    PositionType.Mid => champ.CurrentLocation == Location.MidLine,
                    PositionType.Bot => champ.CurrentLocation == Location.BotLine,
                    PositionType.Support => champ.CurrentLocation == Location.BotLine,
                    _ => false,
                };
            }
        }

        private void TryToFarm(GameChar champ)
        {
            if (!Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && c.CurrentDecision == Decision.ToAttack && (c.IsInCombat || c.Tension > 10)))
            {
                var income = maximumGoldIncome * champ.GetPassiveIncomeCoefficient();
                champ.MatchSeat.GainedGold += income;
                champ.AddExp(income);

                if ((champ.CurrentLocation == Location.TopLine || champ.CurrentLocation == Location.MidLine || champ.CurrentLocation == Location.BotLine)
                    && !Characters.Any(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && GetTension(c) >= 20))
                {
                    TryToPush(champ);
                }

                champ.IsActed = true;
            }
            else
            {
                TryToKill(champ);
            }
        }

        private void TryToKill(GameChar champ)
        {
            var location = champ.CurrentLocation;
            var opponents = GetOpponents(champ);
            var partners = GetPartners(champ);
            partners.Add(champ);

            if (opponents.Any())
            {
                if (partners.OrderBy(a => a.Tension).Last().Tension > opponents.OrderBy(a => a.Tension).Last().Tension)
                {
                    foreach (var partner in partners)
                    {
                        partner.ChampMadeAMove();
                        partner.IsInCombat = true;

                        if (partner.Abilities.Any(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0))
                        {
                            partner.UseAllyAbility(partner.Abilities.Where(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0).First(), partners);
                        }
                        else
                        {
                            var target = opponents.OrderBy(c => c.CurrentHealth).FirstOrDefault();
                            if (target is not null)
                            {
                                if (AttackVariation(partner, target, partners))
                                {
                                    opponents.Remove(target);
                                }
                            }
                        }
                    }
                    foreach (var opponent in opponents.Where(c => c.CurrentLocation == location))
                    {
                        opponent.ChampMadeAMove();
                        opponent.IsInCombat = true;

                        if (opponent.Abilities.Any(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0))
                        {
                            opponent.UseAllyAbility(opponent.Abilities.Where(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0).First(), opponents);
                        }
                        else
                        {
                            var target = partners.OrderBy(c => c.CurrentHealth).FirstOrDefault();
                            if (target is not null)
                            {
                                if (AttackVariation(opponent, target, opponents))
                                {
                                    partners.Remove(target);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var opponent in opponents)
                    {
                        opponent.ChampMadeAMove();
                        opponent.IsInCombat = true;

                        if (opponent.Abilities.Any(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0))
                        {
                            opponent.UseAllyAbility(opponent.Abilities.Where(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0).First(), opponents);
                        }
                        else
                        {
                            var target = partners.OrderBy(c => c.CurrentHealth).FirstOrDefault();
                            if (target is not null)
                            {
                                if (AttackVariation(opponent, target, opponents))
                                {
                                    partners.Remove(target);
                                }
                            }
                        }

                    }
                    foreach (var partner in partners.Where(c => c.CurrentLocation == location))
                    {
                        partner.ChampMadeAMove();
                        partner.IsInCombat = true;

                        if (partner.Abilities.Any(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0))
                        {
                            partner.UseAllyAbility(partner.Abilities.Where(a => a.Usability == GameCharAbilitiesUsabilityType.Ally && a.RechargeProgress <= 0).First(), partners);
                        }
                        else
                        {
                            var target = opponents.OrderBy(c => c.CurrentHealth).FirstOrDefault();
                            if (target is not null)
                            {
                                if (AttackVariation(partner, target, partners))
                                {
                                    opponents.Remove(target);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                TryToFarm(champ);
            }
        }

        private bool TryToSkip(GameChar champ)
        {
            champ.ChampMadeAMove();

            var champMobility = champ.GetMobilityPower() + champ.MatchSeat.Member.ReactionSpeed;
            var opponentsMobility = GetOpponents(champ).FirstOrDefault()?.GetMobilityPower() ?? 0 + (GetOpponents(champ)?.Select(a => a.GetMobilityPower())?.Skip(1)?.Sum() ?? 0 / 2);

            if (champMobility < opponentsMobility)
            {
                var selfAbility = champ.Abilities.Where(a => a.DefenseCoefficient < 1 && a.RechargeProgress <= 0).OrderBy(a => a.DefenseCoefficient);
                if (selfAbility.Any())
                {
                    if (selfAbility.First().DefenseCoefficient > 100 )
                    {
                        champ.UseSelfAbility(selfAbility.First());
                        champ.IsInCombat = false;
                        return true;
                    }
                    else
                    {
                        champ.UseSelfAbility(selfAbility.First());
                        return false;
                    }
                }
                else
                {
                    var healAbility = champ.Abilities.Where(a => a.Healing > 0 && a.RechargeProgress <= 0).OrderBy(a => a.Healing);
                    if (healAbility.Any())
                    {
                        champ.UseSelfAbility(healAbility.Last());
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            champ.IsInCombat = false;
            return true;
        }

        private void TryToMove(GameChar champ)
        {
            champ.ChampMadeAMove();

            var directions = GetDirections(champ.MatchSeat.Side);
            var direction = directions[_random.Next(0, directions.Count)];
            champ.CurrentLocation = direction;
            champ.Tension -= 30 / champ.BasicMobility;
        }

        private void TryToPush(GameChar champ)
        {
            if (champ.CurrentLocation is Location.TopLine or Location.MidLine or Location.BotLine)
            {
                var targets = Map.AllObjects.Where(o => o.Side != champ.MatchSeat.Side && o.Health > 0).OrderBy(o => o.Type);
                var target = targets.FirstOrDefault(o => o.Location == champ.CurrentLocation) ?? targets.FirstOrDefault(o => o.Location is null);

                if (target is not null)
                {
                    if (PushingObject(champ, target))
                    {
                        target.IsDestroyed = true;
                        DestroyedTurrets.Add(target);

                        CurrentMatch.Events.Add(new MatchEvent
                        {
                            TimeOfMoment = CurrentMatch.Duration,
                            Type = MatchEventType.ObjectDestruction,
                            ObjectType = target.Type,
                            DestroyerSeatId = champ.MatchSeat.Id,
                            Destroyers = GetPartners(champ).Select(c => c.MatchSeat).ToHashSet(),
                        });
                    }
                    else
                    {
                        if (champ.CurrentHealth <= 0)
                        {
                            CurrentMatch.Events.Add(new MatchEvent
                            {
                                TimeOfMoment = CurrentMatch.Duration,
                                Type = MatchEventType.Kill,
                                ObjectType = target.Type,
                            });

                            champ.GoToBase(CurrentMatch.Duration);
                        }
                    }
                }
            }
            else
            {
                TryToFarm(champ);
            }
        }

        /// <returns>True if target was denied</returns>
        private bool AttackVariation(GameChar attacker, GameChar defender, IEnumerable<GameChar> partners)
        {
            var attackSkills = attacker.Abilities.Where(a => a.Usability == GameCharAbilitiesUsabilityType.Enemy && a.RechargeProgress <= 0);

            if (attackSkills.Any())
            {
                attacker.UseEnemyAbility(attackSkills.First(), defender);
                if (defender.CurrentHealth <= 0)
                {
                    defender.GoToBase(CurrentMatch.Duration);

                    CurrentMatch.Events.Add(new MatchEvent
                    {
                        TimeOfMoment = CurrentMatch.Duration,
                        Type = MatchEventType.Kill,
                        Killer = new MatchEventKill
                        {
                            Seat = attacker.MatchSeat,
                        },
                        Dead = new MatchEventDeath
                        {
                            Seat = defender.MatchSeat,
                        },
                        Assistants = partners.Select(c => new MatchEventAssist
                        {
                            Seat = c.MatchSeat,
                        }).ToHashSet(),
                    });

                    _logger.EventKill(attacker, defender, partners);

                    return true;
                }
                return false;
            }
            else
            {
                attacker.Hit(defender);

                if (defender.CurrentHealth <= 0)
                {
                    defender.GoToBase(CurrentMatch.Duration);

                    CurrentMatch.Events.Add(new MatchEvent
                    {
                        TimeOfMoment = CurrentMatch.Duration,
                        Type = MatchEventType.Kill,
                        Killer = new MatchEventKill
                        {
                            Seat = attacker.MatchSeat,
                        },
                        Dead = new MatchEventDeath
                        {
                            Seat = defender.MatchSeat,
                        },
                        Assistants = partners.Select(c => new MatchEventAssist
                        {
                            Seat = c.MatchSeat,
                        }).ToHashSet(),
                    });

                    _logger.EventKill(attacker, defender, partners);

                    return true;
                }
                return false;
            }
        }

        /// <returns>True if object was destroyed.</returns>
        private bool PushingObject(GameChar champ, MapObject mapObject)
        {
            champ.Hit(mapObject);

            return mapObject.Health < 0;
        }

        private List<Location> GetDirections(MatchSide side) => Map.IsLateGameIsStarted
            ? Characters.Where(c => c.MatchSeat.Side == side).Select(c => c.CurrentLocation).ToList()
            : Characters.Where(c => c.MatchSeat.Side == side && c.CurrentLocation != Location.Jungle).Select(c => c.CurrentLocation).ToList();

        private HashSet<GameChar> GetPartners(GameChar champ) =>
            Characters.Where(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side == champ.MatchSeat.Side && c.CurrentDecision == champ.CurrentDecision && c.Name != champ.Name).ToHashSet();

        private HashSet<GameChar> GetOpponents(GameChar champ) =>
            Characters.Where(c => c.CurrentLocation == champ.CurrentLocation && c.MatchSeat.Side != champ.MatchSeat.Side && c.CurrentDecision == Decision.ToAttack).ToHashSet();
    }
}

using System;
using System.Collections.Generic;
using Board;
using Game;
using Investigators;
using UnityEngine;

namespace Cards
{
    public class EncounterCard 
    {
        public uint ID = 0;
        public NeighborhoodID NeighborhoodID;
        public Dictionary<TileID, Encounter> Encounters;

        public EncounterCard(uint id, NeighborhoodID neighborhoodID, Dictionary<TileID, Encounter> encounters)
        {
            ID = id;
            NeighborhoodID = neighborhoodID;
            Encounters = encounters;
        }

    }
    
    public class Encounter
    {
        public Location Location;
        public Action<bool> Resolution;
        public bool HasClue;
        public Sprite Card;

        public Encounter(Location location, Action<bool> resolution, bool hasClue = false, Sprite card = null)
        {
            Location = location;
            Resolution = resolution;
            HasClue = hasClue;
            Card = card;
        }
    }
    
    public enum ResolutionType 
    {
        Mandatory,
        Choice,
        Optional
    }
    
    public static class EncounterGenerator 
    {
        public static GameManager Game;
        
        public static List<EncounterCard> CreateAoAEventDeck()
        {
            return new()
            {
                CreateAoAEncounterCard1(),
                CreateAoAEncounterCard2(),
                CreateAoAEncounterCard3()
            };
        }
        
        public static List<EncounterCard> CreateDowntownEncounterDeck()
        {
            return new()
            {
            };
        }

        private static EncounterCard CreateAoAEncounterCard1()
        {
            static void Res1(bool isChoicePositive = true)
            {
                if (DoTest(SkillID.Observation) > 0)
                {
                    GainClueFromNeighborhood();
                }
            }
            static void Res2(bool isChoicePositive = true)
            {
                if (DoTest(SkillID.Observation) > 0)
                {
                    GainClueFromNeighborhood();
                }
            }
            static void Res3(bool isChoicePositive)
            {
                Recover(RecoveryType.Physical, 2, false);
                if (isChoicePositive)
                {
                    Spend(ResourceType.Dollars, 1);
                    Recover(RecoveryType.Physical, 2, false);
                    GainClueFromNeighborhood();
                }
            }
            
            var enc1 = new Encounter(new Location(NeighborhoodID.Easttown, TileID.HibbsRoadhouse), Res1, true); 
            var enc2 = new Encounter(new Location(NeighborhoodID.Easttown, TileID.PoliceStation), Res2); 
            var enc3 = new Encounter(new Location(NeighborhoodID.Easttown, TileID.VelmasDiner), Res3);
            var encounters = new Dictionary<TileID, Encounter> {{TileID.HibbsRoadhouse, enc1}, {TileID.PoliceStation, enc2}, {TileID.VelmasDiner, enc3}};
            
            return new EncounterCard(1, NeighborhoodID.Easttown, encounters);
        }
        
        private static EncounterCard CreateAoAEncounterCard2()
        {
            static void Res1(bool isChoicePositive = true)
            {
                if (DoTest(SkillID.Lore) > 0)
                {
                    GainSpell();
                    GainClueFromNeighborhood();
                }
            }
            static void Res2(bool isChoicePositive)
            {
                GainClueFromNeighborhood();
                if (isChoicePositive)
                {
                    ShowItemsDisplay(ItemTypeID.Common, false);
                }
            }
            static void Res3(bool isChoicePositive = true)
            {
                Recover(RecoveryType.Physical, 2, false);
                if (DoTest(SkillID.Strength) > 0)
                {
                    Gain(ResourceType.Dollars, 3);
                    GainClueFromNeighborhood();
                }
            }
            
            var enc1 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.BlackCave), Res1, true); 
            var enc2 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.GeneralStore), Res2); 
            var enc3 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.Graveyard), Res3);
            var encounters = new Dictionary<TileID, Encounter> {{TileID.BlackCave, enc1}, {TileID.GeneralStore, enc2}, {TileID.Graveyard, enc3}};
            
            return new EncounterCard(1, NeighborhoodID.Easttown, encounters);
        }
        
        private static EncounterCard CreateAoAEncounterCard3()
        {
            static void Res1(bool isChoicePositive = true)
            {
                GainClueFromNeighborhood();
                if (DoTest(SkillID.Lore) > 0)
                {
                    GainSpell();
                }
            }
            static void Res2(bool isChoicePositive)
            {
                GainClueFromNeighborhood();
                if (isChoicePositive)
                {
                    ShowItemsDisplay(ItemTypeID.Common, false);
                    // TODO IF BUY GIVE CLUE
                }
            }
            static void Res3(bool isChoicePositive)
            {
                uint result = 0;
                if (isChoicePositive)
                {
                    result = DoTest(SkillID.Strength);
                }
                else
                {
                    BecomeDelayed();
                }
                if (result > 0 || !isChoicePositive)
                {
                    Gain(ResourceType.Dollars, 3);
                    GainClueFromNeighborhood();
                }
            }
            
            var enc1 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.BlackCave), Res1); 
            var enc2 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.GeneralStore), Res2); 
            var enc3 = new Encounter(new Location(NeighborhoodID.Rivertown, TileID.Graveyard), Res3, true);
            var encounters = new Dictionary<TileID, Encounter> {{TileID.BlackCave, enc1}, {TileID.GeneralStore, enc2}, {TileID.Graveyard, enc3}};
            
            return new EncounterCard(1, NeighborhoodID.Easttown, encounters);
        }

        public static void DoNothing()
        {
            throw new NotImplementedException();
        }
        
        public static void Recover(RecoveryType recoveryType, uint amount, bool onlySelf) 
        {
            throw new NotImplementedException();
        }
        
        public static void Spend(ResourceType resourceType, uint amount) 
        {
            throw new NotImplementedException();
        }
        
        public static void Gain(ResourceType resourceType, uint amount) 
        {
            throw new NotImplementedException();
        }
        
        public static uint DoTest(SkillID skillID)
        {
            return Game.currentInvestigator.DoTest(skillID);
        }

        private static void ShowItemsDisplay(ItemTypeID itemTypeAvailable, bool onlyOne, float discount = 1.0f)
        {
            throw new NotImplementedException();
        }

        private static void GainClueFromNeighborhood()
        {
            Game.GainClueFromNeighborhood();
        }
        
        private static void GainSpell()
        {
            throw new NotImplementedException();
        }
        
        private static void BecomeDelayed()
        {
            throw new NotImplementedException();
        }
        
        public enum RecoveryType 
        {
            Physical,
            Sanity
        }
        
        public enum ResourceType 
        {
            Dollars,
            Remnants,
        }
    }
}

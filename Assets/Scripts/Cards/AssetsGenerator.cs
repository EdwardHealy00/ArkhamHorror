using System.Collections;
using System.Collections.Generic;
using Investigators;
using UnityEngine;

namespace Cards
{
    public static class AssetsGenerator 
    {
        public static Dictionary<AssetID, Asset> CreateRookieCopStartPoss()
        {
            return new()
            {
                {AssetID.Becky, CreateBecky()},
                {AssetID.Handcuffs, CreateHandcuffs()},
                {AssetID.Motorcycle, CreateMotorcycle()}
            };
        }
        
        public static Dictionary<AssetID, Asset> CreateSecretaryStartPoss()
        {
            return new()
            {
                {AssetID.KingInYellow, CreateKingInYellow()},
                {AssetID.AnalatycalMind, CreateAnalyticalMind()},
                {AssetID.Synergy, CreateSynergy()}
            };
        }

        private static Asset CreateBecky()
        {
            return new Item(AssetID.Becky, AssetTypeID.Item, ItemTypeID.Weapon, new Health(2, 3), hands: 2);
        }
        private static Asset CreateHandcuffs()
        {
            return new Item(AssetID.Handcuffs, AssetTypeID.Item, ItemTypeID.Common);
        }
        private static Asset CreateMotorcycle()
        {
            return new Item(AssetID.Motorcycle, AssetTypeID.Item, ItemTypeID.Vehicule);
        }
        private static Asset CreateKingInYellow()
        {
            return new Item(AssetID.KingInYellow, AssetTypeID.Item, ItemTypeID.CurioTome);
        }
        private static Asset CreateAnalyticalMind()
        {
            return new Talent(AssetID.KingInYellow, AssetTypeID.Item, TalentTypeID.Innate);
        }
        private static Asset CreateSynergy()
        {
            return new Talent(AssetID.KingInYellow, AssetTypeID.Item, TalentTypeID.Innate);
        }
    }
}


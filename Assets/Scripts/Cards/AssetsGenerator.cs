using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Investigators;
using UnityEngine;

namespace Cards
{
    public static class AssetsGenerator
    {
        public static GameManager Game;
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
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("Becky");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }
            
            return new Item(AssetID.Becky, AssetTypeID.Item, ItemTypeID.Weapon, OnDraw, OnDiscard, new Health(2, 3), hands: 2);
        }
        private static Asset CreateHandcuffs()
        {
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("Handcuffs");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }
            
            return new Item(AssetID.Handcuffs, AssetTypeID.Item, ItemTypeID.Common, OnDraw, OnDiscard);
        }
        private static Asset CreateMotorcycle()
        {
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("Motorcycle");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }

            return new Item(AssetID.Motorcycle, AssetTypeID.Item, ItemTypeID.Vehicule, OnDraw, OnDiscard);
        }
        private static Asset CreateKingInYellow()
        {
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("King In Yellow");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }
            
            return new Item(AssetID.KingInYellow, AssetTypeID.Item, ItemTypeID.CurioTome, OnDraw, OnDiscard);
        }
        private static Asset CreateAnalyticalMind()
        {
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("Analytical Mind");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }
            
            return new Talent(AssetID.KingInYellow, AssetTypeID.Item, TalentTypeID.Innate, OnDraw, OnDiscard);
        }
        private static Asset CreateSynergy()
        {
            void Trigger(object sender, EventArgs e)
            {
                Debug.Log("SYNERGY");
            }
            void OnDraw() { Game.MoveApplied += Trigger; };
            void OnDiscard() { Game.MoveApplied -= Trigger; }
            
            return new Talent(AssetID.KingInYellow, AssetTypeID.Item, TalentTypeID.Innate, OnDraw, OnDiscard);
        }
    }
}


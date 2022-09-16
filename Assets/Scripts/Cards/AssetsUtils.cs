using System;
using System.Collections.Generic;
using Investigators;
using UnityEditor;
using UnityEngine;

namespace Cards
{
    public enum AssetID
    {
        Becky,
        Handcuffs,
        Motorcycle,
        KingInYellow,
        AnalatycalMind,
        Synergy
    }

    public abstract class Asset
    {
        public AssetID AssetID;
        public AssetTypeID AssetTypeID;
    }

    public class Item : Asset
    {
        public ItemTypeID ItemTypeID;
        public Health? Health;
        public int Cost;
        public int Hands;

        public Item(AssetID assetID, AssetTypeID assetTypeID, ItemTypeID itemTypeID, Health? health = null, int cost = 0, int hands = 0)
        {
            AssetID = assetID;
            AssetTypeID = assetTypeID;
            ItemTypeID = itemTypeID;
            Health = health;
            Cost = cost;
            Hands = hands;
        }
    }
    
    public class Talent : Asset
    {
        public TalentTypeID TalentTypeID;

        public Talent(AssetID assetID, AssetTypeID assetTypeID, TalentTypeID talentTypeID)
        {
            AssetID = assetID;
            AssetTypeID = assetTypeID;
            TalentTypeID = talentTypeID;
        }
    }
    
    public class Ally : Asset
    {
        public AllyTypeID AllyTypeID;
        public Health Health;

        public Ally(AssetID assetID, AssetTypeID assetTypeID, AllyTypeID allyTypeID, Health health)
        {
            AssetID = assetID;
            AssetTypeID = assetTypeID;
            AllyTypeID = allyTypeID;
            Health = health;
        }
    }
    
    public class Spell : Asset
    {
        public SpellTypeID SpellTypeID;
        public int Hands;

        public Spell(AssetID assetID, AssetTypeID assetTypeID, SpellTypeID spellTypeID, int hands)
        {
            AssetID = assetID;
            AssetTypeID = assetTypeID;
            SpellTypeID = spellTypeID;
            Hands = hands;
        }
    }
    
    public class Condition : Asset
    {
        public ConditionTypeID ConditionTypeID;
        public int Hands;

        public Condition(AssetID assetID, AssetTypeID assetTypeID, ConditionTypeID conditionTypeID)
        {
            AssetID = assetID;
            AssetTypeID = assetTypeID;
            ConditionTypeID = conditionTypeID;
        }
    }


    public enum AssetTypeID
    {
        Item,
        Ally,
        Spell,
        Condition,
        Talent
    }

    public enum ItemTypeID
    {
        Common,
        Weapon,
        CommonWeapon,
        Vehicule,
        Curio,
        CurioTome,
        MagicalCurio
    }

    public enum SpellTypeID
    {
        Incantation,
        Ritual
    }

    public enum AllyTypeID
    {
        Pugilist,
        AspiringActor,
        MysticBountyHunter,
        Consultant,
        ParanormalDetective,
        PoliceDetective,
        AntiquitiesDealer,
        Secretary,
        Teacher,
        PoliceOfficer,
        UniversityProfessor,
        Dog,
        AsylumPatient,
        CatFamiliar,
        Witch,
        Zealot,
        Cat,
    }

    public enum TalentTypeID
    {
        Innate,
        Retainer,
        Membership
    }

    public enum ConditionTypeID
    {
        None,
        Innate,
    }
}


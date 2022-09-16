using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Cards;
using UnityEditor;
using UnityEngine;

namespace Investigators
{
    public class Investigator
    {
        public InvestigatorID ID;
        public string Name;
        public Health Health;
        public Dictionary<SkillID, Skill> Skills;
        public Dictionary<AssetID, Asset> Assets;
        public int FocusLimit;
        public int FocusAmount => Skills.Sum(skill => skill.Value.TimesFocused);
        public int Dollars;
        public int Remnants;
        public int MoveLimit = 2;
        public int ActionLimit = 2;
        [HideInInspector] public int ActionLeftThisTurn = 2;
        public int FocusLimitPerSkill = 1;
        
        [HideInInspector]public Tile Tile;
        [HideInInspector] public GameObject Pawn;

        public Investigator(InvestigatorID investigatorID, Health health, Dictionary<SkillID, Skill> skills, Dictionary<AssetID, Asset> startingPossessions, int focusLimit, int dollars)
        {
            ID = investigatorID;
            Health = health;
            Skills = skills;
            Assets = startingPossessions;
            FocusLimit = focusLimit;
            Dollars = dollars;
        }

        void FocusSkill(SkillID skillId)
        {
            if (Skills[skillId].TimesFocused < FocusLimitPerSkill)
            {
                Skills[skillId].FocusSkill();
            }
        }
    
        void UnfocusSkill(SkillID skillId)
        {
            if (Skills[skillId].TimesFocused > 0)
            {
                Skills[skillId].UnfocusSkill();
            }
            else
            {
                Debug.LogError("Tried to unfocus unfocused skill");
            }
        }
        public Sprite GetSprite()
        {
            return ID switch
            {
                InvestigatorID.Magician => Resources.LoadAll<Sprite>("Sprites/Characters/4chars")[0],
                InvestigatorID.Astronomer => Resources.LoadAll<Sprite>("Sprites/Characters/4chars")[1],
                InvestigatorID.Mobster => Resources.LoadAll<Sprite>("Sprites/Characters/4chars")[2],
                InvestigatorID.Entertainer => Resources.LoadAll<Sprite>("Sprites/Characters/4chars")[3],
                InvestigatorID.Dilettante => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[0],
                InvestigatorID.Mechanic => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[1],
                InvestigatorID.Reporter => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[2],
                InvestigatorID.RookieCop => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[3],
                InvestigatorID.Haunted => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[4],
                InvestigatorID.Waitress => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[5],
                InvestigatorID.Urchin => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[6],
                InvestigatorID.Secretary => Resources.LoadAll<Sprite>("Sprites/Characters/8chars")[7],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public class Skill
    {
        public int TimesFocused;
        public int Value;
        public SkillID SkillID;

        public Skill(SkillID skillId, int value)
        {
            TimesFocused = 0;
            Value = value;
            SkillID = skillId;
        }

        public void FocusSkill()
        {
            TimesFocused++;
            Value++;
        }
    
        public void UnfocusSkill()
        {
            TimesFocused--;
            Value--;
        }
    }

    public struct Health
    {
        public int Physical;
        public int Sanity;

        public Health(int physical, int sanity)
        {
            Physical = physical;
            Sanity = sanity;
        }
    }
}
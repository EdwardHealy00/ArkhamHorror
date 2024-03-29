using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Board;
using Cards;
using UnityEditor;
using UnityEngine;

namespace Investigators
{
    public class Investigator
    {
        public InvestigatorID ID;
        public string Name => InvestigatorUtils.InvestigatorIDtoName(ID);
        public string Class => InvestigatorUtils.InvestigatorIDtoClass(ID);
        public Health Health;
        public Dictionary<SkillID, Skill> Skills;
        public Dictionary<AssetID, Asset> Assets;
        public uint FocusLimit;
        public uint FocusAmount => (uint) Skills.Sum(skill => skill.Value.TimesFocused);
        public uint Dollars;
        public uint Remnants;
        public uint Clues = 0;
        public uint MoveLimit = 2;
        public uint ActionLimit = 2;
        public uint minRollForSuccess = 5;
        public bool isEngaged = false;
        public bool isDelayed = false;
        [HideInInspector] public uint ActionsLeftThisTurn = 1;
        #region ActionsDoneThisTurn 
        [HideInInspector] public Dictionary<ActionID, bool> ActionsDoneThisTurn = new Dictionary<ActionID, bool>   {{ActionID.Move, false}, {ActionID.GatherResources, false}, {ActionID.Attack, false}, {ActionID.Evade, false}, {ActionID.Research, false}, {ActionID.Ward, false}, {ActionID.Trade, false}, {ActionID.Focus, false}}; 
        #endregion
        public uint FocusLimitPerSkill = 1;
        
        [HideInInspector]public Tile Tile;
        [HideInInspector] public GameObject Pawn;
        
        public event EventHandler MoveApplied;

        public Investigator(InvestigatorID investigatorID, Health health, Dictionary<SkillID, Skill> skills, Dictionary<AssetID, Asset> startingPossessions, uint focusLimit, uint dollars)
        {
            ID = investigatorID;
            Health = health;
            Skills = skills;
            Assets = startingPossessions;
            FocusLimit = focusLimit;
            Dollars = dollars;
        }

        public void MoveTo(Tile tile)
        {
            // Remove investigator from current tile
            Tile.Investigators.Remove(ID);
            var i = 0;
            
            // Offset investigator pawns on current tile since this pawn moves
            foreach (var inv in Tile.Investigators.Values)
            {
                inv.Pawn.transform.position = Tile.CenterPos.position + new Vector3(1 * i, 0, 0);
                i++;
            }
            
            // Move pawn
            tile.Investigators[ID] = this;
            Tile = tile;
            Pawn.transform.position = tile.CenterPos.position + new Vector3(1 * (tile.Investigators.Count - 1), 0, 0);
        }

        public bool CanFocusSkill(SkillID skillId)
        {
            if (FocusAmount >= FocusLimit) return false;
            if (Skills[skillId].TimesFocused < FocusLimitPerSkill) return false;
            return true;
        }

        public void FocusSkill(SkillID skillId)
        {
            if (skillId == SkillID.None) return;
            
            if (Skills[skillId].TimesFocused < FocusLimitPerSkill)
            {
                Skills[skillId].FocusSkill();
            }
        }
    
        public void UnfocusSkill(SkillID skillId)
        {
            if (skillId == SkillID.None) return;
            
            if (Skills[skillId].TimesFocused > 0)
            {
                Skills[skillId].UnfocusSkill();
            }
            else
            {
                Debug.LogError("Tried to unfocus unfocused skill");
            }
        }

        public uint DoTest(SkillID skill)
        {
            uint numberOfSuccesses = 0;
            for(int i = 0; i < Skills[skill].Value; i++)
            {
                var roll = UnityEngine.Random.Range(1, 6);
                Debug.Log(roll);
                if (roll >= minRollForSuccess) numberOfSuccesses++;
            }
            // TODO MANIPULATE DICE 490.4
            return numberOfSuccesses;
        }
        
        public void ResetActionsForNewTurn()
        {
            ActionsLeftThisTurn = ActionLimit;
            foreach (var actionID in ActionsDoneThisTurn.Keys.ToList())
            {
                 ActionsDoneThisTurn[actionID] = false;
            }
        }

        public void InvokeMoveAppliedEvent()
        {
            MoveApplied?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Skill
    {
        public uint TimesFocused;
        public uint Value;
        public SkillID SkillID;

        public Skill(SkillID skillId, uint value)
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
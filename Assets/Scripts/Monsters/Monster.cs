using System;
using Board;
using Investigators;
using UnityEngine;

namespace Monsters
{
    public delegate void ActionRef<T>(ref T item);
    
    public class Monster
    {
        public MonsterID ID;
        public string Name => MonsterUtils.MonsterIDToName(ID);
        public uint Health;
        public int StrengthModifier;
        public int ObservationModifier;
        public TileID StartingSpace;
        public Health Damage;
        public TileID MoveToward;
        public ActionRef<TileID> MoveTowardFunction;
        public uint Speed;
        public MonsterState State;
        public bool IsElusive = false;
        public Action<Investigator> Reward;
        public Action<Investigator> SpecialAbility;
        public Action<TileID> LurkerAbility;
        public uint RemnantsDrop;
        
        [HideInInspector]public Tile Tile;
        [HideInInspector] public GameObject Pawn;

        public Monster(MonsterID id, uint health, int strengthModifier, int observationModifier, TileID startingSpace, Health damage, ActionRef<TileID> moveTowardFunction, uint speed, Action<Investigator> reward, Action<Investigator> specialAbility = null, Action<TileID> lurkerAbility = null, uint remnantsDrop = 0, bool isElusive = false, MonsterState state = MonsterState.Ready)
        {
            ID = id;
            Health = health;
            StrengthModifier = strengthModifier;
            ObservationModifier = observationModifier;
            StartingSpace = startingSpace;
            Damage = damage;
            MoveTowardFunction = moveTowardFunction;
            Speed = speed;
            State = state;
            IsElusive = isElusive;
            Reward = reward;
            SpecialAbility = specialAbility;
            LurkerAbility = lurkerAbility;
            RemnantsDrop = remnantsDrop;
        }
        
        public void Spawn()
        {
            MoveTowardFunction.Invoke(ref MoveToward);
            
        }
        
        public void Activate()
        {
            MoveTowardFunction.Invoke(ref MoveToward);
            
        }

        public void Attack(Investigator investigator)
        {
            investigator.Health.Physical -= Damage.Physical;
            investigator.Health.Sanity -= Damage.Sanity;
        }

        public void Exhaust()
        {
            State = MonsterState.Exhausted;
        }
        
        public void Die()
        {
        }
    }
}
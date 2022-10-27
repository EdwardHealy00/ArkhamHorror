using System;
using System.Collections.Generic;
using Board;
using Cards;
using Investigators;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private BoardManager board;
        [SerializeField] private UIManager canvas;
        [SerializeField] private InvestigatorManager investigatorManager;
        [SerializeField] private DecksManager decksManager;

        public Dictionary<ActionID, Action> ActionTriggers;
        
        public int numberOfPlayers = 2;
        public Dictionary<InvestigatorID, Investigator> investigators;
        public Investigator currentInvestigator;
        public ActionID currentAction = ActionID.None;

        // Start is called before the first frame update
        void Start()
        {
            Screen.SetResolution(2560, 1440, true);
            AssetsGenerator.Game = this;
            EncounterGenerator.Game = this;
            
            investigatorManager.CreateInvestigators();
            FetchStartingInvestigators();
            board.CreateApproachOfAzatothMap();
            board.SpawnInvestigators(investigators);
            ActionTriggers = new Dictionary<ActionID, Action>()
            {
                {ActionID.Move, TriggerMove},
                {ActionID.GatherResources, TriggerGatherResources},
                {ActionID.Focus, TriggerFocus},
                {ActionID.Ward, TriggerWard},
                {ActionID.Attack, TriggerAttack},
                {ActionID.Evade, TriggerEvade},
                {ActionID.Research, TriggerResearch},
                {ActionID.Trade, TriggerTrade}
            };
            canvas.CreateActionPanel(ActionTriggers);
            StartGame();
        }
        
        public void FetchStartingInvestigators()
        {
            investigators = new Dictionary<InvestigatorID, Investigator>
            {
                [InvestigatorID.RookieCop] = investigatorManager.Investigators[InvestigatorID.RookieCop],
                [InvestigatorID.Secretary] = investigatorManager.Investigators[InvestigatorID.Secretary],
            };
            currentInvestigator = investigators[InvestigatorID.RookieCop];
            canvas.CreateInvestigatorsPanel(investigators);
        }
        
        public Investigator GetNextInvestigator()
        {
            foreach (var investigator in investigators.Values)
            {
                if (investigator.ActionsLeftThisTurn != 0)
                {
                    return investigator;
                }
            }
            EndActionPhase(); // All players have played their turn
            return null;
        }

        // Update is called once per frame
        void Update()
        {
        }
    
        #region Phases
        private void StartGame()
        {
            DoActionPhase();
        }

        private void DoActionPhase()
        {
            foreach (var investigator in investigators.Values)
            {
                investigator.ResetActionsForNewTurn();
            }
            canvas.actionPanel.SetActive(true);
            canvas.RefreshAllActionToggles();
        }
        
        private void EndActionPhase()
        {
            canvas.actionPanel.SetActive(false);
            DoMonsterPhase();
        }

        private void DoMonsterPhase()
        {
            //DoEncounterPhase();
        }

        private void DoEncounterPhase()
        {
            DoMythosPhase();
        }

        private void DoMythosPhase()
        {
            DoActionPhase();
        }
        
        #endregion
        
        #region ActionTriggers

        private void TriggerTrade()
        {
            throw new NotImplementedException();
        }

        private void TriggerResearch()
        {
            currentAction = ActionID.Research;
        }

        private void TriggerEvade()
        {
            throw new NotImplementedException();
        }

        private void TriggerAttack()
        {
            throw new NotImplementedException();
        }

        private void TriggerWard()
        {
            currentAction = ActionID.Ward;
        }

        private void TriggerFocus()
        {
            currentAction = ActionID.Focus;
            canvas.focusSkillPopup.Show();
        }

        private void TriggerGatherResources()
        {
            currentAction = ActionID.GatherResources;
        }

        public void TriggerMove()
        {
            currentAction = ActionID.Move;
            board.ShortestPath = board.FindShortestPath(currentInvestigator.Tile);
        }

        #endregion

        public void ApplyAction()
        {
            switch (currentAction)
            {
                case ActionID.Move: ApplyMove();
                    break;
                case ActionID.GatherResources: ApplyGatherResources();
                    break;
                case ActionID.Focus: ApplyFocus();
                    break;
                case ActionID.Ward: ApplyWard();
                    break;
                case ActionID.Research: ApplyResearch();
                    break;
            }
            currentInvestigator.ActionsLeftThisTurn--; 
            canvas.ApplyAction(currentInvestigator);
            currentAction = ActionID.None;
        }

        #region ActionApplied
        
        private void ApplyTrade()
        {
            throw new NotImplementedException();
        }

        private void ApplyResearch()
        {
            var result = currentInvestigator.DoTest(SkillID.Observation);
            if (result > currentInvestigator.Clues)
            {
                board.ClueAmount = currentInvestigator.Clues;
                currentInvestigator.Clues = 0;
                return;
            }
            currentInvestigator.Clues -= result;
            board.ClueAmount += result;
        }

        private void ApplyEvade()
        {
            throw new NotImplementedException();
        }

        private void ApplyAttack()
        {
            throw new NotImplementedException();
        }

        private void ApplyWard()
        {
            var result = currentInvestigator.DoTest(SkillID.Lore);
            var tile = currentInvestigator.Tile;
            if (result > tile.DoomAmount)
            {
                tile.DoomAmount = 0;
                return;
            }
            tile.DoomAmount -= result;
        }

        private void ApplyFocus()
        {
            var selectedPlusID = canvas.focusSkillPopup.SelectedPlusID;
            var selectedMinusID = canvas.focusSkillPopup.SelectedMinusID;
            
            if(selectedPlusID != SkillID.None)
                currentInvestigator.FocusSkill(canvas.focusSkillPopup.SelectedPlusID);
            
            if(selectedMinusID != SkillID.None)
                currentInvestigator.UnfocusSkill(canvas.focusSkillPopup.SelectedMinusID);
            
            canvas.RefreshInvestigatorPanel(currentInvestigator);
        }
        private void ApplyMove()
        {
            board.MoveInvestigator();
            currentInvestigator.InvokeMoveAppliedEvent();
        }

        private void ApplyGatherResources()
        {
            currentInvestigator.Dollars++;
            canvas.RefreshInvestigatorPanel(currentInvestigator);
        }
        
        #endregion

        public void GainClueFromNeighborhood()
        {
            currentInvestigator.Clues++;
            board.Neighborhoods[currentInvestigator.Tile.Location.NeighborhoodID].ClueAmount--;
            canvas.RefreshInvestigatorPanel(currentInvestigator);
        }
    }
}

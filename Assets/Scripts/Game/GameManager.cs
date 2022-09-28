using System;
using System.Collections.Generic;
using Investigators;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private BoardManager board;
        [SerializeField] private UIManager canvas;
        [SerializeField] private InvestigatorManager investigatorManager;


        public Dictionary<ActionID, Action> ActionTriggers;
        
        public int numberOfPlayers = 2;
        public Dictionary<InvestigatorID, Investigator> investigators;
        public Investigator currentInvestigator;
        public ActionID currentAction = ActionID.None;

        // Start is called before the first frame update
        void Start()
        {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                case ActionID.None: return;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void ApplyFocus()
        {
            currentInvestigator.FocusSkill(InvestigatorUtils.SkillNameToID(canvas.focusSkillPopup.selectedToggle.name));
            canvas.RefreshInvestigatorPanel(currentInvestigator);
        }
        private void ApplyMove()
        {
            board.MoveActionInvestigator();
        }

        private void ApplyGatherResources()
        {
            currentInvestigator.Dollars++;
            canvas.RefreshInvestigatorPanel(currentInvestigator);
        }
        
        #endregion
    }
}

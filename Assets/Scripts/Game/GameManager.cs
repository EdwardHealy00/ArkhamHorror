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

            canvas.UpdateInvestigatorsPanel(investigators);
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
            canvas.actionPanel.SetActive(true);
            //DoMonsterPhase();
        }

        private void DoMonsterPhase()
        {
            DoEncounterPhase();
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
            throw new NotImplementedException();
        }

        private void TriggerGatherResources()
        {
            throw new NotImplementedException();
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
                case ActionID.Move: board.MoveActionInvestigator();
                    break;
            }

            currentAction = ActionID.None;
        }
    }
}

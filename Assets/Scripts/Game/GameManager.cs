using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameObject board;
        [SerializeField] private GameObject canvas;
        private BoardManager _boardManager;
        private UIManager _uiManager;
        
        public Dictionary<ActionID, Action> ActionTriggers;

        // Start is called before the first frame update
        void Start()
        {
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
            _boardManager = board.GetComponent<BoardManager>();
            _uiManager = canvas.GetComponent<UIManager>();
            _uiManager.CreateActionPanel(ActionTriggers);
            StartGame();
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
            _uiManager.actionPanel.SetActive(true);
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

        private void TriggerMove()
        {
            _boardManager.inMoveAction = true;
        }

        #endregion
    }
}

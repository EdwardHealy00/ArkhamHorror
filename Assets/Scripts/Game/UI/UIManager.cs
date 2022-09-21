using System;
using System.Collections.Generic;
using Investigators;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [SerializeField] private ToggleGroup actionGroup;
        [SerializeField] private ToggleGroup portraitGroup;
        [SerializeField] private Toggle actionPrefab;
        [SerializeField] private Toggle portraitPrefab;
        public GameObject actionPanel;
        public GameObject investigatorsPanel; 
        
        // Start is called before the first frame update
        void Start()
        {
        }

        public void CreateActionPanel(Dictionary<ActionID, Action> actionTriggers)
        {
            foreach (var actionId in actionTriggers.Keys)
            {
                var toggle = Instantiate(actionPrefab, actionGroup.transform);
                toggle.GetComponentInChildren<Text>().text = ActionUtils.EnumToString(actionId);
                toggle.group = actionGroup;
                toggle.onValueChanged.AddListener((value) => TriggerAction(actionTriggers, actionId, value));
            }
        }

        public void TriggerAction(Dictionary<ActionID, Action> actionTriggers, ActionID actionId, bool value)
        {
            if (!value) return;
            actionTriggers[actionId].Invoke();
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void UpdateInvestigatorsPanel(Dictionary<InvestigatorID, Investigator> investigators)
        {
            foreach (var investigator in investigators.Values)
            {
                var toggle = Instantiate(portraitPrefab, portraitGroup.transform);
                toggle.GetComponentInChildren<Image>().sprite = investigator.GetSprite();
                toggle.group = portraitGroup;
                toggle.onValueChanged.AddListener((value) => TriggerInvestigatorChange(investigator, value));
            }
        }

        private void TriggerInvestigatorChange(Investigator investigator, bool value)
        {
            if (!value) return;
            game.currentInvestigator = investigator;
        }
    }
}

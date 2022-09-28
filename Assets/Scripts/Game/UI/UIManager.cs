using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
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
        public Dictionary<ActionID, Toggle> actionToggles = new Dictionary<ActionID, Toggle>();
        public Dictionary<InvestigatorID, Toggle> portaitToggles = new Dictionary<InvestigatorID, Toggle>();       
        public GameObject actionPanel;
        public GameObject investigatorsPanel;
        public GameObject investigatorPanel;
        public FocusSkillPopup focusSkillPopup;
        
        private TMP_Text Name;
        private TMP_Text Class;
        private TMP_Text Clues;
        private TMP_Text Health;
        private TMP_Text Sanity;
        private TMP_Text Dollars;
        private TMP_Text Remnants;
        private TMP_Text[] Skills;
        
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
                actionToggles[actionId] = toggle;
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


        public void CreateInvestigatorsPanel(Dictionary<InvestigatorID, Investigator> investigators)
        {
            foreach (var investigator in investigators.Values)
            {
                var toggle = Instantiate(portraitPrefab, portraitGroup.transform);
                toggle.GetComponentInChildren<Image>().sprite = investigator.GetSprite();
                toggle.group = portraitGroup;
                toggle.onValueChanged.AddListener((value) => TriggerInvestigatorChange(investigator, value));
                portaitToggles[investigator.ID] = toggle;
            }
            RefreshInvestigatorPanel(game.currentInvestigator);
        }

        private void TriggerInvestigatorChange(Investigator investigator, bool value)
        {
            if (!value) return;
            if (game.currentInvestigator.ActionsLeftThisTurn != game.currentInvestigator.ActionLimit) 
                return; //Can't change play order if one action has been done
            game.currentInvestigator = investigator;
            ResetAllToggles(actionToggles.Values.ToList());
            RefreshInvestigatorPanel(game.currentInvestigator);
        }

        public void RefreshInvestigatorPanel(Investigator investigator)
        {
            if (Name == null) Name = investigatorPanel.FindInChildren("Name").GetComponent<TMP_Text>();
            if (Class == null) Class = investigatorPanel.FindInChildren("Class").GetComponent<TMP_Text>();
            if (Health == null) Health = investigatorPanel.FindInChildren("Health").GetComponent<TMP_Text>();
            if (Sanity == null) Sanity = investigatorPanel.FindInChildren("Sanity").GetComponent<TMP_Text>();
            if (Dollars == null) Dollars = investigatorPanel.FindInChildren("Dollars").GetComponent<TMP_Text>();
            if (Remnants == null) Remnants = investigatorPanel.FindInChildren("Remnants").GetComponent<TMP_Text>();
            if (Clues == null) Clues = investigatorPanel.FindInChildren("Clues").GetComponent<TMP_Text>();
            if (Skills == null) Skills = investigatorPanel.FindInChildren("Skills").GetComponentsInChildren<TMP_Text>();

            Name.text = investigator.Name;
            Class.text = investigator.Class;
            Health.text = investigator.Health.Physical.ToString();
            Sanity.text = investigator.Health.Sanity.ToString();
            Dollars.text = investigator.Dollars.ToString();
            Remnants.text = investigator.Remnants.ToString();
            Clues.text = investigator.Clues.ToString();
            
            Skills[0].text = investigator.Skills[SkillID.Lore].Value.ToString();
            Skills[1].text = investigator.Skills[SkillID.Influence].Value.ToString();
            Skills[2].text = investigator.Skills[SkillID.Observation].Value.ToString();
            Skills[3].text = investigator.Skills[SkillID.Strength].Value.ToString();
            Skills[4].text = investigator.Skills[SkillID.Will].Value.ToString();
        }

        private void ResetAllToggles(List<Toggle> toggles)
        {
            actionGroup.SetAllTogglesOff();
            foreach (var toggle in toggles)
            {
                toggle.interactable = true;
            } 
        }

        public void ApplyAction(Investigator currentInvestigator)
        {
            if (currentInvestigator.ActionsLeftThisTurn == 0)
            {
                portaitToggles[currentInvestigator.ID].interactable = false;
                portaitToggles[currentInvestigator.ID].isOn = false;
                var nextInvestigator = game.GetNextInvestigator();
                if (nextInvestigator != null)
                {
                    game.currentInvestigator = nextInvestigator;
                    portaitToggles[nextInvestigator.ID].isOn = true;
                    
                }
                ResetAllToggles(actionToggles.Values.ToList());
                return;
            }
            actionToggles[game.currentAction].interactable = false;
        }
    }
}

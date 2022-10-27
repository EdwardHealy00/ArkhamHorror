using System;
using System.Collections.Generic;
using System.Linq;
using Investigators;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class FocusSkillPopup : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [SerializeField] private Button submitBtn;
        [SerializeField] private ToggleGroup plusToggles;
        [SerializeField] private ToggleGroup minusToggles;
        [SerializeField] private TMP_Text focusLimitLabel;
        
        [SerializeField] private TMP_Text lore;
        [SerializeField] private TMP_Text influence;
        [SerializeField] private TMP_Text observation;
        [SerializeField] private TMP_Text strength;
        [SerializeField] private TMP_Text will;

        private uint focusLimit = 1;
        private uint focusLimitPerSkill = 2;
        private uint focusAmount = 0;
        private Dictionary<SkillID, Skill> stats;
        private bool initialized = false;
        
        [HideInInspector] public SkillID SelectedPlusID = SkillID.None;
        [HideInInspector] public SkillID SelectedMinusID = SkillID.None;

        public Dictionary<SkillID, SkillElement> Skills;

        public void Initialize()
        { 
            Skills = new Dictionary<SkillID, SkillElement> 
           {
             {SkillID.Lore, SkillElement.CreateSkillElement(lore)},
             {SkillID.Influence, SkillElement.CreateSkillElement(influence)},
             {SkillID.Observation, SkillElement.CreateSkillElement(observation)},
             {SkillID.Strength, SkillElement.CreateSkillElement(strength)},
             {SkillID.Will, SkillElement.CreateSkillElement(will)},
           };
            
            SelectedPlusID = SkillID.None;
            SelectedMinusID = SkillID.None;

            initialized = true;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            if(!initialized) Initialize();
            RefreshSkillValues();
            focusLimitLabel.text = focusAmount + "/" + focusLimit;
            if (focusAmount <= focusLimit)
            {
                DisableMinusToggles(); 
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnPlusToggleChanged(bool enable)
        {
            if (enable)
            {
                if (SelectedPlusID != SkillID.None)
                {
                    Skills[SelectedPlusID].Value.text = (game.currentInvestigator.Skills[SelectedPlusID].Value).ToString();
                } else focusAmount++;
                foreach (var (key, value) in Skills)
                {
                    if (value.Plus.isOn)
                    {
                        SelectedPlusID = key;
                        Skills[SelectedPlusID].Value.text = (game.currentInvestigator.Skills[SelectedPlusID].Value + 1).ToString();
                    }
                }
            }
            else
            {
                if (SelectedPlusID == SkillID.None) return;
                Skills[SelectedPlusID].Value.text = (game.currentInvestigator.Skills[SelectedPlusID].Value).ToString();
                SelectedPlusID = SkillID.None;
                focusAmount--;
            }
            
            RefreshToggleChanged();
        }
        
        public void OnMinusToggleChanged(bool enable)
        {
            if (enable)
            {
                if (SelectedMinusID != SkillID.None)
                {
                    Skills[SelectedMinusID].Value.text = (game.currentInvestigator.Skills[SelectedMinusID].Value).ToString();
                } else focusAmount--;
                foreach (var (key, value) in Skills)
                {
                    if (value.Minus.isOn)
                    {
                        SelectedMinusID = key;
                        Skills[SelectedMinusID].Plus.interactable = false; // Can't add to skill if minus toggle on
                        Skills[SelectedMinusID].Value.text = (game.currentInvestigator.Skills[SelectedMinusID].Value - 1).ToString();
                    }
                }
            }
            else
            {
                if (SelectedMinusID == SkillID.None) return;
                Skills[SelectedMinusID].Plus.interactable = true; // Reenable plus toggle since minus toggle is off
                Skills[SelectedMinusID].Value.text = (game.currentInvestigator.Skills[SelectedMinusID].Value).ToString();
                SelectedMinusID = SkillID.None;
                focusAmount++;
            }
            
            RefreshToggleChanged();
        }
        public void RefreshToggleChanged()
        {
            var isPlusSelected = plusToggles.AnyTogglesOn();
            var isMinusSelected = minusToggles.AnyTogglesOn();

            if (!isPlusSelected) SelectedPlusID = SkillID.None;
            if (!isMinusSelected) SelectedMinusID = SkillID.None;

            if (focusAmount <= focusLimit)
            {
                submitBtn.interactable = isPlusSelected;
                focusLimitLabel.color = Color.white;
                if (!isMinusSelected) DisableMinusToggles();
            }
            else
            {
                submitBtn.interactable = false;
                focusLimitLabel.color = Color.red;
                EnableMinusToggles();
            }
            focusLimitLabel.text = focusAmount + "/" + focusLimit;
        }

        public void Submit()
        {
            if (SelectedPlusID == SkillID.None) return;
            game.ApplyAction();
            Hide();
        }

        private void DisableMinusToggles()
        {
            foreach (var skill in Skills.Values)
            {
                skill.Minus.interactable = false;
            }
        }
        
        private void EnableMinusToggles()
        {
            foreach (var (key, value) in Skills)
            {
                if (game.currentInvestigator.Skills[key].TimesFocused > 0 && !Skills[key].Plus.isOn) // Skill must be focused but not toggled right now
                {
                    value.Minus.interactable = true;
                }
            }
        }

        private void RefreshSkillValues()
        {
            var investigator = game.currentInvestigator;
            foreach (var skill in Skills)
            {
                skill.Value.Value.text = investigator.Skills[skill.Key].Value.ToString();
                skill.Value.Plus.interactable = investigator.Skills[skill.Key].TimesFocused < investigator.FocusLimitPerSkill;
                skill.Value.Minus.interactable = investigator.Skills[skill.Key].TimesFocused > 0 && focusAmount > focusLimit;
            }
            
            plusToggles.SetAllTogglesOff();
            minusToggles.SetAllTogglesOff();

            focusAmount = investigator.FocusAmount;
            focusLimit = investigator.FocusLimit;
            focusLimitPerSkill = investigator.FocusLimitPerSkill;
        }

        public class SkillElement
        {
            [HideInInspector] public Toggle Plus;
            [HideInInspector] public Toggle Minus;
            [HideInInspector] public TMP_Text Value;
            
            public SkillElement(Toggle plus, Toggle minus, TMP_Text value)
            {
                Plus = plus;
                Minus = minus;
                Value = value;
            }

            public static SkillElement CreateSkillElement(TMP_Text skill)
            {
                return new SkillElement(skill.gameObject.FindInChildren("Plus").GetComponent<Toggle>(),
                    skill.gameObject.FindInChildren("Minus").GetComponent<Toggle>(),
                    skill.gameObject.FindInChildren("Stat").GetComponent<TMP_Text>());
            }
        }
    }
}

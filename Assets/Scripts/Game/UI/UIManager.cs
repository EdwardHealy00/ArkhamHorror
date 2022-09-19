using System;
using System.Collections.Generic;
using Investigators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Button portraitPrefab;
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
                var button = Instantiate(buttonPrefab, actionPanel.transform);
                button.GetComponentInChildren<TMP_Text>().text = ActionUtils.EnumToString(actionId);
                button.onClick.AddListener(() =>  actionTriggers[actionId].Invoke());
            }
        }

        public void FooOnClick()
        {
            Debug.Log("Ta-Da!");
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void UpdateInvestigatorsPanel(Dictionary<InvestigatorID, Investigator> investigators)
        {
            foreach (var investigator in investigators.Values)
            {
                var button = Instantiate(portraitPrefab, investigatorsPanel.transform);
                button.GetComponentInChildren<Image>().sprite = investigator.GetSprite();
                button.onClick.AddListener(() => game.currentInvestigator = investigator);
            }
        }
    }
}

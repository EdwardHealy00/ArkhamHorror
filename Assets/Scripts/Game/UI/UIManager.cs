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
        [SerializeField] private Button buttonPrefab;
        public GameObject actionPanel;
        
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
    
    
    }
}

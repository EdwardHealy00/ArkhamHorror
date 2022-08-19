using Investigators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject actionPanel;
        // Start is called before the first frame update
        void Start()
        {
            CreateActionPanel();
        }

        private void CreateActionPanel()
        {
            foreach (var actionId in BoardManager.ActionTriggers.Keys)
            {
                var button = Instantiate(buttonPrefab, actionPanel.transform);
                button.GetComponentInChildren<TMP_Text>().text = ActionUtils.EnumToString(actionId);
                button.onClick.AddListener(() =>  BoardManager.ActionTriggers[actionId].Invoke());
            }
        }

        private void FooOnClick()
        {
            Debug.Log("Ta-Da!");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
    
    }
}

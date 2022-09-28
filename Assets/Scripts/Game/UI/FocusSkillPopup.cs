using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class FocusSkillPopup : MonoBehaviour
    {
        [SerializeField] private GameManager game;
        [SerializeField] private Button submitBtn;
        [SerializeField] private ToggleGroup skillToggles;
        [HideInInspector] public Toggle selectedToggle;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void RefreshSubmitBtn()
        {
            submitBtn.interactable = skillToggles.AnyTogglesOn();
            foreach (var toggle in skillToggles.ActiveToggles())
            {
                if (toggle.isOn)
                {
                    selectedToggle = toggle;
                }
            }
        }

        public void Submit()
        {
            if (selectedToggle == null) return;
            game.ApplyAction();
            Hide();
        }
    }
}

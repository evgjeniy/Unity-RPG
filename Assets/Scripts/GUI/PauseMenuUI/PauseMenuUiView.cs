using UnityEngine;

namespace GUI.PauseMenuUI
{
    public class PauseMenuUiView : MonoBehaviour
    {
        public GameObject background;
        public GameObject continueButton;
        public GameObject menuButton;
        
        public void SetActive(bool active)
        {
            background.SetActive(active);
            continueButton.SetActive(active);
            menuButton.SetActive(active);
        }
    }
}
using UnityEngine;

namespace GUI.PauseMenuUI
{
    [System.Serializable]
    public class PauseMenuUiController
    {
        public PauseMenuUiView View;
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            View.SetActive(true);
            Cursor.visible = true;
            
            Time.timeScale = 0.0f;
            IsPaused = true;
        }

        public void Continue()
        {
            View.SetActive(false);
            Cursor.visible = false;

            Time.timeScale = 1.0f;
            IsPaused = false;
        }
    }
}
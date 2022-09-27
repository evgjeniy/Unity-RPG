using GUI.DescriptionHUD;
using GUI.EquipmentHUD;
using GUI.HealthBarHUD;
using GUI.InventoryHUD;
using GUI.PauseMenuUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class GuiHandler : MonoBehaviour
    {
        public InventoryController inventory;
        public DescriptionHudController descriptionHud;
        public EquipmentHudController equipment;
        public HealthBarHudController healthBar;
        public PauseMenuUiController pauseMenu;
        public GameObject deathScreen;
        
        void Update()
        {
            if (Input.GetButtonDown("Inventory&Equipment"))
            {
                inventory.OnInventoryButtonClick();
                equipment.OnEquipmentButtonClick();
                Cursor.visible = inventory.View.itemsParent.activeSelf;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.IsPaused)
                    pauseMenu.Continue();
                else
                    pauseMenu.Pause();
            }
        }
        
        public void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1.0f;
        }
        
        public void OnContinueButtonClick() => pauseMenu.Continue();
        public void OnMenuButtonClick() => SceneManager.LoadScene(0);
    }
}
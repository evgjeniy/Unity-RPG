using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MainMenuHandler : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void OnExitFromGameButtonClick() => Application.Quit();
}

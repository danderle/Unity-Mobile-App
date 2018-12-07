using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject PauseMenu;

    private void Start()
    {
        PauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }
    /// <summary>
    /// To the Main menu scene.
    /// </summary>
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

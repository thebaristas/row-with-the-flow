using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ControlesMenu;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu() {
        MainMenu.SetActive(true);
        ControlesMenu.SetActive(false);
    }

    public void ShowControlsMenu() {
        MainMenu.SetActive(false);
        ControlesMenu.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame() {
        Application.Quit();
    }
}

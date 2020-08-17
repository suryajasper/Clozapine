using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ExitMainMenu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OpenMainMenu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        ExitMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.GetChild(0).gameObject.activeSelf)
                ExitMainMenu();
            else
                OpenMainMenu();
        }
    }
}

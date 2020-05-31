using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    bool paused = false;
    GameObject ui;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                ui = Instantiate(pauseMenuUI);
                paused = true;
            }
            else
            {
                ui.BroadcastMessage("Resume");
                paused = false;
            }
        }
    }
}

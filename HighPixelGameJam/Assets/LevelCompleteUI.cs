using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUI : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(gameManager.nextLevel);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}

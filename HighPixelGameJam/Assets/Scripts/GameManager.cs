using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public string nextLevel = "Level2";
    public int levelToUnlock = 2;
    public int par;
    public int targetTime;
    public int targetSwitches;
    public GameObject golfBall;
    public GameObject levelCompleteUI;

    public Sprite filledStar;
    public Sprite unfilledStar;

    void Start()
    {
        //Make sure there is an event system so the buttons work
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (golfBall.transform.position.y <= -5)
                golfBall.SendMessage("ResetBall");
                    
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void FinishedLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        GameObject ui = Instantiate(levelCompleteUI);
        //Gets all UI objects
        GameObject bg = ui.transform.GetChild(0).gameObject;
        GameObject titleText = bg.transform.GetChild(0).gameObject;
        GameObject star1 = bg.transform.GetChild(1).GetChild(0).gameObject;
        GameObject star2 = bg.transform.GetChild(1).GetChild(1).gameObject;
        GameObject star3 = bg.transform.GetChild(1).GetChild(2).gameObject;
        GameObject scoreText = bg.transform.GetChild(2).gameObject;
        GameObject strokeText = bg.transform.GetChild(3).gameObject;
        GameObject parText = bg.transform.GetChild(4).gameObject;
        GameObject timeText = bg.transform.GetChild(5).gameObject;

        titleText.GetComponent<Text>().text = "Level " + (levelToUnlock-1) + " Completed";
        //Variables
        int strokeCount = golfBall.GetComponent<HittyBall>().strokeCount;
        int scoreDifference = strokeCount - par;
        string golfScore = "Par";
        float levelTime = golfBall.GetComponent<HittyBall>().levelTime;
        int worldSwitches = golfBall.GetComponent<HittyBall>().worldSwitches;

        //Golf score logic
        if (strokeCount == 1)
            golfScore = "Hole in one!";
        else if (scoreDifference < -4)
            golfScore = -scoreDifference + "under par";
        else if (scoreDifference == -4)
            golfScore = "Condor";
        else if (scoreDifference == -3)
            golfScore = "Albatross";
        else if (scoreDifference == -2)
            golfScore = "Eagle";
        else if (scoreDifference == -1)
            golfScore = "Birdie";
        else if (scoreDifference == 0)
            golfScore = "Par";
        else if (scoreDifference == 1)
            golfScore = "Bogey";
        else if (scoreDifference == 2)
            golfScore = "Double Bogey";
        else if (scoreDifference == 3)
            golfScore = "Triple Bogey";
        else
            golfScore = scoreDifference + " over par";

        //Star Logic
        if (strokeCount <= par)
        {
            star1.GetComponent<Image>().sprite = filledStar;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star1", 1);
        }
        else
        {
            star1.GetComponent<Image>().sprite = unfilledStar;
        }

        if (levelTime <= targetTime)
        {
            star2.GetComponent<Image>().sprite = filledStar;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star2", 1);
        }
        else
        {
            star2.GetComponent<Image>().sprite = unfilledStar;
        }

        if (worldSwitches <= targetSwitches)
        {
            star3.GetComponent<Image>().sprite = filledStar;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star3", 1);
        }
        else
        {
            star3.GetComponent<Image>().sprite = unfilledStar;
        }

        //Update text
        star1.transform.GetChild(0).GetComponent<Text>().text = "Target Par:\n" + par;
        star2.transform.GetChild(0).GetComponent<Text>().text = "Target Time:\n" + targetTime;
        star3.transform.GetChild(0).GetComponent<Text>().text = "Target Switches:\n" + targetTime;
        scoreText.GetComponent<Text>().text = golfScore;
        strokeText.GetComponent<Text>().text = "Strokes: " + strokeCount;
        parText.GetComponent<Text>().text = "Par: " + par;
        timeText.GetComponent<Text>().text = "Time: " + (int)levelTime;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public int tutorialTextTime;
    public GameObject golfBall;

    Text obstacleText;
    bool stationary;
    int worldChangeCounter;
    int worldChangeDelay;
    int tutorialCounter = 0;

    void Start()
    {
        obstacleText = transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        stationary = golfBall.GetComponent<HittyBall>().stationary;
        worldChangeCounter = golfBall.GetComponent<HittyBall>().worldChangeCounter;
        worldChangeDelay = golfBall.GetComponent<HittyBall>().worldChangeDelay;

        if (Input.GetKeyDown("q") && stationary && tutorialCounter == 1)
        {
            obstacleText.CrossFadeAlpha(1, 1, true);
            obstacleText.text = "Well done, now finish the hole. You can restart in the pause menu if needed.";
            tutorialCounter = 2;
            StartCoroutine(WaitThenFade());
        }
    }

    IEnumerator WaitThenFade()
    {
        yield return new WaitForSeconds(tutorialTextTime);
        obstacleText.CrossFadeAlpha(0, 3, true);
    }

    void Trigger1Text()
    {
        if (tutorialCounter == 0)
        {
            obstacleText.text = "There is an obstacle in your way! Press Q while stationary to change to a parallel world, maybe it will be clear there.";
            tutorialCounter = 1;
        }
    }
}

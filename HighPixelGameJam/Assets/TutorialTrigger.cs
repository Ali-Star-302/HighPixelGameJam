using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialUI;

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Golf Ball")
        {
            tutorialUI.SendMessage("Trigger1Text");
        }
    }
}

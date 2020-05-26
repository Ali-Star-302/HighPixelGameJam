using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ball")
        {
            Debug.Log("Ball in hole");
            gameManager.FinishedLevel();
        }
    }
}

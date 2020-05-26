using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallLayer : MonoBehaviour
{
    public int LayerOnEnter; //BallInHole Layer
    public int LayerOnExit; //Default Layer

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            col.gameObject.layer = LayerOnEnter;
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ball")
        {
            col.gameObject.layer = LayerOnExit;
            Debug.Log("Exited");
        }
    }
}

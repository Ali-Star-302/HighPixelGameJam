using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Vector3 lastPosition = Vector3.zero;

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Debug.Log(lastPosition);
            transform.position = lastPosition;
        }
    }
}

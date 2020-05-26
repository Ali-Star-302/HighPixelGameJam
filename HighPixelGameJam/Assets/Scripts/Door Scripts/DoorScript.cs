using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool doorState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorState)
        {
            Debug.Log("Door begone");
            this.gameObject.SetActive(false);
        }
    }
}

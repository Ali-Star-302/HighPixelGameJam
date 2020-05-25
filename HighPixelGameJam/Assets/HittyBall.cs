using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittyBall : MonoBehaviour
{
    Quaternion originalRotation;
    public Text powerText;
    int power = 0;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetButton("Jump"))
        {
            power += 1;
            Debug.Log("Pressed");
        }else if (Input.GetButtonUp("Jump"))
        {
            HitBall();
            Debug.Log("Unpressed");
        }

        powerText.text = "Power: " + power;
    }

    void HitBall()
    {
        transform.rotation = originalRotation;
        transform.GetComponent<Rigidbody>().AddRelativeForce(-transform.forward * ((int)(power/100)),ForceMode.Impulse);
        power = 0;
    }
}

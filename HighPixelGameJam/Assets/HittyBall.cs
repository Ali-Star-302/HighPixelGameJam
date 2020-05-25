using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittyBall : MonoBehaviour
{
    Quaternion originalRotation;
    public Text powerText;
    int power = 0;
    public LayerMask groundLayer;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (power >= 100)
        {
            power = 100;
        }
    }

    void HitBall()
    {
        transform.rotation = originalRotation;
        rb.AddRelativeForce(-transform.forward * power,ForceMode.Impulse);
        power = 0;
    }

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == groundLayer)
        {
            rb.drag = 3;
        }
        else
        {
            rb.drag = 0;
        }
    }*/

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == groundLayer)
        {
            rb.drag = 3;
            Debug.Log("");
        }
        else
        {
            rb.drag = 0;
        }
    }
}

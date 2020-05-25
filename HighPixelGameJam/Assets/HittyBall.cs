﻿using System.Collections;
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
        Physics.gravity *= 2;
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 2, 0);
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -2, 0);
        }

        if (Input.GetButton("Jump"))
        {
            power += 1;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            HitBall();
        }

        powerText.text = "Power: " + power;

        if (power >= 100)
        {
            power = 99;
        }

        if (Physics.Raycast(transform.position,Vector3.down, 1f, groundLayer))
        {
            rb.drag = 0.1f;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    void HitBall()
    {
        transform.rotation = originalRotation;
        rb.AddRelativeForce(transform.forward * ((int)(power*1.2)),ForceMode.Impulse);
        Debug.Log(((int)(power * 1.2)));
        power = 0;
    }
}

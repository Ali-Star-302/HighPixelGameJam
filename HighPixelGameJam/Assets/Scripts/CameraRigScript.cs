using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigScript : MonoBehaviour
{
    public GameObject Ball;
    public Camera cam;

    public float rotationSpeed = 2f;
    private Vector3 offset;
    public Vector3 baseOffset = new Vector3(0, 30, 20);
    public int rotationSpeedDelay;
    public float maxRotSpeed = 1.5f;

    int counter = 0;
    float defaultRotationSpeed;

    void Start()
    {
        offset = baseOffset;
        defaultRotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (Input.GetKey("a") || Input.GetKey("d"))
            counter++;
        else
        {
            counter = 0;
            rotationSpeed = defaultRotationSpeed;
        }

        if (counter > rotationSpeedDelay && rotationSpeed < maxRotSpeed)
        {
            rotationSpeed += 0.005f;
        }
        //Debug.Log("C: " + counter + " R: " + rotationSpeed);
    }

    void FixedUpdate()
    {
        this.transform.position = Ball.transform.position;
    }

    void LateUpdate()
    {
        
        float input = Input.GetAxisRaw("PanRight") - Input.GetAxisRaw("PanLeft");
        
        offset = Quaternion.AngleAxis(input * rotationSpeed, Vector3.up) * offset;
        cam.transform.position = Ball.transform.position + offset;
        cam.transform.LookAt(Ball.transform.position);
        
        /*
        Quaternion camAngle = Quaternion.AngleAxis(input * rotationSpeed, Vector3.up);
        offset = camAngle * offset;
        cam.transform.position = Ball.transform.position + offset;
        cam.transform.LookAt(Ball.transform.position);
        */
    }
}

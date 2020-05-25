using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    /*
    public float rotationSpeed = 90;
    public Transform target;
    public Vector3 offset = new Vector3(0,30,20);

    private void Update()
    {
        transform.position = target.position + offset;
        

        if (Input.GetKey("d"))
        {
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }else if (Input.GetKey("a"))
        {
            transform.RotateAround(target.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(target.position);
        }
    }
    */

    public float turnSpeed = 2.0f;
    public Transform Ball;

    private Vector3 offset;
    private Vector3 baseOffset = new Vector3(0, 30, 20);

    void Start()
    {
        offset = Ball.position + baseOffset;
    }

    void LateUpdate()
    {
        float input = Input.GetAxisRaw("PanRight") - Input.GetAxisRaw("PanLeft");
        offset = Quaternion.AngleAxis(-input * turnSpeed, Vector3.up) * offset;
        transform.position = Ball.position + offset;
        transform.LookAt(Ball.position);
    }
}

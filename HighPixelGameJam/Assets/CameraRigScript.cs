using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ball;
    public Camera cam;

    public float rotationSpeed = 2f;
    private Vector3 offset;
    public Vector3 baseOffset = new Vector3(0, 30, 20);
    void Start()
    {
        offset = Ball.transform.position + baseOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Ball.transform.position;
    }

    void LateUpdate()
    {
        float input = Input.GetAxisRaw("PanRight") - Input.GetAxisRaw("PanLeft");
        offset = Quaternion.AngleAxis(-input * rotationSpeed, Vector3.up) * offset;
        cam.transform.position = Ball.transform.position + offset;
        cam.transform.LookAt(Ball.transform.position);
    }
}

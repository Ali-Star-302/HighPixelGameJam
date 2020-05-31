using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRigScript : MonoBehaviour
{
    public GameObject Ball;
    public Camera cam;

    public float rotationSpeed = 2f;
    private Vector3 offset;
    public Vector3 baseOffset = new Vector3(0, 30, 20);
    public int rotationSpeedDelay;
    public float maxRotSpeed = 2f;
    public float rotationAcceleration;
    public List<Transform> obstructions = new List<Transform>();
    public List<Transform> oldObstructions = new List<Transform>();
    int invertLook;
    int counter = 0;
    int invertMutliplier;
    float defaultRotationSpeed;
    int groundLayer = 8;

    void Start()
    {
        invertLook = PlayerPrefs.GetInt("InvertLook", 0);
        baseOffset = new Vector3(0, PlayerPrefs.GetFloat("CameraHeight", 10f), PlayerPrefs.GetFloat("CameraWidth", 20f));

        offset = baseOffset;
        defaultRotationSpeed = rotationSpeed;
        obstructions.Add(Ball.transform);
        if (invertLook == 1)
        {
            invertMutliplier = -1;
        }
        else
        {
            invertMutliplier = 1;
        }

        
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
            rotationSpeed += rotationAcceleration * 100 * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        this.transform.position = Ball.transform.position;
    }

    void LateUpdate()
    {
        float input = Input.GetAxisRaw("PanRight") - Input.GetAxisRaw("PanLeft");
        
        offset = Quaternion.AngleAxis(invertMutliplier* input * rotationSpeed, Vector3.up) * offset;
        cam.transform.position = Ball.transform.position + offset;
        cam.transform.LookAt(Ball.transform.position);
        /* Quaternion camAngle = Quaternion.AngleAxis(input * rotationSpeed, Vector3.up);
        offset = camAngle * offset;
        cam.transform.position = Ball.transform.position + offset;
        cam.transform.LookAt(Ball.transform.position); */
        ViewObstructed();
    }

    void ViewObstructed()
    {
        RaycastHit hit;
        Vector3 dir = Ball.transform.position - cam.transform.position;
        List<MeshRenderer> mrList = new List<MeshRenderer>();
        List<SkinnedMeshRenderer> smrList = new List<SkinnedMeshRenderer>();

        if (Physics.Raycast(cam.transform.position, dir, out hit, 20f, ~groundLayer))
        {
            if (hit.collider.gameObject.tag != "Ball")
            {
                if (!obstructions.Contains(hit.transform))
                    obstructions.Add(hit.transform);

                if (hit.collider.gameObject.GetComponent<MeshRenderer>())
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
                else  //Goes through all children, disabling their mesh renderers
                {
                    foreach (Transform child in hit.transform)
                    {
                        if (child.GetComponent<MeshRenderer>())
                        {
                            child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        }
                        else if (child.GetComponent<SkinnedMeshRenderer>())
                        {
                            child.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        }
                    }
                }
            }
            else
            {
                foreach (Transform t in obstructions)
                {
                    if (t.gameObject.GetComponent<MeshRenderer>())
                        t.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    else  //Goes through all children, enabling their mesh renderers
                    {
                        foreach (Transform child in t)
                        {
                            if (child.GetComponent<MeshRenderer>())
                            {
                                child.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                            }
                            else if (child.GetComponent<SkinnedMeshRenderer>())
                            {
                                child.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                            }
                        }
                    }
                }
                obstructions.Clear();
            }
            
        }
    }
}

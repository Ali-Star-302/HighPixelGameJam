using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittyBall : MonoBehaviour
{
    public Image arrowBody;
    public Image arrowHead;
    public float barMultiplier = 0.5f;
    public float arrowWidth = 5f;
    public Gradient gradient;
    public Text strokeCountText;
    public Text worldChangeText;
    public LayerMask groundLayer;
    public GameObject Cam;
    public int strokeCount;
    public PowerBar powerBar;
    public int worldChangeDelay;
    public int worldChangeCounter;
    public bool isNormalWorld = true;
    public GameObject changingObjects;
    public bool stationary = true;

    Quaternion originalRotation;
    Rigidbody rb;
    float power = 0f;
    bool powerUp = true;
    int maxPower = 100;
    GameObject arrow;
    
    private void Start()
    {
        powerBar.SetMaxPower(maxPower);
        Physics.gravity *= 2;
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
        //arrow = transform.GetChild(0).gameObject;
        arrow = GameObject.FindGameObjectWithTag("Aiming Arrow");
    }

    void Update()
    {
        if (worldChangeCounter < worldChangeDelay)
            worldChangeCounter++;
        if (isNormalWorld)
        {
            worldChangeText.color = Color.blue;
        }
        else
            worldChangeText.color = Color.magenta;


        if (rb.velocity == Vector3.zero)
        {
            stationary = true;
            arrowHead.enabled = true;
            arrowBody.enabled = true;
            arrow.SetActive(true);
        }
        else
        {
            stationary = false;
            arrowHead.enabled = false;
            arrowBody.enabled = false;
            arrow.SetActive(false);
        }
        
        if (Input.GetKeyDown("q") && stationary && worldChangeCounter >= worldChangeDelay)
        {
            ChangeWorld();
            worldChangeCounter = 0;
        }

        if (powerUp && power >= maxPower)
        {
            powerUp = false;
        }
        else if (!powerUp && power <= 0)
        {
            powerUp = true;
        }

        if (Input.GetButton("Jump") && stationary)
        {
            if (powerUp)
                power += 0.5f;
            else
                power -= 0.5f;
        }
        else if (Input.GetButtonUp("Jump") && stationary)
        {
            HitBall();
        }
        worldChangeText.text = "World change charge: " + worldChangeCounter;
        strokeCountText.text = "Count: " + strokeCount;
        powerBar.SetPower((int)power);

        if (Physics.Raycast(transform.position,Vector3.down, 1f, groundLayer))
        {
            if (rb.velocity.sqrMagnitude < 1)
            {
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
            else if(rb.velocity.sqrMagnitude < 12)
            {
                rb.drag = 1f;
            }
            else
            {
                rb.drag = 0.1f;
            }
        }
        else
        {
            rb.drag = 0f;
        }
        if (stationary)
        {
            if (power != 0)
            {
                arrowHead.enabled = true;
            }
            else
                arrowHead.enabled = false;
            arrowBody.enabled = true;
            arrow.SetActive(true);
            Vector3 newLocalPos = Vector3.ClampMagnitude(Cam.transform.rotation * new Vector3(1, 0, 0), 1) * 15;
            arrow.transform.position = this.transform.position + newLocalPos;
            arrow.transform.RotateAround(this.transform.position, Vector3.up, -90f);
            arrowBody.transform.position = this.transform.position;
            arrowBody.transform.LookAt(arrow.transform.position);
            arrowBody.transform.Rotate(new Vector3(90, 0, 0));
            arrowBody.rectTransform.sizeDelta = new Vector2(arrowWidth, power * barMultiplier);
            arrowHead.transform.localPosition = Vector3.up * (power * (barMultiplier*0.96f));
            arrowHead.transform.LookAt(arrow.transform.position);
            arrowHead.transform.Rotate(new Vector3(90, 0, 0));

            arrowBody.color = gradient.Evaluate(power/100);
            arrowHead.color = gradient.Evaluate(power / 100);
        }
    }

    void ChangeWorld()
    {
        if (isNormalWorld)
        {
            changingObjects.BroadcastMessage("NormalToParallel", SendMessageOptions.DontRequireReceiver);
            isNormalWorld = false;
        }
        else
        {
            changingObjects.BroadcastMessage("ParallelToNormal", SendMessageOptions.DontRequireReceiver);
            isNormalWorld = true;
        }
    }


    void HitBall()
    {
        Vector3 newRot = Vector3.ClampMagnitude(Cam.transform.rotation * new Vector3(0,0,1),1);
        //transform.rotation = Quaternion.Euler(newRot);
        Vector3 newForce = newRot * ((int)(power * 1.2));
        newForce.y = 0;
        rb.AddForce(newForce,ForceMode.Impulse);
        power = 0;
        strokeCount++;
    }
}

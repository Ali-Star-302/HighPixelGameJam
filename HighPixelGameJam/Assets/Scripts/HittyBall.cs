using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittyBall : MonoBehaviour
{
    public Sprite normalToParallelImage;
    public Sprite parallelToNormalImage;
    public Image arrowBody;
    public Image arrowHead;
    public int maxRewinds;
    public float barMultiplier = 0.5f;
    public float arrowWidth = 5f;
    public Gradient gradient;
    public Text strokeCountText;
    public Text timerText;
    public Text switchesText;
    public LayerMask groundLayer;
    public GameObject Cam;
    public int strokeCount;
    public PowerBar powerBar;
    public WorldChangeBar worldChangeBar;
    public int worldChangeDelay;
    public int worldChangeCounter;
    public bool isNormalWorld = true;
    public GameObject changingObjects;
    public bool stationary = true;
    public float levelTime;
    public int worldSwitches;

    Quaternion originalRotation;
    Rigidbody rb;
    float power = 0f;
    bool powerUp = true;
    int maxPower = 100;
    GameObject arrow;
    Vector3 lastPosition;
    float levelStartTime;

    //Pausing variables
    public GameObject pauseMenuUI;
    bool paused = false;
    GameObject ui;

    private void Start()
    {
        levelStartTime = Time.time;
        worldChangeBar.SetMaxCharge(worldChangeDelay);
        powerBar.SetMaxPower(maxPower);
        Physics.gravity = Vector3.down*20;
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
        //arrow = transform.GetChild(0).gameObject;
        arrow = GameObject.FindGameObjectWithTag("Aiming Arrow");
    }

    void Update()
    {
        Pausing();

        if (paused)
            return;

        //Stationary/not stationary
        if (rb.velocity.sqrMagnitude <= 1)
        {
            stationary = true;
            arrowHead.enabled = true;
            arrowBody.enabled = true;
            arrow.SetActive(true);
            SetLastPosition();
        }
        else
        {
            stationary = false;
            arrowHead.enabled = false;
            arrowBody.enabled = false;
            arrow.SetActive(false);
        }
        
        //Change world
        if (Input.GetKeyDown("q") && stationary && worldChangeCounter >= worldChangeDelay)
        {
            worldSwitches++;
            ChangeWorld();
            worldChangeCounter = 0;
        }

        //Reset position
        if (Input.GetKeyDown("r") && stationary)
        {
            transform.position = Vector3.zero;
        }

        Power();
        UI();
        Drag();
        DisplayArrows();
    }
    
    void SetLastPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20f))
        {
            if (!(hit.transform.gameObject.tag == "Platform"))
            {
                lastPosition = transform.position;
            }
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

    void ResetBall()
    {
        transform.position = lastPosition;
        rb.velocity = Vector3.zero;
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

    void Pausing()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                ui = Instantiate(pauseMenuUI);
                paused = true;
            }
            else
            {
                ui.BroadcastMessage("Resume");
                paused = false;
            }
        }
    }

    void UI()
    {
        //Timer
        levelTime = (Time.time - levelStartTime);
        timerText.text = "Time: " + (int)(levelTime) + "s";

        if (worldChangeCounter < worldChangeDelay)
            worldChangeCounter++;

        strokeCountText.text = "Strokes: " + strokeCount;
        switchesText.text = "Duality Switches: " + worldSwitches;
        powerBar.SetPower((int)power);
        worldChangeBar.SetCharge((int)worldChangeCounter);

        if (isNormalWorld)
            worldChangeBar.worldImage.sprite = normalToParallelImage;
        else
            worldChangeBar.worldImage.sprite = parallelToNormalImage;

        if (stationary)
            worldChangeBar.stationary = true;
        else
            worldChangeBar.stationary = false;
    }

    void Power()
    {
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
                power += 0.7f * 100 * Time.deltaTime;
            else
                power -= 0.7f * 100 * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump") && stationary)
        {
            HitBall();
        }
    }

    void Drag()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer))
        {
            if (rb.velocity.sqrMagnitude < 1)
            {
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
            else if (rb.velocity.sqrMagnitude < 12)
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
    }

    void DisplayArrows()
    {
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
            arrowHead.transform.localPosition = Vector3.up * (power * (barMultiplier * 0.96f));
            arrowHead.transform.LookAt(arrow.transform.position);
            arrowHead.transform.Rotate(new Vector3(90, 0, 0));

            arrowBody.color = gradient.Evaluate(power / 100);
            arrowHead.color = gradient.Evaluate(power / 100);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Start is called before the first frame update
    int invertLook;
    float camHeight;
    float camWidth;
    public Toggle InvertLookToggle;
    public Slider camHeightSlider;
    public Slider camWidthSlider;
    void Start()
    {
        invertLook = PlayerPrefs.GetInt("InvertLook", 0);
        if (invertLook == 0)
            InvertLookToggle.isOn = false;
        else
            InvertLookToggle.isOn = true;

        camHeight = PlayerPrefs.GetFloat("CameraHeight", 10f);
        camHeightSlider.value = camHeight;

        camWidth = PlayerPrefs.GetFloat("CameraWidth", 20f);
        camWidthSlider.value = camWidth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void InvertLook()
    {
        invertLook = (invertLook + 1) % 2;
        PlayerPrefs.SetInt("InvertLook", invertLook);
    }

    public void CamHeightChanged()
    {
        camHeight = camHeightSlider.value;
        PlayerPrefs.SetFloat("CameraHeight", camHeight);
    }
    public void CamWidthChanged()
    {
        camWidth = camWidthSlider.value;
        PlayerPrefs.SetFloat("CameraWidth", camWidth);
    }
}

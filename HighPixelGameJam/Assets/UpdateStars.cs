using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStars : MonoBehaviour
{
    Image star1;
    Image star2;
    Image star3;

    public Sprite filledStar;
    public Sprite unfilledStar;

    public string level;

    void Start()
    {
        star1 = transform.GetChild(0).GetComponent<Image>();
        star2 = transform.GetChild(1).GetComponent<Image>();
        star3 = transform.GetChild(2).GetComponent<Image>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(level + "Star1") == 1)
            star1.sprite = filledStar;
        else
            star1.sprite = unfilledStar;
        if (PlayerPrefs.GetInt(level + "Star2") == 1)
            star2.sprite = filledStar;
        else
            star2.sprite = unfilledStar;
        if (PlayerPrefs.GetInt(level + "Star3") == 1)
            star3.sprite = filledStar;
        else
            star3.sprite = unfilledStar;
    }
}

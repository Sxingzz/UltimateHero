using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public Image fadeImg;
    public float fadeSpeed = 2f;
    private bool fadingToBlack;
    private bool fadingFromBlack;

    void Update()
    {
        if (fadingToBlack)
        {
            fadeImg.color = new Color(fadeImg.color.r,
                fadeImg.color.g,
                fadeImg.color.b,
                Mathf.MoveTowards(fadeImg.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeImg.color.a == 1f)
            {
                fadingToBlack = false;
            }
        }
        else if (fadingFromBlack)
        {
            fadeImg.color = new Color(fadeImg.color.r,
                fadeImg.color.g,
                fadeImg.color.b,
                Mathf.MoveTowards(fadeImg.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeImg.color.a == 0f)
            {
                fadingFromBlack = false;
                UIController.Instance.ActiveFadePanel(false);
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadingFromBlack = true;
        fadingToBlack = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider seSlider;

    private float bgmValue;
    private float seValue;

    private void Awake()
    {
        SetUp();
    }

    private void OnEnable()
    {
        SetUp();
    }

    private void SetUp()
    {
        if (AudioManager.HasInstance)
        {
            bgmValue = AudioManager.Instance.AttachBGMSource.volume;
            seValue = AudioManager.Instance.AttachSESource.volume;
            bgmSlider.value = bgmValue;
            seSlider.value = seValue;
        }
    }

    public void OnSliderChangeBGMValue(float v)
    {
        bgmValue = v;
    }

    public void OnSliderChangeSEValue(float v)
    {
        seValue = v;
    }

    public void OnCancelButtonClick()
    {
        if (UIController.HasInstance)
        {
            UIController.Instance.ActiveMenuPanel(true);
            UIController.Instance.ActiveSettingPanel(false);
        }
    }

    public void OnSubmitButtonClick()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeBGMVolume(bgmValue);
            AudioManager.Instance.ChangeSEVolume(seValue);
        }

        if (UIController.HasInstance)
        {
            UIController.Instance.ActiveMenuPanel(true);
            UIController.Instance.ActiveSettingPanel(false);
        }
    }
}

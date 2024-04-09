using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        if (UIController.HasInstance)
        {
            UIController.Instance.ActiveMenuPanel(false);
            UIController.Instance.ActiveLoadingPanel(true);
        }
    }

    public void OnSettingButtonClick()
    {
        if (UIController.HasInstance)
        {
            UIController.Instance.ActiveSettingPanel(true);
            UIController.Instance.ActiveMenuPanel(false);
        }
    }

    public void OnQuitButtonClick()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.EndGame();
        }
    }
}

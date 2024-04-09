using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnClickedResumeButton()
    {
        if (GameManager.HasInstance && UIController.HasInstance)
        {
            GameManager.Instance.ResumeGame();
            UIController.Instance.ActivePausePanel(false);
        }
    }

    public void OnClickedBackToMenuButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestarGame();
        }
    }
}

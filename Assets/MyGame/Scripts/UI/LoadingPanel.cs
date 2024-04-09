using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public TextMeshProUGUI loadingPercentText;
    public Slider loadingSlider;

    private void OnEnable()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene1");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingPercentText.SetText($"LOADING SCENES: {asyncOperation.progress * 100}%");
            if (asyncOperation.progress >= 0.9f)
            {
                loadingSlider.value = 1f;
                loadingPercentText.SetText("Press the space bar to continue");
                if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1)
                {
                    asyncOperation.allowSceneActivation = true;
                    if (UIController.HasInstance)
                    {
                        UIController.Instance.ActiveGamePanel(true);
                        UIController.Instance.GamePanel.SetTimeRemain(120);
                        UIController.Instance.ActiveLoadingPanel(false);
                    }
                    if (GameManager.HasInstance)
                    {
                        GameManager.Instance.StartGame();
                    }

                    if (AudioManager.HasInstance)
                    {
                        AudioManager.Instance.PlayBGM(AUDIO.BGM_LEVEL_MUSIC2);
                    }
                }
            }
            yield return null;
        }
    }
}

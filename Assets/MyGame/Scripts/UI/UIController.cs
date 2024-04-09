using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : BaseManager<UIController>
{
    [SerializeField]
    private MenuPanel menuPanel;
    public MenuPanel MenuPanel => menuPanel;

    [SerializeField]
    private LoadingPanel loadingPanel;
    public LoadingPanel LoadingPanel => loadingPanel;

    [SerializeField]
    private GamePanel gamePanel;
    public GamePanel GamePanel => gamePanel;

    [SerializeField]
    private FadePanel fadePanel;
    public FadePanel FadePanel => fadePanel;

    [SerializeField]
    private SettingPanel settingPanel;
    public SettingPanel SettingPanel => settingPanel;

    [SerializeField]
    private LosePanel losePanel;
    public LosePanel LosePanel => losePanel;

    [SerializeField]
    private PausePanel pausePanel;
    public PausePanel PausePanel => pausePanel;

    [SerializeField]
    private WinPanel winPanel;
    public WinPanel WinPanel => winPanel;

    void Start()
    {
        ActiveMenuPanel(true);
        ActiveLoadingPanel(false);
        ActiveGamePanel(false);
        ActiveFadePanel(false);
        ActiveSettingPanel(false);
        ActiveLosePanel(false);
        ActivePausePanel(false);
        ActiveWinPanel(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.HasInstance && GameManager.Instance.IsPlaying == true)
            {
                GameManager.Instance.PauseGame();
                ActivePausePanel(true);
            }
        }
    }

    public void ActiveMenuPanel(bool active)
    {
        menuPanel.gameObject.SetActive(active);
    }

    public void ActiveLoadingPanel(bool active)
    {
        loadingPanel.gameObject.SetActive(active);
    }

    public void ActiveGamePanel(bool active)
    {
        gamePanel.gameObject.SetActive(active);
    }

    public void ActiveFadePanel(bool active)
    {
        fadePanel.gameObject.SetActive(active);
    }

    public void ActiveSettingPanel(bool active)
    {
        settingPanel.gameObject.SetActive(active);
    }

    public void ActiveLosePanel(bool active)
    {
        losePanel.gameObject.SetActive(active);
    }

    public void ActivePausePanel(bool active)
    {
        pausePanel.gameObject.SetActive(active);
    }

    public void ActiveWinPanel(bool active)
    {
        winPanel.gameObject.SetActive(active);
    }
}

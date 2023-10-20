using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuUI;
    public GameObject finishMenuUI;
    public Button playButton;
    public Button nextLevelButton;
    public Button playAgainButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayButton);
        playAgainButton.onClick.AddListener(PlayAgainButton);
        nextLevelButton.onClick.AddListener(NextLevelButton);
    }

    public void OpenMainMenuUI()
    {
        mainMenuUI.SetActive(true);
        finishMenuUI.SetActive(false);
    }

    public void OpenFinishUI()
    {
        finishMenuUI.SetActive(true);
    }

    public void PlayButton()
    {
        mainMenuUI.SetActive(false);
        LevelManager.Instance.OnStart();
    }

    public void PlayAgainButton()
    {
        LevelManager.Instance.LoadLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenuUI();
    }

    public void NextLevelButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.NextLevel();
        OpenMainMenuUI();
        
    }
}

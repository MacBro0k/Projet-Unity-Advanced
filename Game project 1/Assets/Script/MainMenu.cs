using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider SliderUI;
    private GameObject OptionMenu;

    // private void HandleGameStateChanged(GameManager.GameState previousGameState, GameManager.GameState CurrentGameState) {
    //     if (previousGameState == GameManager.GameState.PREGAME && CurrentGameState == GameManager.GameState.RUNNING)
    //         PlayGame();
    // }
    private void Start() {
        // GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    public void PlayLevel(string LevelName) {
        GameManager.Instance.LoadLevel(LevelName);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volume", SliderUI.value);
        Debug.Log(PlayerPrefs.GetFloat("volume"));
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Toggled fullscreen");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Slider SliderUI;
    private GameObject OptionMenu;
    public AudioMixer MasterVolume;

    // private void HandleGameStateChanged(GameManager.GameState previousGameState, GameManager.GameState CurrentGameState) {
    //     if (previousGameState == GameManager.GameState.PREGAME && CurrentGameState == GameManager.GameState.RUNNING)
    //         GameManager.Instance.StartGame();
    // }
    private void Start() {
        // GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    public void PlayLevel(string LevelName) {
        SceneManager.LoadScene(LevelName);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volume", SliderUI.value);
        Debug.Log(PlayerPrefs.GetFloat("volume"));
        MasterVolume.SetFloat("MasterVolume", Mathf.Log10(SliderUI.value) * 20);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Toggled fullscreen");
    }

}

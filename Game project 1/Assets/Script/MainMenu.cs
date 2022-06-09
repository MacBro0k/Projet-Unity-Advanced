using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Slider SliderUI;

    private GameObject OptionMenu;
    public void PlayGame() {
        GameManager.LoadLevel("SampleScene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void Zebi() {
        Application.OpenURL("https://paypal.me/cestmoilebossjetedis?country.x=FR&locale.x=fr_FR");
    }
    


}

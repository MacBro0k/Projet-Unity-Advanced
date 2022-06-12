using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDeath : MonoBehaviour
{

    public void RestartGame(){
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quitgame(){
        Application.Quit();
    }


}

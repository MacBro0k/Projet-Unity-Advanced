using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void RestartGame(string SceneName){
        Debug.Log("restart");
        SceneManager.LoadScene(SceneName);
    }

    public void Quitgame(){
        Application.Quit();
    }


}

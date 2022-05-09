using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public string NextLevelName;

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("oui");
        if (col.tag == "Player"){
            SceneManager.LoadScene(NextLevelName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerHearth : MonoBehaviour
{
    public CanvasGroup endCG;
    public TextMeshProUGUI endGameUIText; 
    public string endText = "You win!";
    public string endTextLoose = "You died!";

    void Start()
    {
        if (GetComponent<PlayerInventory>().Life == 0)
        {
            endText = endTextLoose;
        }
    }

    public void EndGame()
    {
        endGameUIText.text = endText;
        endCG.alpha = 1;
        endCG.interactable = true;
    }

    public void WinGame()
    {
        endGameUIText.text = endText;
        EndGame();
    }
    public void MakeDead(){
        endText = endTextLoose;
        EndGame();
        Destroy(gameObject);
    }


}

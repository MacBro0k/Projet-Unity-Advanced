using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Interact")]
    [SerializeField] private GameObject VisualInteract;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        VisualInteract.SetActive(false);
    }

    private void Update()
    {
        if (playerinRange){
            VisualInteract.SetActive(true);
            //if (In)
        }else{
            VisualInteract.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
                if(collider.tag == "player")
        {
            playerInRange = false;
        }
    }
}

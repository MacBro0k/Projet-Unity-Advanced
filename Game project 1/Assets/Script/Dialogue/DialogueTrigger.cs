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
        if(playerInRange)
        {
            VisualInteract.SetActive(true);
            if (Input.GetButtonDown("Interact")){
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }else{
            VisualInteract.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
                if(collider.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

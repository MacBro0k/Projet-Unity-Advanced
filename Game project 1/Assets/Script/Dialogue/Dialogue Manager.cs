using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTexte;

    private Story currentStory;

    private bool dialogueIsPlaying;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null){
            Debug.LogWarning("More than one Dialogue Mana in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void start(){
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void update(){
        if(!dialogueIsPlaying)
            return;
        
        if(Input.GetKeyDown("Submit")){
            ContinueStory();
        }
    }

    private void EnterDialogueMode(TextAsset inkJSON){
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    private void ExitDialogueMode(){
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueTexte.text = "";
    }

    private void ContinueStory(){
        if(currentStory.canContinue){
            dialogueTexte.text = currentStory.Continue();
        }
        else{
            ExitDialogueMode();
        }
    }
}

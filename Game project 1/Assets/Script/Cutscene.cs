using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene: MonoBehaviour
{
    static Cutscene instance;
    public bool CutscenePlaying;
    // Start is called before the first frame update
    void Start()
    {
        CutscenePlaying = false;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Mana in the scene");
        }
        instance = this;
    }

    public static Cutscene GetInstance()
    {
        return instance;
    }

    public void ToggleCutscene()
    {
        CutscenePlaying = !CutscenePlaying;
    }
}

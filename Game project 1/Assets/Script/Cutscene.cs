using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene: MonoBehaviour
{
    static Cutscene instance;
    public bool CutscenePlaying {get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Cutscene in the scene");
        }
        instance = this;
    }

    public static Cutscene GetInstance()
    {
        return instance;
    }

    public void StartCutscene()
    {
        CutscenePlaying = true;
    }

    public void EndCutscene() {
        CutscenePlaying = false;
        Destroy(gameObject.GetComponent<Animator>());
    }
}

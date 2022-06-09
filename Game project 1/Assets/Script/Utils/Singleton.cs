using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private static bool isInitialized;

    public static T Instance {
        get { return instance; }
    }

    protected virtual void Awake() {
        if(instance != null) {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class.");
        } 
        else
            instance = (T)this;
    }

    protected virtual void OnDestroy() {
        if (instance == this)
            instance = null;    
    }

    public static bool IsInitialized {
        get { if (instance != null)
            return true; 
            else
            return false;}
    }
}

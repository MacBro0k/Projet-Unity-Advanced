using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fonctionnalités à mettre en place :
    // - Garder en mémoire le niveau du jeu courant
    // - Charger et décharger les différents niveaux du jeu
    // - Garder une trace de l'état courant du jeu
    // - Générer d'autres systèmes persistants

    private string _currentLevelName = string.Empty;
    List<AsyncOperation> _loadOperations;
    private static GameManager instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } 
        else {
            Destroy(gameObject);
            Debug.LogError("Yo don't do this bruh");
 }
    }
    private void Start() {
        LoadLevel("SampleScene");
        _loadOperations = new List<AsyncOperation>();
        DontDestroyOnLoad(this.gameObject);
        
    }
    public void LoadLevel(string levelName) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        if (ao != null) {
            Debug.LogError("[GameManager] Unable to load level" + levelName);
            return;
        }
        
        _currentLevelName = levelName;
    }
    public void UnloadLevel(string levelName) {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;

        if (ao != null) {
            Debug.LogError("[GameManager] Unable to unload level" + levelName);
            return;
        }
        _currentLevelName = levelName;
    }

    void OnLoadOperationComplete(AsyncOperation elem) {
        if (_loadOperations.Contains(elem)) {
            _loadOperations.Remove(elem);
        }
        UnloadLevel(_currentLevelName);
        Debug.Log("Load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation elem) {
        Debug.Log("Unload Complete");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Fonctionnalités à mettre en place :
    // - Garder en mémoire le niveau du jeu courant
    // - Charger et décharger les différents niveaux du jeu
    // - Garder une trace de l'état courant du jeu
    // - Générer d'autres systèmes persistants

    private string _currentLevelName = string.Empty;
    List<AsyncOperation> _loadOperations;
    public GameObject[] SystemPrefabs;
    private List<GameObject> _instanciedSystemPrefabs;

    
    private void Start() {
        LoadLevel("Lvl_1-2");
        _loadOperations = new List<AsyncOperation>();
        DontDestroyOnLoad(this.gameObject);
        _instanciedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
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

    void InstantiateSystemPrefabs() {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; i++) {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instanciedSystemPrefabs.Add(prefabInstance);

        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instanciedSystemPrefabs.Count; i++) {
            Destroy(_instanciedSystemPrefabs[i]);
        }

        _instanciedSystemPrefabs.Clear();
    }
}

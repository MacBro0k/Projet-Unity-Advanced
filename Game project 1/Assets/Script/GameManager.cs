using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[System.Serializable]
public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> {
    
}

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
    List<GameObject> _instanciedSystemPrefabs;
    GameState _currentGameState = GameState.PREGAME;
    public EventGameState OnGameStateChanged;

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }


    // Loading Levels Methods
    
    private void Start() {
        _currentLevelName = SceneManager.GetActiveScene().name;
        Debug.Log(_currentLevelName);
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

    private void OnLoadOperationComplete(AsyncOperation elem) {
        if (_loadOperations.Contains(elem)) {
            _loadOperations.Remove(elem);
        }

        UnloadLevel(_currentLevelName);

        if (_loadOperations.Count == 0)
            UpdateState(GameState.RUNNING);

        Debug.Log("Load Complete");
    }

    private void OnUnloadOperationComplete(AsyncOperation elem) {
        Debug.Log("Unload Complete");
    }


    // Singleton Methods

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


    // GameState Methods

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { UpdateState(value); }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                break;

            case GameState.RUNNING:
                break;

            case GameState.PAUSED:
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    private void StartGame() {
        if (CurrentGameState != GameState.PREGAME)
            return;
        LoadLevel(_currentLevelName);
    }

}

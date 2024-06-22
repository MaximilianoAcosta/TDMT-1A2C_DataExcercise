using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    [SerializeField] private DataSource<ScenesManager> sceneryManagerDataSource;
    [SerializeField] private DataSource<Level> defaultLevelDataSource;
    [SerializeField] private LevelEventChannel LevelChange;
    [SerializeField] private VoidEventChannel WinEvent;
    [SerializeField] private VoidEventChannel LoseEvent;

    private Level _currentLevel;
    private void OnEnable()
    {
        if (sceneryManagerDataSource != null)
        {
            sceneryManagerDataSource.Value = this;
        }
        LevelChange.OnEventRaised += ChangeLevel;
        WinEvent.OnEventRaised += UnloadCurrentLevel;
        LoseEvent.OnEventRaised += UnloadCurrentLevel;
    }
    private void Start()
    {
        StartCoroutine(LoadFirstLevel(defaultLevelDataSource.Value));
    }
  
    private void OnDisable()
    {
        if (sceneryManagerDataSource != null && sceneryManagerDataSource.Value == this)
        {
            sceneryManagerDataSource.Value = null;
        }
    }
    public void ChangeLevel(Level level)
    {
        StartCoroutine(ChangeLevel(_currentLevel, level));
    }
    private IEnumerator ChangeLevel(Level currentLevel, Level newLevel)
    {

        if (currentLevel != null) 
        { 
            yield return Unload(currentLevel); 
        }
        yield return Load(newLevel);
        _currentLevel = newLevel;
       
    }

    private IEnumerator LoadFirstLevel(Level level)
    {
        foreach (string sceneName in level.SceneNames)
        {
            var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadOp.isDone);
        }
       
    }
    private void UnloadCurrentLevel()
    {
        StartCoroutine(Unload(_currentLevel));
        _currentLevel = null;
    }

    private IEnumerator Load(Level level)
    {
        
        foreach (var sceneName in level.SceneNames)
        {
            var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadOp.isDone);
        }
    }

    private IEnumerator Unload(Level level)
    {
        foreach (var sceneName in level.SceneNames)
        {
            var loadOp = SceneManager.UnloadSceneAsync(sceneName);
            yield return new WaitUntil(() => loadOp.isDone);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] LevelDataSource SceneList;
    private List<string> Scenes;
    string SceneToActive;
    void Start()
    {
        if (SceneList == null)
        {
            StartCoroutine(StartGameCorroutine(sceneToLoad));
        }
    }

     IEnumerator StartGameCorroutine(string scene)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadOperation.isDone);
    }
}

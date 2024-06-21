using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DataSource<string> Play;
        [SerializeField] private DataSource<string> Exit;
        [SerializeField] private DataSource<GameManager> gameManagerDataSource;
        [SerializeField] private DataSource<ScenesManager> sceneryManagerDataSource;
        [SerializeField] private DataSource<Level> StartingLevel;

        private void OnEnable()
        {
            if (gameManagerDataSource != null)
                gameManagerDataSource.Value = this;
        }

        private void OnDisable()
        {
            if (gameManagerDataSource != null && gameManagerDataSource.Value == this)
            {
                gameManagerDataSource.Value = null;
            }
        }

        public void HandleSpecialEvents(string id)
        {
            if (id == Play.Value)
            {
                if (sceneryManagerDataSource != null && sceneryManagerDataSource.Value != null)
                {
                    sceneryManagerDataSource.Value.ChangeLevel(StartingLevel.Value);
                }
            }
            else if (id == Exit.Value)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
    }
}

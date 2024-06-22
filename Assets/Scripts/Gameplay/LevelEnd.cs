
using UnityEngine;

namespace Gameplay
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel WinEvent;
        [SerializeField] private LevelEventChannel ChangeLevel;
        [SerializeField] private DataSource<Level> LevelToChange;

        private void OnTriggerEnter(Collider other)
        {
            //DONE TODO: Raise event through event system telling the game to show the win sequence.
            if (ChangeLevel != null && LevelToChange != null)
            {
                ChangeLevel.OnEventRaised?.Invoke(LevelToChange.Value);
                return;
            }
            WinEvent.OnEventRaised?.Invoke();


            Debug.Log($"{name}: Player touched the flag!");
        }
    }
}

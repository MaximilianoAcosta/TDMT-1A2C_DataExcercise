using Characters;
using System.Diagnostics.Tracing;
using UnityEngine;
namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] DataSource<PlayerController> PlayerReference;
        [SerializeField] Vector2EventChannel MoveEvent;
        [SerializeField] BoolEventChannel SprintEvent;
        private Character _character;

        private void Reset()
        {
            _character = GetComponent<Character>();
            
        }

        private void Awake()
        {
            _character ??= GetComponent<Character>();
            if (_character)
            {
                _character.enabled = false;
            }
            PlayerReference.Value = this;
        }

        private void OnEnable()
        {
            //DONE TODO: Subscribe to inputs via event manager/event channel
            SprintEvent.OnEventRaised += HandleRun;
            MoveEvent.OnEventRaised += HandleMove;
            //DONE TODO: Set itself as player reference via ReferenceManager/DataSource
            PlayerReference.Value = this;
        }

        private void OnDisable()
        {
            //DONE TODO: Unsubscribe from all inputs via event manager/event channel
            SprintEvent.OnEventRaised -= HandleRun;
            MoveEvent.OnEventRaised -= HandleMove;
            //DONE TODO: Remove itself as player reference via reference manager/dataSource
            PlayerReference.Value = null;
        }

        public void SetPlayerAtLevelStartAndEnable(Vector3 levelStartPosition)
        {
            transform.position = levelStartPosition;
            _character.enabled = true;
        }
        
        private void HandleMove(Vector2 direction)
        {
            _character.SetDirection(new Vector3(direction.x, 0, direction.y));
        }

        private void HandleRun(bool shouldRun)
        {
            if (shouldRun)
                _character.StartRunning();
            else
                _character.StopRunning();
        }
    }
}

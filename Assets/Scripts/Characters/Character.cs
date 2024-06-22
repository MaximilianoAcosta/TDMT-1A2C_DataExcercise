using UnityEngine;
using Core.Interactions;

namespace Characters
{
    public class Character : MonoBehaviour, ISteerable, ITarget
    {
        [SerializeField] private VoidEventChannel LoseEvent;
        [SerializeField] private float speed = 2.5f;
        [SerializeField] private float runningSpeed = 5;

        private Vector3 _currentDirection = Vector3.zero;
        private bool _isRunning = false;

        private void Update()
        {
            var currentSpeed = _isRunning ? runningSpeed : speed;
            transform.Translate(_currentDirection * (Time.deltaTime * currentSpeed), Space.World);
        }

        public void SetDirection(Vector3 direction)
        {
            _currentDirection = direction;
        }

        public void StartRunning() => _isRunning = true;

        public void StopRunning() => _isRunning = false;

        public void ReceiveAttack()
        {
            //DONE TODO: Raise event through event system telling the game to show the defeat sequence.
            if (LoseEvent != null)
            {
                LoseEvent.OnEventRaised?.Invoke();
            }
            Debug.Log($"{name}: received an attack!");
        }
    }
}
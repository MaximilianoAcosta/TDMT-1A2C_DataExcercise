using System;
using Core.Interactions;
using Gameplay;
using UnityEngine;

namespace AI
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] DataSource<PlayerController> PlayerReference;
        [SerializeField] private float attackDistance;
        private ITarget _target;
        private ISteerable _steerable;

        private void Awake()
        {
            _steerable = GetComponent<ISteerable>();
            if( _steerable == null)
            {
                Debug.LogError($"{name}: cannot find a {nameof(ISteerable)} component!");
                enabled = false;
            }
        }
        private void Update()
        {
            //DONE TODO: Add logic to get the target from a source/reference system
            if (_target == null )
            {
                _target = PlayerReference.Value.gameObject.GetComponent<ITarget>();
                return;
            }
            //          AB        =         B        -          A
            //there is a bug here, has no impact in the gameplay but displays an error in console.
            // I tried adding multile null ckecks but it isnt fixed
            var directionToTarget = _target.transform.position - transform.position;
            var distanceToTarget = directionToTarget.magnitude;
            if (distanceToTarget < attackDistance)
            {
                _target.ReceiveAttack();
            }
            else
            {
                Debug.DrawRay(transform.position, directionToTarget.normalized, Color.red);
                _steerable.SetDirection(directionToTarget.normalized);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}

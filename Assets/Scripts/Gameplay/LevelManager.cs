using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] DataSource<PlayerController> PlayerReference;    
        [SerializeField] private Transform levelStart;
        [SerializeField] private Transform levelEnd;
        
        private PlayerController _playerController;
        private IEnumerator Start()
        {
            while (_playerController == null)
            {
                // DONE TODO: Get reference to player controller from ReferenceManager/DataSource
                if (PlayerReference != null)
                {
                    _playerController = PlayerReference.Value;
                }
                yield return null;
            }
            _playerController.SetPlayerAtLevelStartAndEnable(levelStart.position);
        }
    }
}

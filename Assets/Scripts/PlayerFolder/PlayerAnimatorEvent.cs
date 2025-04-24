using Components;
using UnityEngine;

namespace PlayerFolder
{
    public class PlayerAnimatorEvent :MonoBehaviour
    {
        [SerializeField] private SpawnComponent spawnComponent;
        private Player _player;
        
        private void Awake()
        { 
            _player = GetComponentInParent<Player>();
        }

        public void SpawnMovementPartical()
        {
            spawnComponent.Spawn();
        }
    }
}
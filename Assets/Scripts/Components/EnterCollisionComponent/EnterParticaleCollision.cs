using PlayerFolder;
using UnityEngine;

namespace Components.EnterCollisionComponent
{
    public class EnterParticaleCollision : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        private void OnParticleCollision(GameObject other)
        {
            Debug.Log(other.name);
            Player _player = other.GetComponent<Player>();
            if (_player != null)
            {
                _player.Teleport(_destination.position);
            }
        }
    }
}
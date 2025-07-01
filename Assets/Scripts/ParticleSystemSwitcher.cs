using Creatures;
using Creatures.CreaturesStateMachine.Hero;
using UnityEngine;

namespace DefaultNamespace
{
    public class ParticleSystemSwitcher : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private bool _playerInside;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            if (_particleSystem == null)
                Debug.LogError("ParticleSystem not found!");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Hero>() != null)
            {
                _playerInside = true;
                _particleSystem?.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Hero>() != null)
            {
                _playerInside = false;
                _particleSystem?.Stop();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _playerInside ? Color.green : Color.red;
            Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
        }
    }
}

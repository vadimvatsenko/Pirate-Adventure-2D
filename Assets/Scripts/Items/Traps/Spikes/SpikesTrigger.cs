using Components;
using Components.HealthComponentFolder;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Spikes
{
    public class SpikesTrigger : MonoBehaviour
    {
        [SerializeField] private int damage;
        private Creature _creature;
        private IHealthComponent _healthComponent;
        private SpikesController _spikesController;

        private void Awake()
        {
            _spikesController = GetComponent<SpikesController>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            _healthComponent = other.GetComponent<IHealthComponent>();
            
            if (_healthComponent != null)
            {
                _spikesController.ActivateSpikes();
                _healthComponent.ApplyDamage(damage);
            }
        }
    }
}
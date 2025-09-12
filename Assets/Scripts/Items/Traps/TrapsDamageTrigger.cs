using Creatures.CreaturesHealth;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Items.Traps.Spikes;
using UnityEngine;

// триггер для урона ловушками
namespace Items.Traps
{
    public class TrapsDamageTrigger : MonoBehaviour
    {
        [SerializeField] private int damage;
        private Creature _creature;
        private CreatureHealth _healthComponent;
        private SpikesController _spikesController;

        private void Awake()
        {
            _spikesController = GetComponent<SpikesController>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            _healthComponent = other.GetComponent<CreatureHealth>();
            
            if (_healthComponent != null)
            {
                if (_spikesController != null)
                {
                    _spikesController.ActivateSpikes();
                }
                _healthComponent.TakeDamage(damage, this.transform);
            }
        }
    }
}
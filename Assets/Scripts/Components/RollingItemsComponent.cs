using Creatures;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.CreatureBasic;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    // механика платформера, например если бочка упёрлась в стену мы можем ее сдвинуть если запрыгнуть на нее
    // и начать идти в сторону стены
    public class RollingItemsComponent :MonoBehaviour
    {
        [SerializeField] private float pushMultiplier = 1f;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            Creature creature = other.gameObject.GetComponent<Creature>();


            if (creature != null)
            {
                CreatureCollisionInfo collisionInfo = creature.GetComponent<CreatureCollisionInfo>();
                if (collisionInfo != null)
                {
                    if (collisionInfo.IsWallDetected && creature.XInput != 0)
                    {
                        // Двигаем в противоположную сторону от взгляда игрока
                        _rb.velocity = new Vector2(-creature.FacingDirection * pushMultiplier, _rb.velocity.y);
                    }
                }
            }
        }
    }
}
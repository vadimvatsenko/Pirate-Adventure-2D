using Creatures.CreaturesStateMachine;
using PlayerFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures
{
    public class HeroInputReader : MonoBehaviour
    {
        
        private Creature _hero; 
        private CreatureCollisionInfo _creatureCollisionInfo;
        
        // Название должно совпадать с тем, что настроено в системе с приставкой On

        private void Start()
        {
            _hero = FindObjectOfType<Creature>();
            _creatureCollisionInfo = _hero.GetComponent<CreatureCollisionInfo>();
        }
        private void OnMovement(InputValue context) 
        {
            float direction = context.Get<float>();
            if (_hero != null) _hero.SetDirection(direction); 
        }

        private void OnJump(InputValue context)
        {
            // Передаем значение нажата ли кнопка, вызов происходит много раз. Так как значение у кнопки Value Axis
            /*if (!_hero.IsDead)
            {
                _hero.HandleJump(context.isPressed);
            }*/
        }

        private void OnInteract(InputValue context)
        {
            _creatureCollisionInfo.Interact();
        }

        private void OnHit(InputValue context) // временно
        {
            //_hero.TakeDamage();
        }

        private void OnAttack(InputValue context)
        {
            //_hero.Attack();
        }
    }
}


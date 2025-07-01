using System;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.Hero;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures
{
    public class HeroInputReader : MonoBehaviour
    {
        
        private Hero _hero; 
        private CreatureCollisionInfo _creatureCollisionInfo;
        public event Action<bool> OnPressJumpButtonPressed;
        
        // Название должно совпадать с тем, что настроено в системе с приставкой On

        private void Start()
        {
            _hero = GetComponent<Hero>();
            _creatureCollisionInfo = _hero.GetComponent<CreatureCollisionInfo>();
        }
        
        private void OnMovement(InputValue context) 
        {
            float direction = context.Get<Vector2>().x;
            if (_hero != null) _hero.SetDirection(direction); 
        }

        private void OnJump(InputValue context)
        {
            // Передаем значение нажата ли кнопка, вызов происходит много раз. Так как значение у кнопки Value Axis
            /*if (!_hero.IsDead)
            {
                _hero.HandleJump(context.isPressed);
            }*/

            //_hero.HandleJump(context.isPressed);

            if (_hero.CollisionInfo.IsGrounded)
            {
                _hero.StateMachine.ChangeState(_hero.HeroJumpState);
            }

            if (_hero.CanDoubleJump && _hero.IsAirBorn)
            {
                _hero.StateMachine.ChangeState(_hero.HeroDoubleJumpState);
            }

            //OnPressJumpButtonPressed?.Invoke(context.isPressed);
            
            
            /*if (context.isPressed)
            {
                _hero.HandlePressJumpButton(context.isPressed);
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


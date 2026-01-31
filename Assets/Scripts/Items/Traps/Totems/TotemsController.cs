using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemsController : MonoBehaviour
    {
        private TotemTrap[] _totemsElements;
        public TotemCollisionInfo TotemCollInfo {get; private set;}
        private readonly WaitForSeconds _wait2s = new WaitForSeconds(1f);
        
        private List<TotemTrap> _totemsAtacker;

        private bool _isAttack;

        private void Awake()
        {
            _totemsElements = GetComponentsInChildren<TotemTrap>();
            TotemCollInfo = GetComponent<TotemCollisionInfo>();
        }

        private void Update()
        {
            TotemCollInfo.HeroDetection();
            TotemCollInfo.HeroAttackDetection();

            if (TotemCollInfo.HeroDetect)
                CheckTotemForFlip();

            if (TotemCollInfo.HeroAttack && !_isAttack)
            {
                StartAttack();
            }
            else
            {
                TotemsToIdleState();
            }
        }

        private void TotemsToIdleState()
        {
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            foreach (var t in snapshot)
            {
                if (t.StateMachine.CurrentState != t.HitState)
                {
                    t.StateMachine.ChangeState(t.IdleState);
                }
            }
        }

        public void CheckTotemForFlip() => StartCoroutine(TotemsFlipRoutine());
        public void StartAttack() => StartCoroutine(TotemsStartAttackRoutine());

        private IEnumerator TotemsStartAttackRoutine()
        {
            _isAttack = true;
            
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            
            var hero = TotemCollInfo != null ? TotemCollInfo.HeroTransform : null;
            
            if (hero == null) yield break;

            foreach (var t in snapshot)
            {
                yield return _wait2s;
                if (hero == null) yield break; // герой пропал — прекращаем рутину
                if (t == null) continue;

                var curState = t.StateMachine.CurrentState;

                if (curState == t.AttackState || curState == t.PauseState || curState == t.HitState) yield break;
                t.StateMachine.ChangeState(t.AttackState);
            }

            //_isAttack = false;
        }

        private IEnumerator TotemsFlipRoutine()
        {
            // Если героя нет — выходим
            var hero = TotemCollInfo != null ? TotemCollInfo.HeroTransform : null;
            if (hero == null) yield break;

            // Работаем с дублем тотемов, если какой то удалится не словим ошибку
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            
            if(snapshot.All(t => t.transform.position.x > hero.transform.position.x))

            foreach (var to in snapshot)
            {
                yield return _wait2s;
                // Повторные проверки после задержки:
                if (hero == null) yield break; // герой пропал — прекращаем рутину
                if (to == null) continue; // тотем уничтожен — пропускаем

                float heroX = hero.position.x;
                float totemX = to.transform.position.x;

                int needFlip = (totemX < heroX) ? 1 : -1;

                // Если у тебя FacingDirection может читаться с уничтоженного компонента —
                // лучше тоже обернуть проверкой:
                if (to != null && needFlip != to.FacingDirection)
                {
                    to.Flip(); // Пауза между тотемам
                }
                else
                {
                    break;
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemsController : MonoBehaviour
    {
        private TotemTrap[] _totemsElements;
        public TotemCollisionInfo TotemCollInfo {get; private set;}
        private readonly WaitForSeconds wait2S = new WaitForSeconds(2f);
        
        private List<TotemTrap> _totemsAttacker;
        private bool _isAttack = false;

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

            if (TotemCollInfo.HeroAttack)
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
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            var hero = TotemCollInfo != null ? TotemCollInfo.HeroTransform : null;
            
            if (hero == null) yield break;

            foreach (var t in snapshot)
            {
                yield return wait2S;
                if (hero == null)
                {
                    t.StateMachine.ChangeState(t.IdleState);
                    yield break; // герой пропал — прекращаем рутину
                }
                if (t == null) continue;

                var curState = t.StateMachine.CurrentState;

                t.StateMachine.ChangeState(t.AttackState);
                if (curState == t.AttackState || curState == t.PauseState || curState == t.HitState) yield break;
            }
        }

        private IEnumerator TotemsFlipRoutine()
        {
            // Если героя нет — выходим
            var hero = TotemCollInfo != null ? TotemCollInfo.HeroTransform : null;
            if (!hero) yield break;

            // Работаем с дублем тотемов, если какой то удалится не словим ошибку
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            
            if(snapshot.All(t => t.transform.position.x > hero.transform.position.x))

            foreach (var to in snapshot)
            {
                yield return wait2S;
                // Повторные проверки после задержки:
                if (hero == null) yield break; // герой пропал — прекращаем рутину
                if (to == null) continue; // тотем уничтожен — пропускаем

                float heroX = hero.position.x;
                float totemX = to.transform.position.x;

                int needFlip = (totemX < heroX) ? 1 : -1;

                // Если FacingDirection может читаться с уничтоженного компонента — лучше тоже обернуть проверкой
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
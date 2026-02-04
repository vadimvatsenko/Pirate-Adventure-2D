using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemsController : MonoBehaviour
    {
        [SerializeField] private int delayForOneTotem = 2;
        private WaitForSeconds _waitDelay;
        private TotemTrap[] _totemsElements;
        private TotemCollisionInfo _totemCollInfo;
        
        private List<TotemTrap> _totemsAttacker;
        private bool _isAttackingNow = false;

        private void Awake()
        {
            _totemsElements = GetComponentsInChildren<TotemTrap>();
            _totemCollInfo = GetComponent<TotemCollisionInfo>();
            _waitDelay = new WaitForSeconds(delayForOneTotem);
        }

        private void Update()
        {
            _totemCollInfo.HeroDetection();
            _totemCollInfo.HeroAttackDetection();

            if (_totemCollInfo.HeroDetect)
                CheckTotemForFlip();

            if (_totemCollInfo.HeroAttack && !_isAttackingNow)
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
            _isAttackingNow = true;
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            
            foreach (var t in snapshot)
            {
                t.StateMachine.ChangeState(t.AttackState);
                yield return _waitDelay;
            }
            _isAttackingNow = false;
        }

        private IEnumerator TotemsFlipRoutine()
        {
            // Если героя нет — выходим
            var hero = _totemCollInfo != null ? _totemCollInfo.HeroTransform : null;
            if (!hero) yield break;

            // Работаем с дублем тотемов, если какой то удалится не словим ошибку
            var snapshot = _totemsElements.Where(t => t != null).ToArray();
            
            if(snapshot.All(t => t.transform.position.x > hero.transform.position.x))

            foreach (var to in snapshot)
            {
                yield return _waitDelay;
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
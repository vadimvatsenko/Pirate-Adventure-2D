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
        

        private void Awake()
        {
            _totemsElements = GetComponentsInChildren<TotemTrap>();
            TotemCollInfo = GetComponent<TotemCollisionInfo>();
            
            Debug.Log(_totemsElements.Length);
            Debug.Log(TotemCollInfo);
            
            if (TotemCollInfo != null)
            {
                TotemCollInfo.HeroDetection();
            }
        }

        private void Update()
        {
            TotemCollInfo.HeroDetection();

            if (TotemCollInfo.HeroDetect)
            {
                CheckTotemForFlip();
            }
        }
        
        public void CheckTotemForFlip() => StartCoroutine(TotemsFlipRoutine());

        private IEnumerator TotemsFlipRoutine()
        {
            // Если героя нет — выходим
            var hero = TotemCollInfo != null ? TotemCollInfo.HeroTransform : null;
            if (hero == null) yield break;

            // Работаем с дублем тотемов, если какой то удалится не словим ошибку
            var snapshot = _totemsElements.Where(t => t != null).ToArray();

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
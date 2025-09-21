using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class LedgeDetection : MonoBehaviour
    {
        [SerializeField] private Creature owner;
        [SerializeField] private float radius = 0.1f;
        [SerializeField] private LayerMask whatIsClimb;

        public bool LedgeDetected { get; private set; }

        private bool _canDetect = true;  // по умолчанию можно
        private bool _prevHit;           // прошлый кадр (для edge-детекта)

        private void Update()
        {
            // Если детект отключён (во время залаза и т.п.) — выходим
            if (!_canDetect) return;

            bool hit = Physics2D.OverlapCircle(transform.position, radius, whatIsClimb);

            // фронт: стало true сейчас, а в прошлом кадре было false
            bool risingEdge = hit && !_prevHit;

            // не перезаходим в тот же стейт
            bool alreadyClimbing = owner.StateMachine.CurrentState == owner.ClimbState;

            if (risingEdge && !alreadyClimbing)
            {
                LedgeDetected = true;
                _canDetect = false; // заблокировали до явного разрешения
                owner.StateMachine.ChangeState(owner.ClimbState);
            }

            _prevHit = hit;
        }

        // Эти методы удобно вызывать из Enter/Exit соответствующего состояния
        public void EnableDetection()
        {
            _canDetect = true;
            LedgeDetected = false;
            _prevHit = false;
        }

        public void DisableDetection()
        {
            _canDetect = false;
        }

        // Если всё-таки хочешь управлять детектом триггером "ClimbPoint"
        private void OnTriggerEnter2D(Collider2D c)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("ClimbPoint"))
                DisableDetection();
        }

        private void OnTriggerExit2D(Collider2D c)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("ClimbPoint"))
                EnableDetection();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
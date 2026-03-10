using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class VisionComponent : MonoBehaviour
    {
        [Header("Hero Detection")] 
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float radius;
        [SerializeField] private UnityEvent onDetectedEvent;

        private bool isDetection;
        public bool IsDetected => isDetection;

        public void Update()
        {
            isDetection = Physics2D.CircleCast(
                transform.position, radius, Vector2.zero, 0, _layerMask);

            if (isDetection)
            {
                onDetectedEvent?.Invoke();
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = isDetection ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
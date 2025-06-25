using System;
using UnityEngine;
using UnityEngine.Events;

// далее в Hero получаем скрипт, и на основе IsTouchingLayer можем прыгать или нет
// старый, не используется сейчас, компонент Ground отключен в Hero на сцене в иерархии
namespace Components
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        private Collider2D _collider2D;
        public bool IsTouchingLayer { get; private set; }
        
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2D.IsTouchingLayers(layer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2D.IsTouchingLayers(layer);
        }
    }
}


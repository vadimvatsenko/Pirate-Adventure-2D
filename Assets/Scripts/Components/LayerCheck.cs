using UnityEngine;

// далее в Hero получаем скрипт, и на основе IsTouchingLayer можем прыгать или нет
// старый, не используется сейчас, компонент Ground отключен в Hero на сцене в иерархии
namespace Components
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        private Collider2D _collider2D;
    
        private bool _isTouchingLayer;
        public bool IsTouchingLayer
        {
            get { return _isTouchingLayer; }
            set { _isTouchingLayer = value; }
        }

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2D.IsTouchingLayers(groundLayer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsTouchingLayer = _collider2D.IsTouchingLayers(groundLayer);
        }
    }

}


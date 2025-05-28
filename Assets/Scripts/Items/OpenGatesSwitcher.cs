using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class OpenGatesSwitcher : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] bool isActivated = false;
        [SerializeField] private UnityEvent onSwitch;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Switch()
        {
            if (isActivated) return;
            
            _rb.MovePosition(_rb.position + Vector2.down * 0.075f);
            onSwitch?.Invoke();
            isActivated = true;
        }
    }
}
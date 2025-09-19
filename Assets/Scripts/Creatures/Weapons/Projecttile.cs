using Creatures.CreaturesCollisions;
using UnityEngine;

namespace Creatures.Weapons
{
    public class Projecttile : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Rigidbody2D _rigidbody;
        private int _direction;
        private void Awake()
        {
            Debug.Log(transform.lossyScale.x);
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
            _rigidbody = GetComponent<Rigidbody2D>();
            
        }
        
        private void FixedUpdate()
        {
            // если у нас есть на компоненте Rigidbody2D, то нужно его двигать через него, документация Юнити
            
            var position = _rigidbody.position;
            position.x += -_direction * speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(position);
        }
    }
}
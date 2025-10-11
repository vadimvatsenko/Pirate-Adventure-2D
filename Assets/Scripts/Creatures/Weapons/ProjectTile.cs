using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.Weapons
{
    public class ProjectTile : MonoBehaviour, IFacingDirection
    {
        [SerializeField] private float speed;
        
        private Rigidbody2D _rigidbody;
        public int FacingDirection { get; private set; }
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        
        public void SetDirection(int direction)
        {
            FacingDirection = direction;
            
            if (FacingDirection == -1)
                Flip();
        }
        
        private void FixedUpdate()
        {
            // если у нас есть на компоненте Rigidbody2D, то нужно его двигать через него, документация Юнити
            
            var position = _rigidbody.position;
            position.x += FacingDirection * speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(position);
        }
        
        public void Flip()
        {
            //FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
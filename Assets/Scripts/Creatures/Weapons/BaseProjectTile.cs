using Creatures.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.Weapons
{
    public class BaseProjectTile : MonoBehaviour, IFacingDirection
    {
        [SerializeField] protected float speed;
        [SerializeField] protected bool isFacingRight = true;
        
        protected Rigidbody2D Rigidbody;
        public int FacingDirection { get; private set; } = 1;
        
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void ChangeDirection(int direction)
        {
            if (FacingDirection != direction)
            {
                Flip();
                FacingDirection = direction;
            }
        }
        
        public void Flip()
        {
            isFacingRight = !isFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
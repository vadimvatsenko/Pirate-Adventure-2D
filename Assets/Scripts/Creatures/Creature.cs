using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Attack Power Info")] 
        [SerializeField] protected int attackPower = 1;
        
        [Header("Movement Info")] 
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;
        
        [Header("Knockback Info")] 
        [SerializeField] protected float knockbackDuration;
        [SerializeField] protected Vector2 knockbackPower; 
        protected bool isKnocked;

        [Header("Die Info")] 
        [SerializeField] protected float maxSaveHieght = 20f;
        protected bool isDead;
        protected bool isAllreadyDead;
        
        protected bool isFacingRight = true;

        protected Rigidbody2D rb;
        protected Collider2D c2d;
        protected Animator animator;
        public Rigidbody2D Rb => rb;
        public bool IsDead => isDead;
        public int FacingDirection { get; protected set; } = 1;
        
    }
}
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemCollisionInfo : MonoBehaviour
    {
        private TotemTrap[] _totemsTrapsEl;
        [Header("Hero Detection Collision Info")]
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private Vector2 checkVisionBoxSize = new Vector2(5f, 0.1f);
        
        [Header("Totem Attack Collision Info")]
        [SerializeField] private Vector2 checkAttackBoxSize = new Vector2(5f, 0.1f);

        private Transform _heroTransform;
        public Transform HeroTransform => _heroTransform;
        public bool HeroDetect { get; private set; }
        public bool HeroAttack {get; private set;}
        
        private void Awake()
        {
            _totemsTrapsEl = GetComponents<TotemTrap>();
        }
        
        public void HeroDetection()
        {
            RaycastHit2D hit = 
                Physics2D.BoxCast(
                    transform.position, 
                    checkVisionBoxSize, 
                    0, 
                    Vector2.one, 
                    0, 
                    whatIsHero);
            
            if (hit.collider != null)
            {
                HeroDetect = true;
                _heroTransform = hit.collider.transform;
            }
            else
            {
                HeroDetect =  false;
            }
        }
        
        public void HeroAttackDetection()
        {
            RaycastHit2D hit = 
                Physics2D.BoxCast(
                    transform.position, 
                    checkAttackBoxSize, 
                    0, 
                    Vector2.one, 
                    0, 
                    whatIsHero);
            
            
            if (hit.collider != null)
            {
                HeroAttack = true;
            }
            else
            {
                HeroAttack =  false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, checkVisionBoxSize);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, checkAttackBoxSize);
        }
    }
}
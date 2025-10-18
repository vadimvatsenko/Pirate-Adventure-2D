using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemCollisionInfo : MonoBehaviour
    {
        private TotemTrap[] _totemsTrapsEl;
        [Header("Hero Detection Collision Info")]
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private Vector2 checkBoxSize = new Vector2(5f, 0.1f);

        private Transform _heroTransform;
        public Transform HeroTransform => _heroTransform;
        public bool HeroDetect { get; private set; }
        
        private void Awake()
        {
            _totemsTrapsEl = GetComponents<TotemTrap>();
        }
        
        public void HeroDetection()
        {
            RaycastHit2D hit = 
                Physics2D.BoxCast(
                    transform.position, 
                    checkBoxSize, 
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, checkBoxSize);
        }
    }
}
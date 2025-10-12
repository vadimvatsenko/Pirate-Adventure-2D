using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemCollisionInfo : MonoBehaviour
    {
        private BasicCreature _totem;
        [Header("Hero Detection Collision Info")]
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private float distanceToHero;
        [SerializeField] private float attackDistance;
        [SerializeField] private Transform hero;

        private void Awake()
        {
            _totem = GetComponent<BasicCreature>();
        }
        
        public RaycastHit2D HeroDetection()
        {
            RaycastHit2D hit = 
                Physics2D.Raycast(_totem.transform.position, 
                    Vector2.right * _totem.FacingDirection, 
                    distanceToHero, 
                    whatIsHero);

            if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                return default;
            
            return hit;
        }

        private void OnDrawGizmos()
        {
            if (_totem != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_totem.transform.position,
                    new Vector2(_totem.transform.position.x + (_totem.FacingDirection * attackDistance), 
                        _totem.transform.position.y));
            }
        }
    }
}
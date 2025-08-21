using System;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Components.Triggers
{
    public class TriggerClimb : MonoBehaviour
    {
        [SerializeField] private string targetTag;
        private Hero _hero;
        private BoxCollider2D _climbingBox;
        private SpriteRenderer _climbingRenderer;
        private readonly Color _startColor = new Color(1f, 1f, 1f, 0f);
        private readonly Color _endColor = new Color(1f, 1f, 1f, 1f);
        
        private void Start()
        {
            _hero = GetComponentInParent<Hero>();
            _climbingBox = GetComponents<BoxCollider2D>()[0];
            _climbingRenderer = GetComponentInChildren<SpriteRenderer>();
            
            _climbingRenderer.color = _startColor;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                _climbingBox.enabled = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                _climbingBox.enabled = true;
                _climbingRenderer.color = _startColor;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log(other.gameObject.layer);
            if (other.gameObject.CompareTag(targetTag))
            {
                _climbingRenderer.color = _endColor;
                _hero.StateMachine.ChangeState(_hero.ClimbState);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                _climbingRenderer.color = _startColor;
                _hero.StateMachine.ChangeState(_hero.ClimbState);
            }
        }
    }
}
using System;
using System.Collections;
using Creatures.CreaturesStateMachine;
using Model;
using UnityEngine;

namespace Creatures
{
    public class CreatureAnimController : MonoBehaviour
    {
        // что тут происходит, перевод string в hash
        
        [Header("Animator Controllers")]
        /*[SerializeField] private RuntimeAnimatorController withoutArmor;
        [SerializeField] private RuntimeAnimatorController withArmor;*/
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        
        public Animator CreatureAnimator { get; private set; }
        private Creature _creature;
        private CreatureCollisionInfo _creatureCollisionInfo;
        //private GameSession _gameSession; // ++
        public event Action OnIsArmed;
        
        // Colors
        private readonly Color _startColor = new Color(1f, 1f, 1f, 0f);
        private readonly Color _endColor = new Color(1f, 1f, 1f, 1f);
        
        private void Awake()
        {
            _creature = GetComponent<Creature>();
            CreatureAnimator = GetComponentInChildren<Animator>();
            _creatureCollisionInfo = GetComponent<CreatureCollisionInfo>();
            //_gameSession = FindObjectOfType<GameSession>();

            /*if (_gameSession != null) // ++
            {
                UpdateArmedState();
            }*/
        }

        private void Start()
        {
            //playerSpriteRenderer.color = _startColor;
            //ShowCreature();
        }

        //private void ShowCreature() => StartCoroutine(ShowPlayerCoroutine(_startColor, _endColor));
        //public void HideCreature(float duration) => StartCoroutine(ShowPlayerCoroutine(_endColor, _startColor, duration));

        /*private IEnumerator ShowPlayerCoroutine(Color col1, Color col2, float duration = 1)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                playerSpriteRenderer.color = 
                    Color.Lerp(col1, col2, elapsed / duration);
                yield return null;
            }
        }*/
        
        /*public void HandleAnimation()
        {
            Vector2 velocityNormalized = _creature.Rb2D.velocity.normalized;
            CreatureAnimator.SetFloat(XVelocityKey, velocityNormalized.x);
            CreatureAnimator.SetFloat(YVelocityKey, velocityNormalized.y);
            CreatureAnimator.SetBool(IsGroundedKey, _creatureCollisionInfo.IsGrounded);
        }*/
        
        /*public void ChangeArmedState()
        {
            _gameSession.PlayerData.isArmed = !_gameSession.PlayerData.isArmed;
            UpdateArmedState();
            OnIsArmed?.Invoke();
        }*/

        /*private void UpdateArmedState()
        {
            CreatureAnimator.runtimeAnimatorController 
                = _gameSession.PlayerData.isArmed ? withArmor : withoutArmor;
        }*/

        /*public void SetAttackAnimation()
        {
            CreatureAnimator.SetTrigger(AttackKey);
        }

        public void SetKnockbackAnimation()
        {
            CreatureAnimator.SetTrigger(Knockback);
        }
        
        public void SetDieAnimation()
        {
            CreatureAnimator.SetTrigger(Die);
        }*/
    }
}
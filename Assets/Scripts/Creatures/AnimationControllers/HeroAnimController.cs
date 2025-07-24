using System;
using System.Collections;
using Model;
using UnityEngine;

namespace Creatures.AnimationControllers
{
    public class HeroAnimController : CreatureAnimController
    {
        [Header("Animator Controllers")]
        [SerializeField] private RuntimeAnimatorController withoutArmor;
        [SerializeField] private RuntimeAnimatorController withArmor;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        
        private GameSession _gameSession;
        public event Action OnIsArmed;
        
        // Colors
        private readonly Color _startColor = new Color(1f, 1f, 1f, 0f);
        private readonly Color _endColor = new Color(1f, 1f, 1f, 1f);

        protected override void Awake()
        {
            base.Awake();

            CreatureAnim.runtimeAnimatorController = withArmor;
            
            _gameSession = FindObjectOfType<GameSession>();

            if (_gameSession != null) UpdateArmedState();
        }

        private void Start()
        {
            playerSpriteRenderer.color = _startColor;
            ShowCreature();
        }
        
        public void ChangeArmedState()
        {
            _gameSession.PlayerData.isArmed = !_gameSession.PlayerData.isArmed;
            UpdateArmedState();
            OnIsArmed?.Invoke();
            Cre.StateMachine.Initialize(Cre.MoveState);
        }
        
        private void UpdateArmedState()
        {
            Debug.Log("Updating armed state");
            CreatureAnim.runtimeAnimatorController
                = _gameSession.PlayerData.isArmed ? withArmor : withoutArmor;
        }
        
        private void ShowCreature() => StartCoroutine(ShowPlayerCoroutine(_startColor, _endColor));
        public void HideCreature(float duration) => StartCoroutine(ShowPlayerCoroutine(_endColor, _startColor, duration));

        private IEnumerator ShowPlayerCoroutine(Color col1, Color col2, float duration = 1)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                playerSpriteRenderer.color =
                    Color.Lerp(col1, col2, elapsed / duration);
                yield return null;
            }
        }
    }
}
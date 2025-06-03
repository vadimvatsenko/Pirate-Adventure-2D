using System;
using Components.SpriteAnimator.AnimationControllers;
using DefaultNamespace.Model;
using PlayerFolder;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class IsPlayerWithArmor : MonoBehaviour
    {
        [SerializeField] private GameObject armorObjectForUI;
        [SerializeField] private PlayerAnimController playerAnimController;
        private GameSession _gameSession;

        private void Awake()
        {
            if (playerAnimController != null)
            {
                playerAnimController.OnIsArmed += UpdatePlayerWithArmorStatus;
            }
        }

        private void OnDisable()
        {
            if (playerAnimController != null)
            {
                playerAnimController.OnIsArmed -= UpdatePlayerWithArmorStatus;
            }
        }

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            
            if (_gameSession != null)
            {
                UpdatePlayerWithArmorStatus();
            }
        }
        private void UpdatePlayerWithArmorStatus()
        {
            
            if (_gameSession.PlayerData.isArmed)
            {
                armorObjectForUI.SetActive(true);
                
            }
            else
            {
                armorObjectForUI.SetActive(false);
            }
        }
    }
}
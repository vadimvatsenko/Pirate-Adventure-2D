using Model;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.PlayerControllers
{
    public class IsPlayerWithArmor : MonoBehaviour
    {
        [SerializeField] private GameObject armorObjectForUI;
        [FormerlySerializedAs("playerAnimController")] [SerializeField] private CratureAnimController cratureAnimController;
        private GameSession _gameSession;

        private void Awake()
        {
            if (cratureAnimController != null)
            {
                cratureAnimController.OnIsArmed += UpdateCratureWithArmorStatus;
            }
        }

        private void OnDisable()
        {
            if (cratureAnimController != null)
            {
                cratureAnimController.OnIsArmed -= UpdateCratureWithArmorStatus;
            }
        }

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            
            if (_gameSession != null)
            {
                UpdateCratureWithArmorStatus();
            }
        }
        private void UpdateCratureWithArmorStatus()
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
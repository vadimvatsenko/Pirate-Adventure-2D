using Creatures;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.PlayerControllers
{
    public class IsPlayerWithArmor : MonoBehaviour
    {
        [SerializeField] private GameObject armorObjectForUI;
        [FormerlySerializedAs("cratureAnimController")] [SerializeField] private CreatureAnimController creatureAnimController;
        private GameSession _gameSession;

        private void Awake()
        {
            if (creatureAnimController != null)
            {
                creatureAnimController.OnIsArmed += UpdateCreatureWithArmorStatus;
            }
        }

        private void OnDisable()
        {
            if (creatureAnimController != null)
            {
                creatureAnimController.OnIsArmed -= UpdateCreatureWithArmorStatus;
            }
        }

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            
            if (_gameSession != null)
            {
                UpdateCreatureWithArmorStatus();
            }
        }
        private void UpdateCreatureWithArmorStatus()
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
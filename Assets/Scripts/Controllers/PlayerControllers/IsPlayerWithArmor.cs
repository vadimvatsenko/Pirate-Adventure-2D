using Creatures;
using Creatures.AnimationControllers;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.PlayerControllers
{
    public class IsPlayerWithArmor : MonoBehaviour
    {
        [SerializeField] private GameObject armorObjectForUI;
        [FormerlySerializedAs("creatureAnimController")] [FormerlySerializedAs("cratureAnimController")] [SerializeField] private CreatureArmAnimController creatureArmAnimController;
        private GameSession _gameSession;

        private void Awake()
        {
            if (creatureArmAnimController != null)
            {
                //creatureAnimController.OnIsArmed += UpdateCreatureWithArmorStatus;
            }
        }

        private void OnDisable()
        {
            if (creatureArmAnimController != null)
            {
                //creatureAnimController.OnIsArmed -= UpdateCreatureWithArmorStatus;
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
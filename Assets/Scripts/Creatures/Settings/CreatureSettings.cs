using UnityEngine;

namespace Creatures.Settings
{
    [CreateAssetMenu(fileName = "Creature Settings", menuName = "Creature/Settings")]
    public class CreatureSettings : ScriptableObject
    {
        [Header("Buffer Jump")] 
        [SerializeField] private float bufferJumpWindow = 0.25f;
        private float _bufferJumpActivated = -1;
        public float BufferJumpWindow => bufferJumpWindow;
        public float BufferJumpActivated => _bufferJumpActivated;
        
        [Header("Coyote Jump")] 
        [SerializeField] private float coyoteJumpWindow = 0.5f; // Окно буфера (сколько секунд допустимо)
        private float _coyoteJumpActivated = -1; 
        public float CoyoteJumpWindow => coyoteJumpWindow;
        public float CoyoteJumpActivated => _coyoteJumpActivated;
    }
}
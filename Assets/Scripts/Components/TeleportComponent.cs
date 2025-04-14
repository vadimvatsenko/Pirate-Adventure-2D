using System.Collections;
using Components.Teleport;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class TeleportComponent :MonoBehaviour
    {
        [SerializeField] private Transform destTransform;
        [SerializeField] private CanvasGroup flashCanvas; // Белый Canvas поверх камеры
        [SerializeField] private float flashDuration = 0.25f;
        
        [SerializeField] private TeleportEvent onStartTeleport;
        [SerializeField] private TeleportEvent onEndTeleport;
        public void Teleport(GameObject target) => StartCoroutine(TeleportPlayerRoutine(target));
        
        private IEnumerator TeleportPlayerRoutine(GameObject target)
        {
            if (target != null && target.GetComponent<Player>() != null)
            {
                    
                    onStartTeleport?.Invoke(target.transform.position);
                    
                    target.GetComponent<Player>().Teleport(destTransform.position);
                    // Задержка перед перемещением
                    yield return new WaitForSeconds(0.5f);
                    
                    // Повторная вспышка (появление)
                    
                    onEndTeleport?.Invoke(destTransform.position);
            }
        }
    }
}
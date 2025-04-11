using System.Collections;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class TeleportComponent :MonoBehaviour
    {
        [SerializeField] private Transform destTransform;
        [SerializeField] private CanvasGroup flashCanvas; // Белый Canvas поверх камеры
        [SerializeField] private float flashDuration = 0.25f;
        public void Teleport(GameObject target) => StartCoroutine(TeleportPlayerRoutine(target));
        
        private IEnumerator TeleportPlayerRoutine(GameObject target)
        {
            if (target != null && target.GetComponent<Player>() != null)
            {
                    // Вспышка
                    yield return StartCoroutine(Flash());

                    Player p = target.GetComponent<Player>();
                    p.Teleport(destTransform.position);
                    // Задержка перед перемещением
                    yield return new WaitForSeconds(0.5f);
                    
                    // Повторная вспышка (появление)
                    yield return StartCoroutine(Flash());
            }
            
        }
        
        private IEnumerator Flash()
        {
            float t = 0f;
            while (t < flashDuration)
            {
                t += Time.deltaTime;
                flashCanvas.alpha = Mathf.Lerp(0, 1, t / flashDuration);
                yield return null;
            }

            t = 0f;
            while (t < flashDuration)
            {
                t += Time.deltaTime;
                flashCanvas.alpha = Mathf.Lerp(1, 0, t / flashDuration);
                yield return null;
            }
        }
    }
}
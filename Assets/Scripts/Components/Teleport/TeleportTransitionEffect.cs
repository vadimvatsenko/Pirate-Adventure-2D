using System.Collections;
using UnityEngine;

namespace Components.Teleport
{
    public class TeleportTransitionEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float transitionDuration;

        public void TeleportWithIris(Vector3 dest)
        {
            StartCoroutine(IrisTeleportRoutine(dest));
        }

        private IEnumerator IrisTeleportRoutine(Vector3 destination)
        {
            // Центрируем маску на игрока
            rectTransform.position = Camera.main.WorldToScreenPoint(playerTransform.position);

            // Сжимаем (закрытие)
            yield return StartCoroutine(IrisScale(3f, 0f));

            // Телепорт
            playerTransform.position = destination;

            // Переставляем маску в новую точку
            rectTransform.position = Camera.main.WorldToScreenPoint(playerTransform.position);

            // Расширяем (открытие)
            yield return StartCoroutine(IrisScale(0f, 3f));
        }

        private IEnumerator IrisScale(float from, float to)
        {
            float elapsed = 0f;
            while (elapsed < transitionDuration)
            {
                float t = elapsed / transitionDuration;
                float scale = Mathf.SmoothStep(from, to, t); // плавно
                rectTransform.localScale = new Vector3(scale, scale, 1);
                elapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = new Vector3(to, to, 1);
        }
    }
    }

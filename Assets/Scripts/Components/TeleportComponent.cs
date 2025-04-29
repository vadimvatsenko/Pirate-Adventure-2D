using System;
using System.Collections;
using Components.Teleport;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] GameObject teleportPrefab;
        [SerializeField] private Transform destTransform;
        
        [SerializeField] private TeleportEvent onStartTeleport;
        [SerializeField] private TeleportEvent onEndTeleport;
        public void Teleport(GameObject target) => StartCoroutine(TeleportPlayerRoutine(target));

        private IEnumerator TeleportPlayerRoutine(GameObject target)
        {
            if (target != null && target.GetComponent<Player>() != null)
            {
                onStartTeleport?.Invoke(target.transform.position);

                Player player = target.GetComponent<Player>();
                
                player.Teleport(destTransform.position, () => CreateTeleport());
                
                onEndTeleport?.Invoke(destTransform.position);
                
                yield return null;
            }
        }

        private void CreateTeleport()
        {
            GameObject tel = Instantiate(teleportPrefab, destTransform);
            ParticleSystem ps = tel.GetComponent<ParticleSystem>();
            var col = ps.colorOverLifetime;
            col.enabled = true;
            Gradient grad = new Gradient();
                
            grad.SetKeys( new GradientColorKey[]
                {
                    new GradientColorKey(Color.blue, 0.0f), 
                    new GradientColorKey(Color.white, 1.0f)
                }, 
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1.0f, 0.0f), 
                    new GradientAlphaKey(0.0f, 1.0f)
                });
                
            col.color = grad;
            Destroy(tel, 1f);
        }
    }
}
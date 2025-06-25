using System;
using System.Collections;
using Components.Teleport;
using Creatures;
using UnityEngine;

namespace Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] GameObject teleportPrefab;
        [SerializeField] private Transform destTransform;
        
        [SerializeField] private TeleportEvent onStartTeleport;
        [SerializeField] private TeleportEvent onEndTeleport;
        
        [Header("Teleport Settings")]
        [SerializeField] private float durationInTeleport = 1f; // длительность подъема
        [SerializeField] float targetInTeleportHeight = 1f; // на сколько поднять
        [SerializeField] private float elapsedInTeleport = 0f;
        private float _startInTeleportY;
        private float _endInTeleportY;
        [Space] 
        [SerializeField] float rotationAmountInTeleport = 360f;
        private float _startRotationInTeleportZ;
        private float _endRotationInTeleportZ;
        [Space] 
        [SerializeField] private float startScaleInTeleport = 1f;
        [SerializeField] private float endScaleInTeleport = 0.1f;
        private bool _isTeleporting;
        
        public void Teleport(GameObject target) => StartCoroutine(TeleportPlayerRoutine(target));

        private IEnumerator TeleportPlayerRoutine(GameObject target)
        {
            if (target != null && target.GetComponent<Creature_OLD>() != null)
            {
                onStartTeleport?.Invoke(target.transform.position);

                Creature_OLD creaturesOld = target.GetComponent<Creature_OLD>();
                
                //creatures.Teleport(destTransform.position, () => CreateTeleport());
                
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
        
        public void Teleport(Vector3 targetPosition, Action OnDestination) => StartCoroutine(SmoothLift(targetPosition, OnDestination));
        
        private IEnumerator SmoothLift(Vector3 targetPosition, Action OnDestination)
        {
            _isTeleporting = true;
            elapsedInTeleport = 0f; // сброс таймера
            
            _startInTeleportY = transform.position.y;
            _endInTeleportY = _startInTeleportY + targetInTeleportHeight;
            
            float startRotationInTeleportY = transform.eulerAngles.y;
            float endRotationInTeleportY = startRotationInTeleportY;
            
            _startRotationInTeleportZ = transform.rotation.eulerAngles.z;
            _endRotationInTeleportZ = _startRotationInTeleportZ + rotationAmountInTeleport;

            while (elapsedInTeleport < durationInTeleport)
            {
                float newY = Mathf.Lerp(_startInTeleportY, _endInTeleportY, elapsedInTeleport / durationInTeleport);
                float newRotationZ = 
                    Mathf.Lerp(_startRotationInTeleportZ, _endRotationInTeleportZ, elapsedInTeleport / durationInTeleport);
                float newScale = Mathf.Lerp(startScaleInTeleport, endScaleInTeleport, elapsedInTeleport / durationInTeleport);
                
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                transform.rotation = Quaternion.Euler(0f, 0f, newRotationZ);
                transform.localScale = new Vector3(newScale, newScale, newScale);
                
                elapsedInTeleport += Time.deltaTime;
                yield return null;
            }
            
            OnDestination?.Invoke();
            
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, endRotationInTeleportY, 0f);
            transform.localScale = new Vector3(startScaleInTeleport, startScaleInTeleport, startScaleInTeleport);
            _isTeleporting = false;
        }
        
    }
}
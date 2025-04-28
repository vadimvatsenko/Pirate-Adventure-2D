using System.Collections;
using Components.Teleport;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class TeleportComponent : MonoBehaviour
    {
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
                player.Teleport(destTransform.position);
                
                yield return new WaitForSeconds(0.5f);
                
                onEndTeleport?.Invoke(destTransform.position);
            }
        }
    }
}
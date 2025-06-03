using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Utils
{
    public class RandomSpawner : MonoBehaviour
    {
        [Header("Spawn bound:")]
        [SerializeField] private float sectorAngle = 60;
        [SerializeField] private float sectorRotation;
        
        [SerializeField] private float waitTime = 0.1f;
        [SerializeField] private float speed = 6f;
        [SerializeField] private float itemPerBurst = 2f;
        [SerializeField] private float numParticles = 200;
        
        private Coroutine _routine;
        
        [SerializeField] private UnityEvent OnFinishedSpawning;
        
        public void StartDrop(GameObject[] items)
        {
            TryStopRoutine();
            _routine = StartCoroutine(StartSpawn(items));
        }

        private IEnumerator StartSpawn(GameObject[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Spawn(particles[i]);
                yield return new WaitForSeconds(waitTime);
            }
            OnFinishedSpawning?.Invoke();
        }

        private void Spawn(GameObject particle)
        {
            GameObject obj = Instantiate(particle, transform.position, Quaternion.identity);
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            
            float randomAngle = Random.Range(0, sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            rb.AddForce(forceVector * speed, ForceMode2D.Impulse);
        }

        private Vector2 AngleToVectorInSector(float angle)
        {
            float angleMiddleDelta = (180f - sectorRotation - sectorAngle) / 2f;
            return GetUnitOnCircle(angle + angleMiddleDelta);
        }

        private Vector3 GetUnitOnCircle(float angleDegrees)
        {
            var angleRadians = angleDegrees * Mathf.PI / 180f;
            
            float x = Mathf.Cos(angleRadians);
            float y = Mathf.Sin(angleRadians);
            
            return new Vector2(x, y);
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 pos = transform.position;
            
            float middleAngleDelta = (180f - sectorRotation - sectorAngle) / 2f;
            Vector3 rightBound = GetUnitOnCircle(middleAngleDelta);
            Handles.DrawLine(pos, pos + rightBound);
            
            Vector3 leftBound = GetUnitOnCircle(middleAngleDelta + sectorAngle);
            Handles.DrawLine(pos, pos + leftBound);
            Handles.DrawWireArc(pos, Vector3.forward, rightBound, sectorAngle, sectorRotation);
            
            Handles.color = new Color(1, 1, 1, 0.1f);
            Handles.DrawSolidArc(pos, Vector3.forward, rightBound, sectorAngle, sectorRotation);
        }

        private void OnDisable()
        {
            TryStopRoutine();
        }
        
        private void OnDestroy()
        {
            TryStopRoutine();
        }
        
        private void TryStopRoutine()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Components
{
    public class GameObjectDropper : MonoBehaviour
    {
        // используется для выброса объектов
        [SerializeField] private DroppedObjectEntry[] droppedObjects;
        
        [Range(0.1f, 10f)]
        [SerializeField] private float spreadForce = 1.5f;
        [Range(0.1f, 10f)]
        [SerializeField] private float spreadRadius = 1.5f;
        [SerializeField] private DropperDirection dropperDirection = DropperDirection.Top;
        [SerializeField] private bool destroyOnFinish = true;
        
        private List<GameObject> objectsToSpawn = new List<GameObject>();
        private Vector2 _currentDirection;

        private void Awake()
        {
            _currentDirection = GetDirection(dropperDirection);
            
            foreach (var obj in droppedObjects)
            {
                for (int i = 0; i < obj.amount; i++)
                {
                    objectsToSpawn.Add(obj.prefab);
                }
            }
            
        }

        public void DropObject()
        {
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                GameObject go = Instantiate(objectsToSpawn[i], transform.position, Quaternion.identity);
                Collider2D c2d = go.GetComponent<Collider2D>();
                Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
                
                if (c2d != null)
                {
                    c2d.isTrigger = false;
                    c2d.enabled = false;
                }
                
                if (rb2d != null)
                {
                    Vector2 direction = (_currentDirection + Random.insideUnitCircle * spreadRadius).normalized;
                    rb2d.AddForce(direction * spreadForce, ForceMode2D.Impulse);
                }
                
                StartCoroutine(EnableColliderAfterDelay(go, c2d, rb2d));
            }
        }
        
        private IEnumerator EnableColliderAfterDelay(GameObject go, Collider2D c2d, Rigidbody2D rb2d)
        {
            
            yield return new WaitForSeconds(0.5f);
            
            if (rb2d != null)
                rb2d.gravityScale = 1f;
            
            if (c2d != null)
            {
                c2d.isTrigger = true;
                c2d.enabled = true;
            }
            
            yield return new WaitForSeconds(3f);
            
            if (destroyOnFinish)
            {
                Destroy(go);
            }
        }

        private Vector3 GetDirection(DropperDirection dir)
        {
            switch (dir)
            {
                case DropperDirection.Left:
                    return Vector3.left;
                case DropperDirection.Right:
                    return Vector3.right;
                case DropperDirection.Top:
                    return Vector3.up;
                case DropperDirection.Bottom:
                    return Vector3.down;
            }
            return Vector3.zero;
        }
    }
}
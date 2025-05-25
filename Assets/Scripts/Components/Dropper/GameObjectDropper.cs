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
        
        [Range(0.1f, 1f)] 
        [SerializeField] private float gravity = 0.25f;
        
        [SerializeField] private DropperDirection dropperDirection = DropperDirection.Top;
        [SerializeField] private bool destroyOnFinish = true;
        
        private List<GameObject> objectsToSpawn = new List<GameObject>();
        private Vector2 _currentDirection;

        private void Awake()
        {
            Debug.Log(droppedObjects.Length);
            _currentDirection = GetDirection(dropperDirection);
            
            /*foreach (var obj in droppedObjects)
            {
                Debug.Log(obj);
            }*/
        }

        public void DropObject()
        {
            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                Collider2D collider = objectsToSpawn[i].GetComponent<Collider2D>();
                Rigidbody2D rb = objectsToSpawn[i].GetComponent<Rigidbody2D>();

                if (collider != null  && rb != null)
                {
                    GameObject go = Instantiate(objectsToSpawn[i], transform.position, Quaternion.identity);
                    rb.gravityScale = gravity;
                    Vector2 direction = (_currentDirection + Random.insideUnitCircle * spreadRadius).normalized;
                    rb.AddForce(_currentDirection * spreadForce, ForceMode2D.Impulse);
                }
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
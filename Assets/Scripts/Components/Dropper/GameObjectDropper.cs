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
                    StartCoroutine(SwitchTrigers(c2d));
                }
                
                if (rb2d != null)
                {
                    Vector2 direction = (_currentDirection + Random.insideUnitCircle * spreadRadius).normalized;
                    rb2d.gravityScale = gravity;
                    rb2d.AddForce(direction * spreadForce, ForceMode2D.Impulse);
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

        private IEnumerator SwitchTrigers(Collider2D c2d)
        {
            //c2d.isTrigger = false;
            c2d.enabled = false;
            yield return new WaitForSeconds(1f);
            Debug.Log(c2d.isTrigger);
            //c2d.isTrigger = true;
            Debug.Log(c2d);
            c2d.enabled = true;
        }
    }
}
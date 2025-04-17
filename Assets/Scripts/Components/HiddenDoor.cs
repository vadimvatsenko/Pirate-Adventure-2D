using System.Collections;
using UnityEngine;

namespace Components
{
    public class HiddenDoor : MonoBehaviour
    {
        [SerializeField] private bool isOpen;
        [SerializeField] private Transform[] points;
        
        public void OpenOrClose()
        {
            isOpen = !isOpen;
            transform.position = isOpen ? points[0].position : points[1].position;
        }
    }
}
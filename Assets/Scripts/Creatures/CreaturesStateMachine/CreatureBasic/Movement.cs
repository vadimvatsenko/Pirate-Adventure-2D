using UnityEngine;

public class Movement : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position += Vector3.right * (2 *  Time.deltaTime);
    }
}

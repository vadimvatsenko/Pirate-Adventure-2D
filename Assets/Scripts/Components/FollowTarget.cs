using UnityEngine;

// старый скрипт слежения за игроком
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offSet;

    private void LateUpdate()
    {
        var destination = new Vector3(target.position.x, target.position.y, transform.position.z);
        
        // Интерполяция, плавное перемещение камеры, от точки а до б 
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * offSet);
        //Mathf.Lerp - интерполяция для float
    }
}

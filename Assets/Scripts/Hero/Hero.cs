using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed;
    private float _direction;
    
    public void SetDirection(float dir)
    {
        _direction = dir;
    }

    public void Update()
    {
        if (_direction != 0)
        {
            
            // Time.deltaTime - возвращает нам время последнего кадра, 
            // Time.deltaTime добавлен в расчет delta для расчёта плавного передвижения игрока,
            // Фрейм может быть не ровным, в зависимости от загрузки процессора рассчитывается время кадра,
            // время кадра может быть всегда разным
            
            float delta = _direction * speed * Time.deltaTime; // смещение объекта - delta
            float newXpos = transform.position.x + delta;
            transform.position = new Vector3(newXpos, transform.position.y, transform.position.z);
        }
    }

    public void Fire()
    {
        Debug.Log("Fire");
    }
}

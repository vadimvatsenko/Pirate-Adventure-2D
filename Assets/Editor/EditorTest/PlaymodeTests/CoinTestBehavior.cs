using PlayerFolder;
using UnityEngine;
using UnityEngine.TestTools;

// добавили интерфейс IMonoBehaviourTest
public class CoinTestBehavior : MonoBehaviour, IMonoBehaviourTest
{
    public bool IsTestFinished { get; private set; } // сюда нужно записать true, когда мы завершили тестирование
    private float _timeToLive = 1.5f;
    
    // private GameObject _player;--
    private Player _player; // ++
    
    private void Start()
    {
        // _player = GameObject.FindGameObjectWithTag("Player"); --
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        _timeToLive -= Time.deltaTime;
        
        if (_timeToLive < 0) IsTestFinished = true;
        
        // тут запутано, хотя мы получаем GameObject, не как Player
        // каким-то образом бы можем послать в скрипт название метода, и что передаем в метод
        
        //_player.SendMessage("SetDirection", 1); // --
        
        // теперь могу напряму обращатся вк методам.
        _player.SetDirection(1); // ++
    }
}

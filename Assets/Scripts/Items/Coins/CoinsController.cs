using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    [SerializeField] private EnterPoint enterPoint;
    private int _coinsScore = 0;

    private void Start()
    {
        DisplayCoins();
    }

    public void AddCoins(int cost)
    {
        _coinsScore += cost;
        DisplayCoins();
        
        if (_coinsScore >= 100)
        {
            enterPoint.ReloadLevelComponent.ReloadLevel();
        }
    }

    private void DisplayCoins()
    {
        Debug.Log($"Score: {_coinsScore}");
    }
}

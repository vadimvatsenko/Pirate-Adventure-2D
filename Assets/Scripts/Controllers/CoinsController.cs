using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

public class CoinsController
{
    private ReloadLevelController _reloadLevelController;
    private ConsoleView _consoleView;
    private int _coinsScore = 0;

    public CoinsController(ReloadLevelController reloadLevelController, ConsoleView consoleView)
    {
        _reloadLevelController = reloadLevelController;
        _consoleView = consoleView;
    }
    
    public void AddCoins(int cost)
    {
        _coinsScore += cost;
        DisplayCoins();
        
        if (_coinsScore >= 100)
        {
            _reloadLevelController.ReloadLevel();
        }
    }

    private void DisplayCoins()
    {
        _consoleView.Show("Score", _coinsScore.ToString());
    }
}

using System;
using Creatures;
using Creatures.CreaturesStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfLevel : MonoBehaviour
{
    [SerializeField] private Creature player;
    
    public void OnEnable()
    {
        //player.SubscribeOnCreatureDeath(ReloadLevel);
    }

    private void OnDisable()
    {
        //player.UnsubscribeOnCreatureDeath(ReloadLevel);
    }
    
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

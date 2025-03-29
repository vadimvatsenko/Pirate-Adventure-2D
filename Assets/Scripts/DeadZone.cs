using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hero hero = collision.GetComponent<Hero>();
        Barrel barrel = collision.GetComponent<Barrel>();
        if(hero != null) hero.Die();
        if(barrel != null) barrel.Destroy();
        
    }
}

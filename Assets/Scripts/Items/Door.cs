using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isDoorOpen;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        /*if (isDoorOpen)
        {
            _animator.SetFloat("isOpen", -1);
        }
        else
        {
            _animator.SetFloat("isOpen", 1);
        }*/
    }
}

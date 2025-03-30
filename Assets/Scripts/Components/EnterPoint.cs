using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    private ReloadLevelComponent _reloadLevelComponent;
    public ReloadLevelComponent ReloadLevelComponent => _reloadLevelComponent;
    private void Awake()
    {
        _reloadLevelComponent = new ReloadLevelComponent();
    }
}

using Cinemachine;
using PlayerFolder;
using UnityEngine;

public class TempCameraFollowHero : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        var player = FindObjectOfType<Player>().transform; // временно
        if(player != null) _cam.Follow = player; // временно
    }
}

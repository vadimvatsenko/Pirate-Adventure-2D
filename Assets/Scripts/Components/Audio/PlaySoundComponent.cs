using System;
using UnityEngine;

namespace Components.Audio
{
    public class PlaySoundComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioData[] audioData;
        
        public void Play(string id)
        {
            foreach (var data in audioData)
            {
                if (data.Id == id)
                {
                    audioSource.PlayOneShot(data.AudioClip);
                    break;
                }
            }
        }
    }

    [Serializable]
    public class AudioData
    {
        [SerializeField] private string id;
        [SerializeField] private AudioClip audioClip;
        
        public string Id => id;
        public AudioClip AudioClip => audioClip;
    }
}
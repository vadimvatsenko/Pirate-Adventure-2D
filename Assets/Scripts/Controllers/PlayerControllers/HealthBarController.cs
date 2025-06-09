using Components.HealthComponentFolder;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] PlayerHealthComponent healthComponent;
        [SerializeField] private Transform heartsParent;
        [SerializeField] GameObject heartContainerPrefab;
        
        private GameObject[] _heartContainers;
        private Image[] _heartFills;
        private GameSession _gameSession;
        
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            
            if (_gameSession == null) return;
            
            _heartContainers = new GameObject[_gameSession.PlayerData.maxTotalHearts];
            _heartFills = new Image[_gameSession.PlayerData.maxTotalHearts];
            
            InstantiateHeartContainers();
            UpdateHeartsHUD();
        }

        private void OnEnable()
        {
            healthComponent.OnHealthChange += UpdateHeartsHUD;
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthChange -= UpdateHeartsHUD;
        }

        public void UpdateHeartsHUD()
        {
            
            SetHeartContainers();
            SetFilledHearts();
        }

        void SetHeartContainers()
        {
            for (int i = 0; i < _heartContainers.Length; i++)
            {
                if (i < _gameSession.PlayerData.maxHealth)
                {
                    _heartContainers[i].SetActive(true);
                }
                else
                {
                    _heartContainers[i].SetActive(false);
                }
            }
        }

        void SetFilledHearts()
        {
            if(_gameSession == null) return;
            
            for (int i = 0; i < _gameSession.PlayerData.maxHealth; i++)
            {
                if (i < _gameSession.PlayerData.health)
                {
                    _heartFills[i].fillAmount = 1;
                }
                else
                {
                    _heartFills[i].fillAmount = 0;
                }
            }

            if (_gameSession.PlayerData.health % 1 != 0)
            {
                int lastPos = Mathf.FloorToInt(_gameSession.PlayerData.health);
                _heartFills[lastPos].fillAmount = _gameSession.PlayerData.health % 1;
            }
        }

        void InstantiateHeartContainers()
        {
            for (int i = 0; i < _gameSession.PlayerData.maxTotalHearts; i++)
            {
                GameObject temp = Instantiate(heartContainerPrefab);
                temp.transform.SetParent(heartsParent, false);
                _heartContainers[i] = temp;
                _heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
            }
        }
    }
}


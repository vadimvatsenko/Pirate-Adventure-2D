using Components;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthBarController : MonoBehaviour
    {
        private GameObject[] _heartContainers;
        private Image[] _heartFills;

        [SerializeField] HealthComponent healthComponent;
        [SerializeField] private Transform heartsParent;
        [SerializeField] GameObject heartContainerPrefab;

        private void Awake()
        {
            _heartContainers = new GameObject[healthComponent.MaxTotalHearts];
            _heartFills = new Image[healthComponent.MaxTotalHearts];
        }
        private void Start()
        {
            if (healthComponent == null) return;
            
            InstantiateHeartContainers();
            UpdateHeartsHUD();
        }

        private void OnEnable()
        {
            healthComponent.OnHealthChange += UpdateHeartsHUD;
        }

        private void OnDisable()
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
                if (i < healthComponent.MaxHealth)
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
            for (int i = 0; i < _heartFills.Length; i++)
            {
                if (i < healthComponent.Health)
                {
                    _heartFills[i].fillAmount = 1;
                }
                else
                {
                    _heartFills[i].fillAmount = 0;
                }
            }

            if (healthComponent.Health % 1 != 0)
            {
                int lastPos = Mathf.FloorToInt(healthComponent.Health);
                _heartFills[lastPos].fillAmount = healthComponent.Health % 1;
            }
        }

        void InstantiateHeartContainers()
        {
            for (int i = 0; i < healthComponent.MaxTotalHearts; i++)
            {
                GameObject temp = Instantiate(heartContainerPrefab);
                temp.transform.SetParent(heartsParent, false);
                _heartContainers[i] = temp;
                _heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
            }
        }
    }
}


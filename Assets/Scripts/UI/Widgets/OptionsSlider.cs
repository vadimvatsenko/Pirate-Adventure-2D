using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

public class OptionsSlider 
    : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    [SerializeField] private GameObject filledBG1;
    [SerializeField] private GameObject filledBG2;
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject gear;

    private RectTransform _mainBgRectTransform;

    private bool isToggle = false;
    private float _percent;
    private float _clampedX;
    
    private void Start()
    {
        _percent = PlayerPrefs.GetFloat(GradientsInfo.Value, _percent);
        _clampedX = PlayerPrefs.GetFloat("ClampedX", _clampedX);
        _mainBgRectTransform = GetComponent<RectTransform>();
        
        SetGearPosition();
        SetValue();
    }
    private void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _mainBgRectTransform,
            mouseScreenPos,
            null, // если Overlay
            out Vector2 localPos
        );
        
        float leftStoper = (_mainBgRectTransform.rect.size.x / -2) + (gear.gameObject.GetComponent<RectTransform>().rect.size.x / 2);
        float rightStoper = (_mainBgRectTransform.rect.size.x / 2) - (gear.gameObject.GetComponent<RectTransform>().rect.size.x / 2);
        
        _clampedX = Mathf.Clamp(localPos.x, leftStoper, rightStoper);

        if (isToggle)
        {
            _percent = Mathf.InverseLerp(leftStoper, rightStoper, _clampedX); 

            SetGearPosition();

            SetValue();
            PlayerPrefs.SetFloat("ClampedX", _clampedX);
        }
    }

    private void SetGearPosition()
    {
        gear.transform.localPosition = new Vector3(_clampedX, gear.transform.localPosition.y, 0);
        gear.transform.localRotation = Quaternion.Euler(0f, 0f, -_percent * 360f);
    }

    private void SetValue()
    {
        filledBG1.GetComponent<Image>().fillAmount = _percent;
        filledBG1.GetComponent<Image>().color 
            = Color.Lerp(HexToRgbUtils.HexToRGB(GradientsInfo.colorTextGradient1), 
                HexToRgbUtils.HexToRGB(GradientsInfo.colorTextGradient2), _percent);

        volume.GetComponent<TextMeshProUGUI>().text = _percent.ToString("0" + "%");
            
        volume.GetComponent<TextMeshProUGUI>().color 
            = Color.Lerp(HexToRgbUtils.HexToRGB(GradientsInfo.colorTextGradient1), 
                HexToRgbUtils.HexToRGB(GradientsInfo.colorTextGradient2), _percent);

        PlayerPrefs.SetFloat(GradientsInfo.Value, _percent);
    }

    public void OnPointerDown(PointerEventData eventData) => isToggle = true;
    
    public void OnPointerUp(PointerEventData eventData) => isToggle = false;
}
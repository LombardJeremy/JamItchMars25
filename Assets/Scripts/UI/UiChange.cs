using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiChange : MonoBehaviour
{

    [Header("Train Value")]
    [SerializeField] private TMP_Text TrainValue;
    [SerializeField] private TMP_Text TrainState;
    [SerializeField] private Image TrainValueSlide;

    [Header("Speed Zone")]
    [SerializeField] private TMP_Text ZoneSpeedValue;
    [SerializeField] private TMP_Text ZoneSpeedState;
    
    [Header("Time")]
    [SerializeField] private TMP_Text Chrono;
    
    [Header("Game Manager")]
    [SerializeField] private GameManager _gameManager;
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        TrainState.text = _gameManager._currentHeat.ToString();
        TrainValue.text = _gameManager._currentHeatProgression.ToString();
        if (_gameManager.enabled)
        {
            ZoneSpeedState.text = _gameManager._zoneManager.CurrentZoneOfSpeed.zoneHeat.ToString();
            ZoneSpeedValue.text = _gameManager._zoneManager.CurrentZoneOfSpeed.currentProgression.ToString();
            int currentValueChrono = (int)_gameManager.Timer;
            Chrono.text = currentValueChrono.ToString();
            //Train UI
            float baseValue = Mathf.Clamp(_gameManager._currentHeatProgression / 100f, 0f, 1f);
            switch (_gameManager._currentHeat)
            {
                case StateOfHeat.LOW:
                    TrainValueSlide.fillAmount = Mathf.Lerp(0f, 0.31f, baseValue);
                    break;
                case StateOfHeat.MEDIUM:
                    TrainValueSlide.fillAmount = Mathf.Lerp(0.31f, 0.715f, baseValue);
                    break;
                case StateOfHeat.HIGH:
                    TrainValueSlide.fillAmount = Mathf.Lerp(0.715f, 1f, baseValue);
                    break;  
            }
        }
    }
}

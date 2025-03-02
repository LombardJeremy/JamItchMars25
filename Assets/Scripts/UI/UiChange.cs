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
        ZoneSpeedState.text = _gameManager._zoneManager.CurrentZoneOfSpeed.zoneHeat.ToString();
        ZoneSpeedValue.text = _gameManager._zoneManager.CurrentZoneOfSpeed.currentProgression.ToString();
        Chrono.text = _gameManager.Timer.ToString();
        TrainValueSlide.fillAmount = Mathf.Clamp(_gameManager._currentHeatProgression / 100f, 0f, 1f);
    }
}

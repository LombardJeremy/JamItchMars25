using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiChange : MonoBehaviour
{

    [Header("Train Value")]
    [SerializeField] private Image TrainValueSlide;
    
    [Header("Time")]
    [SerializeField] private TMP_Text Chrono;
    
    [Header("Game Manager")]
    [SerializeField] private GameManager _gameManager;
    
    [Header("Game Over")]
    [SerializeField] private ZoneManager zoneManager;
    [SerializeField] private TMP_Text _gameOverScore;
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
        if (_gameManager.enabled)
        {
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
        _gameOverScore.text = zoneManager.Score.ToString();
    }
}

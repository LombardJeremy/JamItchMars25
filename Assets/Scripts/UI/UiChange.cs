using TMPro;
using UnityEngine;

public class UiChange : MonoBehaviour
{

    [SerializeField] private TMP_Text Value;
    [SerializeField] private TMP_Text State;

    [SerializeField] private GameManager _gameManager;
    void Start()
    {
        State.text = _gameManager._currentHeat.ToString();
        Value.text = _gameManager._currentHeatProgression.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        State.text = _gameManager._currentHeat.ToString();
        Value.text = _gameManager._currentHeatProgression.ToString();
    }
}

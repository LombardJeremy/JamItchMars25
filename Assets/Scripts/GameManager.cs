using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Puyo Manager
    public Puyo _currentPuyo;
    public Puyo _nextPuyo;
    
    //Data Manager
    [SerializeField] public StateOfHeat _currentHeat = StateOfHeat.VERY_HIGH;
    [SerializeField] public float _currentHeatProgression;
    private ZoneOfSpeed _currentZone;

    //Value Manager
    private float Timer = 0f;
    private float _score;
    [SerializeField] private float looseOfHeatMultiplier = 1f;
    private bool _isHolding = false;
    private float timeToHold = 1f;
    
    //GameManager
    public bool gameOver = false;
    
    #region Unity
    private void Start()
    {
        StartCoroutine(GameHeatLoop());
        GameBoard.PuyoDeleted += APuyoAsBeenDeleted;
    }

    private void OnDestroy()
    {
        GameBoard.PuyoDeleted -= APuyoAsBeenDeleted;
    }
    
    private void FixedUpdate()
    {
        if (_isHolding)
        {
            if (timeToHold <= 0f)
            {
                _isHolding = false;
            }
            else
            {
                timeToHold -= 1f * Time.deltaTime;
            }
        }
        else
        {
            _currentHeatProgression -= 1f / looseOfHeatMultiplier;
        }
        //Debug.Log(_currentHeat + " " + _currentHeatProgression);
        if (_currentHeatProgression >= 100)
        {
            if (_currentHeat + 1 < (StateOfHeat)5)
            {
                _currentHeat += 1;
                _currentHeatProgression = 0f;
            }
            else
            {
                _currentHeatProgression = 99f;
            }
        }
        if (_currentHeatProgression <= 0)
        {
            if (_currentHeat - 1 >= 0)
            {
                _currentHeat -= 1;
                _currentHeatProgression = 99f;
            }
            else
            {
                _currentHeatProgression = 1f;
            }
        }
    }
    #endregion

    #region Events

    private void APuyoAsBeenDeleted(int coloridx)
    {
        //0 = maintient, 1 = Diminue, 2 = Augmente
        switch (coloridx)
        {
            case 0:
                Debug.Log("HOLD");
                _isHolding = true;
                timeToHold += 1f;
                break;
            case 1:
                Debug.Log("Minus");
                _currentHeatProgression -= 5f;
                break;
            case 2:
                Debug.Log("Plus");
                _currentHeatProgression += 5f;
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }

    #endregion

    #region Fct's

    private void CheckStateOfHeat()
    {
        if(_currentZone == null) return;
        if (_currentZone.zoneHeat != _currentHeat)
        {
            Debug.Log("WrongHeat");
            _currentZone.currentProgression += 0.5f;
        }
        else
        {
            Debug.Log("GoodHeat");
            _currentZone.currentProgression += 1f;
        }
    }

    #endregion

    #region Coroutine
    IEnumerator GameHeatLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CheckStateOfHeat();
            if(gameOver) break;
        }
    }

    #endregion
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Puyo Manager
    public Puyo _currentPuyo;
    public Puyo _nextPuyo;
    
    //Data Manager
    [SerializeField] public StateOfHeat _currentHeat = StateOfHeat.MEDIUM;
    [SerializeField] public float _currentHeatProgression;
    [SerializeField] public ZoneManager _zoneManager;

    //Value Manager
    public float Timer = 30f;
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
            if (_currentHeat + 1 < (StateOfHeat)3)
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
        if(_zoneManager.CurrentZoneOfSpeed == null) return;
        if (_zoneManager.CurrentZoneOfSpeed.zoneHeat != _currentHeat)
        {
            Debug.Log("WrongHeat");
            _zoneManager.CurrentZoneOfSpeed.currentProgression += 0.5f;
        }
        else
        {
            Debug.Log("GoodHeat");
            _zoneManager.CurrentZoneOfSpeed.currentProgression += 1f;
        }
        Debug.Log(_zoneManager.CurrentZoneOfSpeed.currentProgression);
        if (_zoneManager.CurrentZoneOfSpeed.currentProgression >= 100f)
        {
            Timer += 15f;
            _zoneManager.NextZone();
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
            Timer -= 0.1f;
            if (Timer <= 0f)
            {
                gameOver = true;
                GameObject.Find("PuyoSpawner").GetComponent<PuyoSpawner>().enabled = false;
                GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().alpha = 1;
                GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().interactable = true;
                GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            if(gameOver) break;
        }
    }

    #endregion
}

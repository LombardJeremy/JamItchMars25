using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Puyo Manager
    [NonSerialized] public Puyo _currentPuyo;
    [NonSerialized] public Puyo _nextPuyo;
    
    //Data Manager
    [Header("Base Heat & Value")]
    [SerializeField] public StateOfHeat _currentHeat = StateOfHeat.MEDIUM;
    [SerializeField] public float _currentHeatProgression;
    
    [Header("Zone Manager")]
    [SerializeField] public ZoneManager _zoneManager;

    //Value Manager
    [Header("Variables")]
    [SerializeField] public float Timer = 30f;
    private float _score;
    [SerializeField] private float looseOfHeatMultiplier = 1f;
    private int _polarity = -1;
    private bool _isHolding = false;
    private float timeToHold = 1f;
    
    //Block
    [Header("Block Value")]
    [SerializeField] private float plusBlockToAdd = 5f;
    [SerializeField] private float minusBlockToAdd = 5f;
    [SerializeField] private float holdBlockToKeep = 1f;
    
    //GameManager
    [NonSerialized] public bool gameOver = false;
    
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
            _currentHeatProgression += Time.deltaTime * looseOfHeatMultiplier * _polarity;
        }
        //Debug.Log(_currentHeat + " " + _currentHeatProgression);
        if (_currentHeatProgression >= 100)
        {
            if (_currentHeat != StateOfHeat.HIGH)
            {
                _currentHeat += 1;
                _currentHeatProgression = 1f;
            }
            else
            {
                _currentHeatProgression = 100f;
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
                timeToHold += holdBlockToKeep;
                break;
            case 1:
                Debug.Log("Minus");
                _currentHeatProgression -= minusBlockToAdd;
                _polarity = -1;
                break;
            case 2:
                Debug.Log("Plus");
                _currentHeatProgression += plusBlockToAdd;
                _polarity = 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private Puyo activePuyo;

    void Start()
    {
        SpawnPuyo();
    }

    public void SpawnPuyo(){
        if(GameBoard.WhatToDelete()){
            StartCoroutine(DelayDelete());
        }

        StartCoroutine(DelaySpawn());
    }

    private bool GameIsOver(){
        return 
            GameBoard.gameBoard[(int)transform.position.x, (int)transform.position.y] != null ||
            GameBoard.gameBoard[(int)transform.position.x + 1, (int)transform.position.y] != null;
    }

    IEnumerator DelayDelete(){
        GameBoard.DropAllColumns();
        yield return new WaitUntil(() => !GameBoard.AnyFallingBlocks());
        if(GameBoard.WhatToDelete()){
            StartCoroutine(DelayDelete());
        };

    }

    IEnumerator DelaySpawn(){
        yield return new WaitUntil(() => !GameBoard.AnyFallingBlocks() && !GameBoard.WhatToDelete());
        if(GameIsOver())
        {
            gameManager.gameOver = true;
            GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().interactable = true;
            GameObject.Find("GameOverCanvas").GetComponent<CanvasGroup>().blocksRaycasts = true;
            enabled = false; 
        } else {
            activePuyo = Instantiate((GameObject)Resources.Load("Puyo"), transform.position, Quaternion.identity).GetComponent<Puyo>();
            gameManager._currentPuyo = activePuyo;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Puyo puyo;
    private float fallSpeed;
    
    public delegate void PuyoActionSound();
    public static event PuyoActionSound PuyoMove;
    public static event PuyoActionSound PuyoRotate;

    void Start()
    {
        puyo = GetComponent<Puyo>();
        fallSpeed = puyo.fallSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            puyo.MoveLeft();
            PuyoMove?.Invoke();
        } else if(Input.GetKeyDown(KeyCode.RightArrow)){
            puyo.MoveRight();
            PuyoMove?.Invoke();
        } else if(Input.GetKey(KeyCode.DownArrow)){
            puyo.fallSpeed = fallSpeed / 6;
        } else if(Input.GetKeyDown(KeyCode.E)){
            puyo.RotateLeft();
            PuyoRotate?.Invoke();
        } else if(Input.GetKeyDown(KeyCode.Q)){
            puyo.RotateRight();
            PuyoRotate?.Invoke();
        }
        else
        {
            puyo.fallSpeed = fallSpeed;
        }
    }
}

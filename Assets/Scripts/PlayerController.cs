using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Puyo puyo;
    private float fallSpeed;

    void Start()
    {
        puyo = GetComponent<Puyo>();
        fallSpeed = puyo.fallSpeed;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            puyo.MoveLeft();
        } else if(Input.GetKeyDown(KeyCode.RightArrow)){
            puyo.MoveRight();
        } else if(Input.GetKey(KeyCode.DownArrow)){
            puyo.fallSpeed = fallSpeed / 6;
        } else if(Input.GetKeyDown(KeyCode.E)){
            puyo.RotateLeft();
        } else if(Input.GetKeyDown(KeyCode.Q)){
            puyo.RotateRight();
        }
        else
        {
            puyo.fallSpeed = fallSpeed;
        }
    }
}

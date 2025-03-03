using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoUnit : MonoBehaviour
{
    private Color[] colorArray = { new Color(0.694f, 0.706f, 0.839f), new Color(0.271f, 0.835f, 0.494f), new Color(0.776f, 0.467f, 0.451f)};
    //MAGENTA = Maintient
    //ROUGE = Augmente
    //VERT = Diminue
    public bool activelyFalling = true;
    public bool forcedDownwards = false;

    public int colorIdx;

    void Awake(){
        colorIdx = Random.Range(0,3);
        GetComponent<SpriteRenderer>().color = colorArray[colorIdx];
        foreach (Transform Children in transform)
        {
            Children.gameObject.SetActive(false);
        }
        switch (colorIdx)
        {
            case 0:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 1:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }

    public IEnumerator DropToFloor(){
        WaitForSeconds wait = new WaitForSeconds( .25f );
        Vector3 currentPos = RoundVector(gameObject.transform.position);
        for(int row = (int)currentPos.y - 1; row >= 0;  row--){
            int currentX = (int)currentPos.x;
            if(GameBoard.IsEmpty(currentX, row)){
                forcedDownwards = true; 
                GameBoard.Clear(currentX, row + 1);
                GameBoard.Add(currentX, row, gameObject.transform);                    
                gameObject.transform.position += Vector3.down;
                yield return wait;
            } else { 
                activelyFalling = false;
                forcedDownwards = false;
                break;
            }
        }
        forcedDownwards = false;
        activelyFalling = false;
    }

    public void DropToFloorExternal(){
        StartCoroutine(DropToFloor());
    }

    public Vector3 RoundVector(Vector3 vect){
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }
}

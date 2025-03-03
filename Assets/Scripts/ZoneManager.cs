using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneManager : MonoBehaviour
{
    public ZoneOfSpeed CurrentZoneOfSpeed;
    public int CurrentZoneOfSpeedID = 0;
    public List<ZoneOfSpeed> ZoneOfSpeedList;

    public int Score;
    
    //Events
    public delegate void ZoneAction();
    public static event ZoneAction ZoneChangeUI ;

    private void Awake()
    {
        ZoneOfSpeedList = new List<ZoneOfSpeed>();
        for (int i = 0; i < 4; i++)
        {
            ZoneOfSpeed tempZone = new ZoneOfSpeed();
            tempZone.zoneHeat = (StateOfHeat)Random.Range(0, 3);
            ZoneOfSpeedList.Add(tempZone); //4 zones
        }
        CurrentZoneOfSpeed = ZoneOfSpeedList[CurrentZoneOfSpeedID]; //Le premier
    }

    public void NextZone()
    {
        CurrentZoneOfSpeedID += 1;
        if ( CurrentZoneOfSpeedID < ZoneOfSpeedList.Count)
        {
            CurrentZoneOfSpeed = ZoneOfSpeedList[CurrentZoneOfSpeedID];
        }
        else
        {
            CurrentZoneOfSpeedID = 0;
            ZoneOfSpeedList.Clear();
            for (int i = 0; i < 4; i++)
            {
                ZoneOfSpeed tempZone = new ZoneOfSpeed();
                tempZone.zoneHeat = (StateOfHeat)Random.Range(0, 3);
                ZoneOfSpeedList.Add(tempZone); //4 zones
            }
            CurrentZoneOfSpeed = ZoneOfSpeedList[CurrentZoneOfSpeedID]; //Le premier
            ZoneChangeUI?.Invoke();
        }
        Score += 10;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneManager : MonoBehaviour
{
    public ZoneOfSpeed CurrentZoneOfSpeed;
    public List<ZoneOfSpeed> ZoneOfSpeedList;

    private void Start()
    {
        ZoneOfSpeedList = new List<ZoneOfSpeed>();
        for (int i = 0; i < 3; i++)
        {
            ZoneOfSpeed tempZone = new ZoneOfSpeed();
            tempZone.zoneHeat = (StateOfHeat)Random.Range(0, 5);
            ZoneOfSpeedList.Add(tempZone); //3 zones
        }
        CurrentZoneOfSpeed = ZoneOfSpeedList[0]; //Le premier
    }

    public void NextZone()
    {
        ZoneOfSpeedList.Remove(ZoneOfSpeedList[0]);
        ZoneOfSpeedList.Add(new ZoneOfSpeed());
        ZoneOfSpeedList[2].zoneHeat = (StateOfHeat)Random.Range(0, 5);
        CurrentZoneOfSpeed = ZoneOfSpeedList[0];
    }
}

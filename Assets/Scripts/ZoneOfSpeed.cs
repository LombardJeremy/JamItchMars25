using UnityEngine;
using UnityEngine.Serialization;

public class ZoneOfSpeed
{
    public float currentProgression;

    public StateOfHeat zoneHeat;

    public ZoneOfSpeed()
    {
        currentProgression = 0f;
        zoneHeat = StateOfHeat.MEDIUM;
    }
}


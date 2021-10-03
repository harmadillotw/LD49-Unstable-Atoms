using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Start is called before the first frame update
    public static Vector3 GetRamdomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public static Vector3 GetRoamingPosition(Vector3 fromPos)
    {
        return fromPos + GetRamdomDirection() * Random.Range(10f, 30f);
    }

    public static bool withinBounds(Vector3 destination, float maxX, float maxY)
    {
        if (destination.x > maxX)
        {
            return false;
        }
        if (destination.x < -maxX)
        {
            return false;
        }
        if (destination.y > maxY)
        {
            return false;
        }
        if (destination.y < -maxY)
        {
            return false;
        }
        return true;
    }

}

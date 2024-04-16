using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * "Passengers" are GameObjects that can spawn on top of a tile
 * (Interactables, Monsters, ...).
 */
[System.Serializable]
public class Passenger
{
    public GameObject prefab;
    public int yOffset;

    public int GetOffset()
    {
        return yOffset;
    }
}

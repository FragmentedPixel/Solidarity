using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aNode 
{
    public bool walkable;
    public Vector3 position;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost + hCost; } }

    public aNode(bool _walkable, Vector3 _position, int _gridX, int _gridY)
    {
        walkable = _walkable;
        position = _position;

        gridX = _gridX;
        gridY = _gridY;
    }
}

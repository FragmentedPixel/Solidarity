using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aNode 
{
    public bool walkable;
    public Vector3 position;

    public aNode(bool _walkable, Vector3 _position)
    {
        walkable = _walkable;
        position = _position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aNode : IComparable<aNode>
{
    #region Properties
    public bool walkable;
    public int movementPenality;
    public Vector3 position;

    public int gridX;
    public int gridY;

    public int HeapIndex { get; set; }
    #endregion

    #region Cost
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public aNode parent;
    #endregion

    #region Constructor
    public aNode(bool _walkable, Vector3 _position, int _gridX, int _gridY, int _penalty)
    {
        walkable = _walkable;
        position = _position;

        gridX = _gridX;
        gridY = _gridY;

        movementPenality = _penalty;
    }
    #endregion

    #region IComparable Interface Methods
    public int CompareTo(aNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);

        return -compare;
    }

    #endregion
}

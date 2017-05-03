using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aGrid : MonoBehaviour
{
    #region GridSize
    public Vector2 gridWorldSize;
    public float nodeRadius;
    #endregion

    #region LayerMasks
    public LayerMask unwalkableMask;
    #endregion

    aNode[,] grid;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
    }
}

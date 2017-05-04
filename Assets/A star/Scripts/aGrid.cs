using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aGrid : MonoBehaviour
{
    #region Variabiles
    #region GridSize
    public Vector2 gridWorldSize;
    public float nodeRadius;
    #endregion

    #region LayerMasks
    public LayerMask unwalkableMask;
    #endregion

    #region Grid
    aNode[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    #endregion
    #endregion

    #region Initialisation
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        CalculateGridSize();
        grid = new aNode[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2);

        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new aNode(walkable, worldPoint, x, y);
            }
    }

    private void CalculateGridSize()
    {
        nodeDiameter = nodeRadius * 2f;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

    }

    #endregion

    #region Methods
    public aNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2);
        float percentY = (worldPosition.z + gridWorldSize.y / 2);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];

    }
    public List<aNode> GetNeighbours (aNode node)
    {
        List<aNode> neighbours = new List<aNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);
            }
        }

        neighbours.Remove(node);

        return neighbours;
    }

    #endregion 

    #region Gismos
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(aNode n in grid)
            {

                Gizmos.color = n.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
    #endregion
}

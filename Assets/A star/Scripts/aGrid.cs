using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aGrid : MonoBehaviour
{
    #region Variabiles
    #region GridSize
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public int MaxSize { get { return gridSizeX * gridSizeY; } }
    #endregion

    #region LayerMasks
    public LayerMask walkableMask;
    public LayerMask unwalkableMask;
    #endregion

    #region Grid
    public bool displayGizmos;
    aNode[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    #endregion

    #region Blur
    public int obstacleAvoidance = 10;
    int penaltyMin = int.MaxValue;
    int penaltyMax = int.MinValue;
    #endregion
    #endregion

    #region Initialisation
    private void Awake()
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

                //int movementPenalty = walkable ? (0) : obstacleAvoidance;

                //Walkable Pen
                int movementPenalty = 0;

                Ray ray = new Ray(worldPoint + Vector3.up * 50f, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, walkableMask))
                    movementPenalty = 0; // it has hit a walkbable.
                if (!walkable)
                    movementPenalty = obstacleAvoidance;
               



                grid[x, y] = new aNode(walkable, worldPoint, x, y, movementPenalty);
            }

        BlurPenaltyMap(3);
    }

    private void CalculateGridSize()
    {
        nodeDiameter = nodeRadius * 2f;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

    }

    private void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtends = blurSize;

        int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
        int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

        //Horizontal Pass
        for(int y = 0; y < gridSizeY; y++)
        {
            for(int x = -kernelExtends; x <= kernelExtends; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtends);
                penaltiesHorizontalPass[0, y] += grid[sampleX, y].movementPenality;
            }
            for(int x = 1; x <gridSizeX; x++)
            {
                int removeIndex = x - kernelExtends - 1;
                removeIndex = Mathf.Clamp(removeIndex, 0, gridSizeX);
                int addIndex = x + kernelExtends;
                addIndex = Mathf.Clamp(addIndex, 0, gridSizeX - 1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y].movementPenality + grid[addIndex, y].movementPenality;
            }
        }

        //Veritcal Pass 
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = -kernelExtends; y <= kernelExtends; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernelExtends);
                penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
            }

            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
            grid[x, 0].movementPenality = blurredPenalty;

            for (int y = 1; y < gridSizeY; y++)
            {
                int removeIndex = y - kernelExtends - 1;
                removeIndex = Mathf.Clamp(removeIndex, 0, gridSizeY);
                int addIndex = y + kernelExtends;
                addIndex = Mathf.Clamp(addIndex, 0, gridSizeY - 1);

                penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                grid[x, y].movementPenality = blurredPenalty;

                if (blurredPenalty > penaltyMax)
                    penaltyMax = blurredPenalty;
                if (blurredPenalty < penaltyMin)
                    penaltyMin = blurredPenalty;
            }
        }


    }

    #endregion

    #region Methods
    public aNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

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

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (!displayGizmos || grid == null)
            return;

        foreach(aNode n in grid)
        {
            Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, n.movementPenality));
            Gizmos.color = n.walkable ? Gizmos.color : Color.red;
            Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter));
        }
    }
    #endregion
}

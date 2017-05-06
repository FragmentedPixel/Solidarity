using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aPathfinding : MonoBehaviour
{
    #region Variabiles
    private aGrid grid;
    #endregion

    #region Initialization
    private void Awake()
    {
        grid = GetComponent<aGrid>();
    }
    #endregion

    #region Methods
    

    #endregion

    #region Utility
    public void FindPath(aPathRequest request, Action<PathResult> callback)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        aNode startNode = grid.NodeFromWorldPoint(request.pathStart);
        aNode targetNode = grid.NodeFromWorldPoint(request.pathEnd);

        if (startNode.walkable && targetNode.walkable)
        {
            aHeap openSet = new aHeap(grid.MaxSize);
            List<aNode> closedSet = new List<aNode>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                aNode currentNode = openSet.RemoveFirst();

                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (aNode neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                        continue;

                    int newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenality;
                    if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCost;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        if (pathSuccess)
            wayPoints = RetracePath(startNode, targetNode);

        pathSuccess = wayPoints.Length > 0;
        callback(new PathResult(wayPoints, pathSuccess, request.callback));

    }

    private int GetDistance(aNode a, aNode b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        else
            return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    private Vector3[] RetracePath(aNode startNode, aNode targetNode)
    {
        List<aNode> path = new List<aNode>();
        aNode currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

    }
    private Vector3[] SimplifyPath(List<aNode> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i <path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
                wayPoints.Add(path[i].position);

            directionOld = directionNew;
        }

        return wayPoints.ToArray();

    }
    #endregion
}

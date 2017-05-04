using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aPathfinding : MonoBehaviour
{
    private aGrid grid;

    private void Awake()
    {
        grid = GetComponent<aGrid>();
    }

    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        aNode startNode = grid.NodeFromWorldPoint(startPosition);
        aNode targetNode = grid.NodeFromWorldPoint(targetPosition);

        List<aNode> openSet = new List<aNode>();
        List<aNode> closedSet = new List<aNode>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            aNode currentNode = openSet[0];
            for(int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                return;
            }

            foreach(aNode neighbour in grid.GetNeighbours(currentNode))
            {
            }
        }
    }

}

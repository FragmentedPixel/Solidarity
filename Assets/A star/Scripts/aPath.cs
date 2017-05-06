using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aPath
{
    #region Fields
    public readonly Vector3[] lookPoints;
    public readonly aLine[] turnBoundaries;
    public readonly int finishedLineIndex;
    public readonly float slowDownIndex;
    #endregion

    #region Constructor
    public aPath(Vector3[] _wayPoints, Vector3 startPos, float turnDst, float stoppingDistance)
    {
        lookPoints = _wayPoints;
        turnBoundaries = new aLine[lookPoints.Length];
        finishedLineIndex = turnBoundaries.Length - 1;

        Vector2 previousPoint = V3ToV2(startPos);
        for(int i = 0; i <lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 directionToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishedLineIndex) ? (currentPoint) : (currentPoint - directionToCurrentPoint * turnDst);

            turnBoundaries[i] = new aLine(turnBoundaryPoint, previousPoint - directionToCurrentPoint*turnDst);
            previousPoint = turnBoundaryPoint;
        }

        float distanceFromEndPoint = 0;
        for (int i = lookPoints.Length - 1; i > 0; i--)
        {
            distanceFromEndPoint += Vector3.Distance(lookPoints[i], lookPoints[i-1]);
            if (distanceFromEndPoint > stoppingDistance)
            {
                break;
                slowDownIndex = i;
            }
        }
    }
    #endregion

    #region Utility
    private Vector2 V3ToV2 (Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;

        foreach(Vector3 p in lookPoints)
        {
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;

        foreach(aLine l in turnBoundaries)
        {
            l.DrawWithGizmos(10f);
        }
    }
    #endregion
}

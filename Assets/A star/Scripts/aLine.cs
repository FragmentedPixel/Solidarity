using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct aLine
{
    #region Variabiles

    const float verticalGradient = 1e5f;

    private float gradient;
    private float y_intersect;

    private Vector2 pointOnLine_1;
    private Vector2 pointOnLine_2;

    private float gradientPerpendicular;
    private bool approachSide;

    #endregion

    #region Constructor
    public aLine(Vector2 pointOnLine, Vector2 pointOnPerpendicular)
    {
        float dx = pointOnLine.x - pointOnPerpendicular.x;
        float dy = pointOnLine.y - pointOnPerpendicular.y;

        gradientPerpendicular = (dx != 0) ? (dy / dx) : verticalGradient;
        gradient = (dx != 0) ? (-1 / gradientPerpendicular) : verticalGradient;

        y_intersect = pointOnLine.y - gradient * pointOnLine.x;

        pointOnLine_1 = pointOnLine;
        pointOnLine_2 = pointOnLine + new Vector2(1, gradient);

        approachSide = false; 
        approachSide = GetSide(pointOnPerpendicular);
    }
    #endregion

    #region Methods
    private bool GetSide(Vector2 p)
    {
        return (p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_2.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);
    }

    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != approachSide;
    }

    public void DrawWithGizmos(float length)
    {
        Vector3 lineDirection = new Vector3(1, 0, gradient).normalized;
        Vector3 lineCentre = new Vector3(pointOnLine_1.x, 0, pointOnLine_1.y) + Vector3.up;
        Gizmos.DrawLine(lineCentre - lineDirection * length / 2, lineCentre + lineDirection * length / 2);
    }

    public float DistanceFromPoint(Vector2 point)
    {
        float yInterceptPerpendicular = point.y - gradientPerpendicular * point.x;
        float intersectX = (yInterceptPerpendicular - y_intersect) / (gradient - gradientPerpendicular);
        float intersectY = gradient * intersectX + y_intersect;

        return Vector2.Distance(point, new Vector2(intersectX, intersectY));
    }
    #endregion
}
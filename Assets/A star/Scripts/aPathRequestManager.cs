using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aPathRequestManager : MonoBehaviour
{
    #region Variabiles
    private static aPathRequestManager instance;
    private aPathfinding pathfinding;
    private bool isProcessingPath;

    private Queue<aPathRequest> pathRequestQueue = new Queue<aPathRequest>();
    private aPathRequest currentPathRequest;
    #endregion

    #region Initialization
    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<aPathfinding>();
    }
    #endregion

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[],bool> callback)
    {
        aPathRequest newRequest = new aPathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    private void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
}

struct aPathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public aPathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }
}
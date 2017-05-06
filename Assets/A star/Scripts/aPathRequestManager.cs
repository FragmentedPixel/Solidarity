﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class aPathRequestManager : MonoBehaviour
{
    #region Variabiles
    private static aPathRequestManager instance;
    private aPathfinding pathfinding;

    Queue<PathResult> results = new Queue<PathResult>();
    #endregion

    #region Initialization
    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<aPathfinding>();
    }
    #endregion

    private void Update()
    {
        if (results.Count > 0)
        {
            int itmsInQueue = results.Count;
            lock(results)
            {
                for(int i = 0; i < itmsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.succes);
                }
            }
        }
    }

    public static void RequestPath(aPathRequest request)
    {
        ThreadStart threadStart = delegate { instance.pathfinding.FindPath(request, instance.FinishedProcessingPath); };
        threadStart.Invoke();
    }

    public void FinishedProcessingPath(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }

    
}

public struct aPathRequest
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

public struct PathResult
{
    public Vector3[] path;
    public bool succes;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] _path, bool _success, Action<Vector3[], bool> _callback)
    {
        path = _path;
        succes = _success;
        callback = _callback;
    }
}
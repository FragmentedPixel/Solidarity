using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aAgent : MonoBehaviour
{
    public Transform target;
    public float speed;

    private Vector3[] path;
    private int targetIndex;

    private void Start()
    {
        aPathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (!pathSuccessful)
            return;

        path = newPath;
        StopCoroutine("FollowPathCR");
        StartCoroutine("FollowPathCR");
    }

    private IEnumerator FollowPathCR()
    {
        Vector3 currentWayPoint = path[0];

        while(true)
        {
            if(transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                    yield break;

                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
            yield return null; 
        }
    }

    private void OnDrawGizmos()
    {
        if (path == null)
            return;

        for(int i = targetIndex; i < path.Length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(path[i], Vector3.one);
            if (i == targetIndex)
                Gizmos.DrawLine(transform.position, path[i]);
            else
                Gizmos.DrawLine(path[i - 1], path[i]);

        }
    }
}

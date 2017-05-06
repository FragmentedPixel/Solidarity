using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aAgent : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float turnSpeed;
    public float turnDistance;
    public float stoppingDistance;

    private aPath path;

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful)
    {
        if (!pathSuccessful)
            return;

        path = new aPath(wayPoints, transform.position, turnDistance, stoppingDistance);

        StopCoroutine("FollowPathCR");
        StartCoroutine("FollowPathCR");
    }

    private IEnumerator FollowPathCR()
    {
        bool followingPath = true;
        int pathIndex = 0;

        transform.LookAt(path.lookPoints[0]);
        float speedPercent = 1f;

        while(followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);

            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishedLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                    pathIndex++;
            }

            /*
            if (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishedLineIndex)
                    followingPath = false;
                else
                    pathIndex++;
            }*/

            //While following path
            if(pathIndex >= path.slowDownIndex && stoppingDistance > 0)
                speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishedLineIndex].DistanceFromPoint(pos2D) / stoppingDistance);

            if (speedPercent < .01f)
                followingPath = false;

            Quaternion targetRot = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);

            yield return null; 
        }
    }
    private IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
            yield return new WaitForSeconds(.3f);

        aPathRequestManager.RequestPath(new aPathRequest(transform.position, target.position, OnPathFound));
        float updateTime = 1f;
        float pathUpdateMoveThreshHold = .5f;
        float squareMoveThreshhold = pathUpdateMoveThreshHold * pathUpdateMoveThreshHold;

        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(updateTime);

            if ((target.position - targetPosOld).sqrMagnitude > squareMoveThreshhold)
            {
                aPathRequestManager.RequestPath(new aPathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null)
            path.DrawWithGizmos();
    }

    //TO Implement

    public void SetDestination(Transform t)
    {

    }
    public bool ReachedDestination { get; set; }
}

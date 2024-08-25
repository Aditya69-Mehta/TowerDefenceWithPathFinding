using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float enemySpeed = 1f;
    [SerializeField] [Range(0f, 1f)] float enemySpeedRamp = 0.1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void Awake(){
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        // Debug.Log("Initiated Start");
        ReturnToStart();
        RecalculatePath(true);
        // Debug.Log("Start Ended");
    }

    void RecalculatePath(bool resetPath){
        Vector2Int coords = new Vector2Int();
        if(resetPath) coords = pathFinder.StartCoord;
        else coords = gridManager.GetCoordsFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.BuildNewPath(coords);
        StartCoroutine(FollowWaypoint());
    }

    void ReturnToStart(){
        transform.position = gridManager.GetPositionFromCoords(pathFinder.StartCoord);
    }

    IEnumerator FollowWaypoint()
    {
        for(int i = 1;i < path.Count; i++){
            // Debug.Log(waypoint.name);
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoords(path[i].coordinates);
            float travelPercentage = 0f;

            transform.LookAt(endPos);

            while(travelPercentage<1){
                travelPercentage+=Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercentage);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();
    }

    void FinishPath(){
        enemy.Penalty();
        gameObject.SetActive(false);
        
        EnemySpeedRamp();
    }

    void EnemySpeedRamp(){
        if(enemySpeed < 5) enemySpeed += enemySpeedRamp;
    }
}

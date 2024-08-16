using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float enemySpeed = 1f;
    [SerializeField] [Range(0f, 1f)] float enemySpeedRamp = 0.1f;

    Enemy enemy;

    void Start(){
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        // Debug.Log("Initiated Start");
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowWaypoint());
        // Debug.Log("Start Ended");
    }

    void FindPath(){
        path.Clear();

        GameObject waypoints = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in waypoints.transform){
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null) path.Add(waypoint);

        }
    }

    void ReturnToStart(){
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowWaypoint()
    {
        foreach(Waypoint waypoint in path){
            // Debug.Log(waypoint.name);
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
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

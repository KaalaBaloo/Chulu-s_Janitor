using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAIPathFinding : MonoBehaviour
{
    public Transform targetPosition;
    private Seeker seeker;
    private Enemy enemy;
    public Path path;
    public int nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;
    Vector3 _dir;
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Enemy>();
        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }
    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);
        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    //Código modificado de: https://arongranberg.com/astar/documentation/4_2_3_5eb80478/old/custom_movement_script.php
    public void SearchPath()
    {
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }
        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        int distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Mathf.RoundToInt(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]));
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < Mathf.RoundToInt(path.vectorPath.Count))
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        _dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Debug.Log("Direccion: " + Mathf.RoundToInt(_dir.x) + "," + Mathf.RoundToInt(_dir.y));
        RandomDirection(_dir);
    }

    //Elige una dirección aleatoria del Vector3 dirección -->  dir.X o dir.Y
    private void RandomDirection(Vector3 dir)
    {
        if (enemy.GetCanMove(Mathf.RoundToInt(dir.x), 0) && enemy.GetCanMove(0, Mathf.RoundToInt(dir.y)))
        {
            int rnd = Random.Range(0, 2);
            if (Mathf.RoundToInt(dir.x) == 0)
            {
                enemy.Movement(0, Mathf.RoundToInt(dir.y));
            }
            else if (Mathf.RoundToInt(dir.y) == 0)
            {
                enemy.Movement(Mathf.RoundToInt(dir.x), 0);
            }
            else
            {
                if (rnd == 0)
                {
                    enemy.Movement(Mathf.RoundToInt(dir.x), 0);
                }
                else
                {
                    enemy.Movement(0, Mathf.RoundToInt(dir.y));
                }
            }
        }
        else if (enemy.GetCanMove(Mathf.RoundToInt(dir.x), 0))
        {
            enemy.Movement(Mathf.RoundToInt(dir.x), 0);
        }
        else if (enemy.GetCanMove(0, Mathf.RoundToInt(dir.y)))
        {
            enemy.Movement(0, Mathf.RoundToInt(dir.y));
        }
        else
        {
            Debug.Log("No hay camino");
            enemy.SetChangeTurn();
        }
    }

    public bool GetReachEndPath()
    {
        return reachedEndOfPath;
    }
}

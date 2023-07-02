using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    private List<Collider> obstacles;
    public List<Vector3> closestPoints;
    public List<Vector3> directions;
    public List<float> distances;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = new List<Collider>();
        closestPoints = new List<Vector3>();
        directions = new List<Vector3>();
        distances = new List<float>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Clears the lists
        closestPoints.Clear();
        directions.Clear();
        distances.Clear();
        
        // Adds the points on each obstacle closest to the tank
        foreach (Collider obstacle in obstacles)
        {
            closestPoints.Add(obstacle.ClosestPoint(transform.position));
        }
        if (closestPoints.Count > 0)
        {
            // Gets the direction towards the points and distances from each obstacle
            for (int i = 0; i < closestPoints.Count; i++)
            {
                directions.Add(closestPoints[i] - transform.position);
                distances.Add(directions[i].magnitude);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            obstacles.Add(other);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            obstacles.Remove(other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    private List<Collider> obstacles = new List<Collider>();
    public List<Vector3> closestPoints = new List<Vector3>();
    public List<Vector3> directions = new List<Vector3>();
    public List<float> distances = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        closestPoints.Clear();
        directions.Clear();
        distances.Clear();
        
        foreach (Collider obstacle in obstacles)
        {
            closestPoints.Add(obstacle.ClosestPoint(transform.position));
        }
        if (closestPoints.Count > 0)
        {
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

using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;

public class EnemyPathing : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> path;
    public float enemySpeed;
    private int index;
    void Start()
    {
        index = 1;
    }

    // Update is called once per frame
    void Update() //Move the enemy towards each step in the path.
    {
        if (index >= path.Count) return;
        transform.position = Vector3.MoveTowards(transform.position, path[index], 0.001f);
        if (transform.position.Equals(path[index]))
            index++;
    }
}

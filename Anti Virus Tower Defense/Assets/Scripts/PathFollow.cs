﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour {

    // Use this for initialization
    public float speed = 3.0f;

    private List<Vector3> waypoints = LevelManager.waypoints;
    private int nextPoint = 0;
	void Start () {
        Debug.Log(waypoints.Count);
	}
	
	// Update is called once per frame
	void Update () {
		if (hitMarker(transform.position, waypoints[nextPoint]))
        {
            nextPoint++;
        }

        // Move towards next waypoint
        if (nextPoint < waypoints.Count)
        {
            Vector3 dir = waypoints[nextPoint] - transform.position;
            dir.Normalize();
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool hitMarker(Vector3 position, Vector3 waypoint)
    {
        float tolerance = 0.03f;
        Vector3 dif = waypoint - position;
        dif = new Vector3(dif.x, dif.y, 0);
        if (dif.magnitude <= tolerance)
            return true;
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;  

public class RSpawnerW : MonoBehaviour 
{
	private GameObject[] walls;
	private GameObject[] crossroads;
	private GameObject[] roads;

	// Use this for initialization
	void Start ()
	{
		//nearestcrossroads = GameObject.FindGameObjectsWithTag("crossroads");

		walls = GameObject.FindGameObjectsWithTag ("wall");
		roads = GameObject.FindGameObjectsWithTag ("road");
		crossroads = GameObject.FindGameObjectsWithTag ("crossroad");

		if (isPositionOverlapping ()) 
		{
			Destroy (this.gameObject);
		}
	}

	bool isPositionOverlapping(){
		foreach (GameObject road in roads) {
			if (transform.position == road.transform.position)
				return true;
		}
		foreach (GameObject crossroad in crossroads) {
			if (transform.position == crossroad.transform.position)
				return true;
		}
		foreach (GameObject InvisibleWall in walls) {
			if (transform.position == InvisibleWall.transform.position)
				return true;
		}
		return false;
	}
}
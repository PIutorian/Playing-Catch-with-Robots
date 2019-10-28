using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;  

public class RSpawnerO : MonoBehaviour 
{
	private GameObject[] walls;

	void Start ()
	{
		walls = GameObject.FindGameObjectsWithTag ("wall");

		if (isPositionOverlappingwithwall ()) 
		{
			Destroy (this.gameObject);
		}
	}

	bool isPositionOverlappingwithwall(){
		foreach (GameObject InvisibleWall in walls) {
			if (transform.position == InvisibleWall.transform.position)
				return true;
		}
		return false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class housing : MonoBehaviour {

	public GameObject house;
	private GameObject[] roadsSN;
	private GameObject[] roadsWO;
	private double buildingspawnchance;

	void Start () {

		roadsSN = GameObject.FindGameObjectsWithTag ("roadN");
		roadsWO = GameObject.FindGameObjectsWithTag ("roadO");
		buildingspawnchance = 0.3;
		builder ();
	}
	

	void builder () {

		foreach (GameObject roadSN in roadsSN) {
			if (Random.value < buildingspawnchance) {
				Instantiate (house, new Vector3 (roadSN.transform.position.x + 5, roadSN.transform.position.y, roadSN.transform.position.z), Quaternion.identity, transform);
				Instantiate (house, new Vector3 (roadSN.transform.position.x - 5, roadSN.transform.position.y, roadSN.transform.position.z), Quaternion.identity,transform);
			} else {
				if (Random.value < buildingspawnchance) {
					Instantiate (house, new Vector3 (roadSN.transform.position.x + 5, roadSN.transform.position.y, roadSN.transform.position.z), Quaternion.identity,transform);
				} else {
					Instantiate (house, new Vector3 (roadSN.transform.position.x - 5, roadSN.transform.position.y, roadSN.transform.position.z), Quaternion.identity,transform);
				}
					

			}
		}
		foreach (GameObject roadWO in roadsWO) {
			if (Random.value < buildingspawnchance) {
				Instantiate (house, new Vector3 (roadWO.transform.position.x, roadWO.transform.position.y + 5, roadWO.transform.position.z), Quaternion.identity,transform);
				Instantiate (house, new Vector3 (roadWO.transform.position.x, roadWO.transform.position.y - 5, roadWO.transform.position.z), Quaternion.identity,transform);
			} else {
				if (Random.value < buildingspawnchance) {
					Instantiate (house, new Vector3 (roadWO.transform.position.x, roadWO.transform.position.y + 5, roadWO.transform.position.z), Quaternion.identity,transform);
				} else {
					Instantiate (house, new Vector3 (roadWO.transform.position.x, roadWO.transform.position.y - 5, roadWO.transform.position.z), Quaternion.identity,transform);
				}


			}
		}
	}
}

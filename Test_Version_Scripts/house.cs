using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour {

	private GameObject[] roadOs;
	private GameObject[] roadNs;
	private GameObject[] crossroads;
	private GameObject[] walls;
	void Start () {

		roadOs = GameObject.FindGameObjectsWithTag ("roadO");
		roadNs = GameObject.FindGameObjectsWithTag ("roadN");
		crossroads = GameObject.FindGameObjectsWithTag ("crossroad");
		walls = GameObject.FindGameObjectsWithTag ("wall");

		if (checkoverlapping ()) 
		{
			Destroy (this.gameObject);
		}
	}
	bool checkoverlapping(){

		foreach (GameObject roadWO in roadOs) {
			if (transform.position == roadWO.transform.position) {
				return true;
			}
		}
		foreach (GameObject roadSN in roadNs) {
			if (transform.position == roadSN.transform.position) {
				return true;
			}
		}
		foreach (GameObject crossroad in crossroads) {
			if (transform.position == crossroad.transform.position) {
				return true;
			}
		} return false;
		foreach (GameObject wall in walls) {
			if (transform.position == wall.transform.position) {
				return true;
			}
		}return false; 
	}
}
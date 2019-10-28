using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RManager : MonoBehaviour {

	private GameObject[] SPNs; //spawner
	private GameObject[] SPSs;
	private GameObject[] SPWs;
	private GameObject[] SPOs;
	private GameObject[] crossroadsarray;
	private GameObject[] roadsarray;
	public GameObject roadSN;
	public GameObject roadWO;
	public GameObject crossroad;
	private float timeinterval;
	private float timer;
	private float count;
	public double crossroadspawnchance;
	private double randomvar;
	private float shortestdistancetocrossroad;
	private float distancetocrossroads;
	private float numberofnearbyroads;


	// Use this for initialization
	void Start () {

		timeinterval = 1F;
		timer = 0;
		count = 0;

	}
	
	// Update is called once per frame
	void Update () {


		SPNs = GameObject.FindGameObjectsWithTag ("SPN");
		SPSs = GameObject.FindGameObjectsWithTag ("SPS");
		SPWs = GameObject.FindGameObjectsWithTag ("SPW");
		SPOs = GameObject.FindGameObjectsWithTag ("SPO");
		timer += Time.deltaTime;

		crossroadsarray = GameObject.FindGameObjectsWithTag ("crossroad");

		if (timer > timeinterval && count < 15) {
			count += 1;
			timer = 0;

			foreach (GameObject SPN in SPNs) {
				shortestdistancetocrossroad = 0;
				foreach (GameObject crossroad in crossroadsarray) { //this loop gives us the distance to a crossroad and subsequently an x for our logarithmic formula
					distancetocrossroads = Vector3.Distance (SPN.transform.position, crossroad.transform.position); // distance to a crossroad
					if (distancetocrossroads <= shortestdistancetocrossroad || shortestdistancetocrossroad == 0) { 
						shortestdistancetocrossroad = distancetocrossroads; // if shorter than the last --> make this the new shortest distance
					}
				}
				if (shortestdistancetocrossroad > 3) {
					crossroadspawnchance = (((Mathf.Log (shortestdistancetocrossroad)) * 5) - 5.5) / 10; // gives us the probability of a crossroad spawning
				} else {
					crossroadspawnchance = 0;
				}
				randomvar = Random.value;
				if (randomvar >= crossroadspawnchance) {
					Instantiate (roadSN, SPN.transform.position, Quaternion.identity, gameObject.transform);
				} else {
					roadsarray = GameObject.FindGameObjectsWithTag ("road");
					foreach (GameObject road in roadsarray) {
						if (((((road.transform.position.x) * (road.transform.position.x)) + ((road.transform.position.y) * (road.transform.position.y))) / 2) < 3.9) {
							numberofnearbyroads += 1;
						}
					}
					if (numberofnearbyroads < 3) {
						Instantiate (crossroad, SPN.transform.position, Quaternion.identity, gameObject.transform);
					} else {
						Instantiate (roadSN, SPN.transform.position, Quaternion.identity, gameObject.transform);
					}
				}
				Destroy (SPN);
			}
				
			foreach (GameObject SPS in SPSs) {
				shortestdistancetocrossroad = 0;
				foreach (GameObject crossroad in crossroadsarray) { //this loop gives us the distance to a crossroad and subsequently an x for our logarythmic formula
					distancetocrossroads = Vector3.Distance (SPS.transform.position, crossroad.transform.position); // distance to a crossroad
					if (distancetocrossroads <= shortestdistancetocrossroad || shortestdistancetocrossroad == 0) { 
						shortestdistancetocrossroad = distancetocrossroads; // if shorter than the last --> make this the new shortest distance
					}
				}
				if (shortestdistancetocrossroad > 3) {
					crossroadspawnchance = (((Mathf.Log (shortestdistancetocrossroad)) * 5) - 5.5) / 10; // gives us the probability of a crossroad spawning
				} else {
					crossroadspawnchance = 0;
				}
				randomvar = Random.value;
				if (randomvar > crossroadspawnchance) {
					Instantiate (roadSN, SPS.transform.position, Quaternion.identity, gameObject.transform);
				} else {
					roadsarray = GameObject.FindGameObjectsWithTag ("road");
					foreach (GameObject road in roadsarray) {
						if (((((road.transform.position.x) * (road.transform.position.x)) + ((road.transform.position.y) * (road.transform.position.y))) / 2) < 3.9) {
							numberofnearbyroads += 1;
						}
					}
					if (numberofnearbyroads < 3) {
						Instantiate (crossroad, SPS.transform.position, Quaternion.identity, gameObject.transform);
					} else {
						Instantiate (roadSN, SPS.transform.position, Quaternion.identity, gameObject.transform);
					}
				}
				Destroy (SPS);
			}
		}
		if (timer > timeinterval && count < 30) {
			count += 1;
			timer = 0;
			foreach (GameObject SPO in SPOs) {
				shortestdistancetocrossroad = 0;
				foreach (GameObject crossroad in crossroadsarray) { //this loop gives us the distance to a crossroad and subsequently an x for our logarythmic formula
					distancetocrossroads = Vector3.Distance (SPO.transform.position, crossroad.transform.position); // distance to a crossroad
					if (distancetocrossroads <= shortestdistancetocrossroad || shortestdistancetocrossroad == 0) { 
						shortestdistancetocrossroad = distancetocrossroads; // if shorter than the last --> make this the new shortest distance
					}
				}
				if (shortestdistancetocrossroad > 3) {
					crossroadspawnchance = (((Mathf.Log (shortestdistancetocrossroad)) * 5) - 5.5) / 10; // gives us the probability of a crossroad spawning
				} else {
					crossroadspawnchance = 0;
				}
				randomvar = Random.value;
				if (randomvar > crossroadspawnchance) {
					Instantiate (roadWO, SPO.transform.position, Quaternion.identity, gameObject.transform);
				} else {
					roadsarray = GameObject.FindGameObjectsWithTag ("road");
					foreach (GameObject road in roadsarray) {
						if (((((road.transform.position.x) * (road.transform.position.x)) + ((road.transform.position.y) * (road.transform.position.y))) / 2) < 3.9) {
							numberofnearbyroads += 1;
						}
					}
					if (numberofnearbyroads < 3) {
						Instantiate (crossroad, SPO.transform.position, Quaternion.identity, gameObject.transform);
					} else {
						Instantiate (roadWO, SPO.transform.position, Quaternion.identity, gameObject.transform);
					}
				}
				Destroy (SPO);
			}

			foreach (GameObject SPW in SPWs) {
				shortestdistancetocrossroad = 0;
				foreach (GameObject crossroad in crossroadsarray) { //this loop gives us the distance to a crossroad and subsequently an x for our logarythmic formula
					distancetocrossroads = Vector3.Distance (SPW.transform.position, crossroad.transform.position); // distance to a crossroad
					if (distancetocrossroads <= shortestdistancetocrossroad || shortestdistancetocrossroad == 0) { 
						shortestdistancetocrossroad = distancetocrossroads; // if shorter than the last --> make this the new shortest distance
					}
				}
				if (shortestdistancetocrossroad > 3) {
					crossroadspawnchance = (((Mathf.Log (shortestdistancetocrossroad)) * 5) - 5.5) / 10; // gives us the probability of a crossroad spawning
				} else {
					crossroadspawnchance = 0;
				}
				randomvar = Random.value;
				if (randomvar > crossroadspawnchance) {
					Instantiate (roadWO, SPW.transform.position, Quaternion.identity, gameObject.transform);
				} else {
					roadsarray = GameObject.FindGameObjectsWithTag ("road");
					foreach (GameObject road in roadsarray) {
						if (((((road.transform.position.x) * (road.transform.position.x)) + ((road.transform.position.y) * (road.transform.position.y))) / 2) < 3.9) {
							numberofnearbyroads += 1;
						}
					}
					if (numberofnearbyroads < 3) {
						Instantiate (crossroad, SPW.transform.position, Quaternion.identity, gameObject.transform);
					} else {
						Instantiate (roadWO, SPW.transform.position, Quaternion.identity, gameObject.transform);
					}
				}
				Destroy (SPW);
			}
		} 
		if (timer > timeinterval && count >= 30) {
			count = 0;
			timer = 0;
		}

		}
	}

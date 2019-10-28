using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Completed;

public class Roadspawner : MonoBehaviour {


	private float columnsize;
	public GameObject roadSN;
	public GameObject roadWO;
	public GameObject crossroad;
	private float xtemp;
	private float ytemp;
	private float randomtemp;
	private GameObject[] SPNs; 
	private GameObject[] SPOs; 
	private GameObject[] roadsN;
	private GameObject[] roadsO;
	private float count;
	public GameObject housing;
	public GameObject entityspawner;
	//public bool finishedroad = false;



	void Start () {

		//columnsize = GameObject.FindGameObjectWithTag ("gm").GetComponent<BoardManager>.Columns;
		columnsize = 14;
		xtemp = 0;
		ytemp = 0;
		count = 0;

		xspawner ();
		yspawner ();
		InvokeRepeating ("filler", 0, 0.1F);
		//Instantiate (housing, transform.position, Quaternion.identity);
		//finishedroad = true;
	}
	void filler (){
		
		SPNs = GameObject.FindGameObjectsWithTag ("SPN");
		SPOs = GameObject.FindGameObjectsWithTag ("SPO");
		//timer += Time.deltaTime; 
		if (count < 15) {
			count += 1;
			foreach (GameObject SPN in SPNs) {
				Instantiate (roadSN, SPN.transform.position, Quaternion.identity, gameObject.transform);
				Destroy (SPN);
			}		
			foreach (GameObject SPO in SPOs) {
				Instantiate (roadWO, SPO.transform.position, Quaternion.identity, gameObject.transform);
				Destroy (SPO);
			}
		} else {
			CancelInvoke ();
			crossroadconverter ();
		}
	}

		void xspawner () {

		while (xtemp < columnsize){
			
			randomtemp = Random.value;
			if (randomtemp > 0.5){	
			randomtemp = 1;}
			else randomtemp = 0;

			if (xtemp == 0) {
				xtemp += 1 + randomtemp;
			} else {
				xtemp += 3 + randomtemp;
			}
			if (xtemp > columnsize){
				xtemp = columnsize;}
			if (xtemp > columnsize && randomtemp == 1){ 
				xtemp -= 1;}
			if (xtemp < columnsize) {
				Instantiate (roadSN, new Vector3 (xtemp*5, 0, 0), Quaternion.identity);
			}
		}
	}
	void yspawner () {

		while (ytemp < columnsize) {

			randomtemp = Random.value;

			if (randomtemp > 0.5) {
				randomtemp = 1;
			} else
				randomtemp = 0;

			if (ytemp == 0) {
				ytemp += 1 + randomtemp;
			} else {
				ytemp += 3 + randomtemp;
			}

			if (ytemp > columnsize && randomtemp == 1) { 
				ytemp -= 1;
			}
			if (ytemp < columnsize) {
				Instantiate (roadWO, new Vector3 (0, ytemp*5, 0), Quaternion.identity);
			}
		}
	}
	void crossroadconverter (){

		roadsN = GameObject.FindGameObjectsWithTag ("roadN");
		roadsO = GameObject.FindGameObjectsWithTag ("roadO");
		foreach (GameObject roadSN in roadsN){
			foreach (GameObject roadWO in roadsO){
				if (roadWO.transform.position == roadSN.transform.position) {
					Instantiate (crossroad, roadWO.transform);
				}
			}
		}
		Instantiate (housing, transform.position, Quaternion.identity);
		Instantiate (entityspawner, transform.position, Quaternion.identity);
	}
}
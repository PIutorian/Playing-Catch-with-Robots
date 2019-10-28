using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityspawner : MonoBehaviour {

	private GameObject[] crossroads;
	private GameObject[] roads1;
	private GameObject[] roads2;
	private GameObject spawnpoint;
	int index;
	public GameObject player;
	public GameObject enemy;
	private GameObject loadingscreen;

	void Start () {
		playerspawn ();
		enemyspawn ();
	}

	void playerspawn(){
		
		crossroads = GameObject.FindGameObjectsWithTag ("crossroad");
		index = Random.Range (0, crossroads.Length);
		spawnpoint = crossroads [index];
		Instantiate (player, spawnpoint.transform.position, Quaternion.identity);
		loadingscreen = GameObject.FindGameObjectWithTag ("loadingscreen");
		Destroy (loadingscreen);
	}

	void enemyspawn(){
		player = GameObject.FindGameObjectWithTag ("player");
		roads1 = GameObject.FindGameObjectsWithTag ("roadO");
		roads2 = GameObject.FindGameObjectsWithTag ("roadN");
		InvokeRepeating ("closetoplayer", 0, 0.1F);
	}
	void closetoplayer(){
		if (Random.value > 0.5) {
			index = Random.Range (0, roads1.Length);
			spawnpoint = roads1 [index];
		} else {
			index = Random.Range (0, roads2.Length);
			spawnpoint = roads2 [index];
		}
		if (Vector3.Distance (player.transform.position, spawnpoint.transform.position) > 25) {
				CancelInvoke();
				Instantiate (enemy, spawnpoint.transform.position, Quaternion.identity);
				Instantiate (enemy, new Vector2(spawnpoint.transform.position.x+1, spawnpoint.transform.position.y+1), Quaternion.identity);
				Instantiate (enemy, new Vector2(spawnpoint.transform.position.x-1, spawnpoint.transform.position.y+1), Quaternion.identity);
		}
	}
}

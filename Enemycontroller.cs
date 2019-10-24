using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller : MonoBehaviour {

	private GameObject player;	
	private int playerenergy;
	private int depth = 4; //depth of the game Tree
	public GameObject Exit;		//Has to be defined in the editor for now
//	private bool Gameover = false;
	public GameObject pathfinder;


	void Start () {
		playerenergy = GameManager.instance.playerenergy;
		player = GameObject.FindGameObjectWithTag ("player");	//get the players position
	}

	void Update () {
		if (GameManager.instance.playersturn) {		//check if it's the players turn
			return;
		}

			
		GameObject finalpath = Instantiate (pathfinder, transform.position + Vector3.left, Quaternion.identity); //actually west -> has to be compared anyway, replaced with finaldecision
		float Max = minimax(pathfinder, finalpath.transform, player.transform, depth, true, 0);

		if (Max != Mathf.Infinity) {
			GameObject east = Instantiate (pathfinder, transform.position + Vector3.right, Quaternion.identity);
			float value = minimax (pathfinder, east.transform, player.transform, depth, true, 0);
			if (value > Max) {
				Max = value;
				Destroy (finalpath);
				finalpath = east;
			}
			Destroy (east);
				if (Max != Mathf.Infinity) {
					GameObject north = Instantiate (pathfinder, transform.position + Vector3.up, Quaternion.identity);
					value = minimax (pathfinder, north.transform, player.transform, depth, true, 0);
					if (value > Max) {
						Max = value;
						Destroy (finalpath);
						finalpath = north;
					}
					Destroy (north);

					if (Max != Mathf.Infinity) {
						GameObject south = Instantiate (pathfinder, transform.position + Vector3.down, Quaternion.identity);
						value = minimax (pathfinder, south.transform, player.transform, depth, true, 0);
						if (value > Max) {
							Destroy (finalpath);
							finalpath = south;
						}
					Destroy (south);
					}
				}
			}

			transform.position = finalpath.transform.position;
			Destroy (finalpath);
			Debug.Log (Max);

// Very ugly, used for second turn

		finalpath = Instantiate (pathfinder, transform.position + Vector3.left, Quaternion.identity);
		Max = minimax(pathfinder, finalpath.transform, player.transform, depth, true, 0);

		if (Max != Mathf.Infinity) {
			GameObject east = Instantiate (pathfinder, transform.position + Vector3.right, Quaternion.identity);
			float value = minimax (pathfinder, east.transform, player.transform, depth, true, 0);
			if (value > Max) {
				Max = value;
				Destroy (finalpath);
				finalpath = east;
			}
			Destroy (east);
			if (Max != Mathf.Infinity) {
				GameObject north = Instantiate (pathfinder, transform.position + Vector3.up, Quaternion.identity);
				value = minimax (pathfinder, north.transform, player.transform, depth, true, 0);
				if (value > Max) {
					Max = value;
					Destroy (finalpath);
					finalpath = north;
				}
				Destroy (north);

				if (Max != Mathf.Infinity) {
					GameObject south = Instantiate (pathfinder, transform.position + Vector3.down, Quaternion.identity);
					value = minimax (pathfinder, south.transform, player.transform, depth, true, 0);
					if (value > Max) {
						Destroy (finalpath);
						finalpath = south;
					}
					Destroy (south);
				}
			}
		}

		transform.position = finalpath.transform.position;
		Destroy (finalpath);
		Debug.Log (Max);
		
		GameManager.instance.playersturn = true;
	}




	/*  path = object that gets instantiated
	 * pathe =coordinates of the last enemy path
	 * pathp = coordinates of the last player path
	 * depth = depth of the Gametree
	 * isMax = defines if it's the max or mini players turn
	 * i = used to count how many spaces have been walked (Enemy = 2, Player 3)
	 */

	float minimax(GameObject path, Transform pathe, Transform pathp, int depth, bool isMax, int i){
		//Check for end of search tree or gameover condition
		if (depth == 0 || playerenergy <= 0 || (pathe.transform.position == pathp.position && isMax == true) ||(pathp.position == Exit.transform.position && isMax == false)){
			return evaluation(pathe, pathp, isMax);
		}
		if (i > 4) {
			i = 0;
			isMax = true;
		}

		//float value = Utility ();
		if (i <= 1) {
			float maxutlity = Mathf.NegativeInfinity;
			//instatntiate the four new possible ways (path1-4, wich create new 4 ways on theyr own
			GameObject path1 = Instantiate (path, pathe.position + Vector3.left, Quaternion.identity);
			float utility1 = minimax (path, path1.transform, pathp, depth - 1, isMax, i++);

			GameObject path2 = Instantiate (path, pathe.position + Vector3.right, Quaternion.identity);
			float utility2 = minimax (path, path2.transform, pathp, depth - 1, isMax, i++);

			GameObject path3 = Instantiate (path, pathe.position + Vector3.up, Quaternion.identity);
			float utility3 = minimax (path, path3.transform, pathp, depth - 1, isMax, i++);

			GameObject path4 = Instantiate (path, pathe.position + Vector3.down, Quaternion.identity);
			float utility4 = minimax (path, path4.transform, pathp, depth - 1, isMax, i++);

			maxutlity = Mathf.Max (utility1, utility2, utility3, utility4);
			Object.Destroy (path1);
			Object.Destroy (path2);
			Object.Destroy (path3);
			Object.Destroy (path4);
			return maxutlity;

		} else{
			isMax = false;
			float minutility = Mathf.Infinity;

			GameObject path1 = Instantiate (path, pathp.position + Vector3.left, Quaternion.identity);
			float utility1 = minimax (path, pathe, path1.transform, depth - 1, isMax, i++);

			GameObject path2 = Instantiate (path, pathp.position + Vector3.right, Quaternion.identity);
			float utility2 = minimax (path, pathe, path2.transform, depth - 1, isMax, i++);

			GameObject path3 = Instantiate (path, pathp.position + Vector3.up, Quaternion.identity);
			float utility3 = minimax (path, pathe, path3.transform, depth - 1, isMax, i++);

			GameObject path4 = Instantiate (path, pathp.position + Vector3.down, Quaternion.identity);
			float utility4 = minimax (path, pathe, path4.transform, depth - 1, isMax, i++);

			minutility = Mathf.Min (utility1, utility2, utility3, utility4);
			Object.Destroy (path1);
			Object.Destroy (path2);
			Object.Destroy (path3);
			Object.Destroy (path4);
			return minutility;
		}
	}


	float evaluation (Transform pathe, Transform pathp, bool ismax) {
		if (playerenergy <= 0 || pathe.position == pathp.position) {
			return Mathf.Infinity;
		}
		if (pathp.position == Exit.transform.position) {
			return Mathf.NegativeInfinity;
		}
		else {
			float dist1 = Vector2.Distance (pathe.position, pathp.position);
			float dist2 = Vector2.Distance (pathp.position, Exit.transform.position);
			//distance to other enemies missing
			return (dist1 + (dist2 / 10) + playerenergy);
		}
	}
}
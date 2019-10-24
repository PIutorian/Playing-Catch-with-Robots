using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontrollervectors : MonoBehaviour {

	private GameObject player;	
	private int playerenergy;
	public int depth = 4; //depth of the game Tree
	public GameObject Exit;		//Has to be defined in the editor for now
//	private bool Gameover = false; 
	public GameObject[] enemies;
	LayerMask mask;



	void Start () {
		playerenergy = GameManager.instance.playerenergy;
		player = GameObject.FindGameObjectWithTag ("player");	//get the players position
		mask = LayerMask.GetMask ("Wall");//Define what Layers the raycast should look for
	}


	void Update () {
		if (GameManager.instance.playersturn) {		//check if it's the players turn
			return;
		}

			
			Vector2Int pathp = new Vector2Int (0, 0);
			Vector2Int finalpath = new Vector2Int (0, 2);
			float Max = minimax (transform, finalpath, pathp, depth, false);

			if (Max != Mathf.Infinity) {
				Vector2Int newpath = new Vector2Int (0, 1);
				float utility2 = minimax (transform, newpath, pathp, depth - 1, false);

				if (utility2 > Max) {
					Max = utility2;
					finalpath = newpath;
				}
				if (Max != Mathf.Infinity) {


					newpath = new Vector2Int (1, 1);
					float utility3 = minimax (transform, newpath, pathp, depth - 1, false);

					if (utility3 > Max) {
						Max = utility3;
						finalpath = newpath;
					}
					if (Max != Mathf.Infinity) {

						newpath = new Vector2Int (2, 0);
						float utility4 = minimax (transform, newpath, pathp, depth - 1, false);

						if (utility4 > Max) {
							Max = utility4;
							finalpath = newpath;
						}
						if (Max != Mathf.Infinity) {

							newpath = new Vector2Int (1, 0);
							float utility5 = minimax (transform, newpath, pathp, depth - 1, false);

							if (utility5 > Max) {
								Max = utility5;
								finalpath = newpath;
							}
							if (Max != Mathf.Infinity) {

								newpath = new Vector2Int (1, -1);
								float utility6 = minimax (transform, newpath, pathp, depth - 1, false);

								if (utility6 > Max) {
									Max = utility6;
									finalpath = newpath;
								}
								if (Max != Mathf.Infinity) {

									newpath = new Vector2Int (0, -2);
									float utility7 = minimax (transform, newpath, pathp, depth - 1, false);

									if (utility7 > Max) {
										Max = utility7;
										finalpath = newpath;
									}
									if (Max != Mathf.Infinity) {

										newpath = new Vector2Int (0, -1);
										float utility8 = minimax (transform, newpath, pathp, depth - 1, false);

										if (utility8 > Max) {
											Max = utility8;
											finalpath = newpath;
										}
										if (Max != Mathf.Infinity) {

											newpath = new Vector2Int (-1, -1);
											float utility9 = minimax (transform, newpath, pathp, depth - 1, false);

											if (utility9 > Max) {
												Max = utility9;
												finalpath = newpath;
											}
											if (Max != Mathf.Infinity) {

												newpath = new Vector2Int (-2, 0);
												float utility10 = minimax (transform, newpath, pathp, depth - 1, false);

												if (utility10 > Max) {
													Max = utility10;
													finalpath = newpath;
												}
												if (Max != Mathf.Infinity) {

													newpath = new Vector2Int (-1, 0);
													float utility11 = minimax (transform, newpath, pathp, depth - 1, false);

													if (utility11 > Max) {
														Max = utility11;
														finalpath = newpath;
													}
													if (Max != Mathf.Infinity) {

														newpath = new Vector2Int (-1, 1);
														float utility12 = minimax (transform, newpath, pathp, depth - 1, false);

														if (utility12 > Max) {
															Max = utility12;
															finalpath = newpath;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			Vector2 p = finalpath;
			Vector3 p1 = p;

			transform.position += p1;
			Debug.Log (Max);


		
		GameManager.instance.playersturn = true;
	}




	/*  enemy = position of the current enemy
	 * pathe = theoretical change of position for enemy
	 * pathp = theoretical change of position for player
	 * depth = depth of the Gametree
	 * isMax = defines if it's the max or mini players turn
	 */

	float minimax(Transform enemy, Vector2Int pathe, Vector2Int pathp, int depth, bool isMax){
		//Check for end of search tree or gameover condition
		float result = terminaltest (enemy, pathe, pathp, depth, isMax);
		if (result != 9991019)
			return result;

		if (isMax) {
			//instatntiate the new possible ways path1-12, wich create 12 new ways on theyr own

			Vector2Int newpath = (pathe + new Vector2Int(0,2));
			float utility1 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility1 == Mathf.Infinity || utility1 == Mathf.NegativeInfinity)
				return utility1;

			newpath = (pathe + new Vector2Int(0,1));
			float utility2 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility2 == Mathf.Infinity || utility2 == Mathf.NegativeInfinity)
				return utility2;

			newpath = (pathe + new Vector2Int(1,1));
			float utility3 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility3 == Mathf.Infinity || utility3 == Mathf.NegativeInfinity)
				return utility3;

			newpath = (pathe + new Vector2Int(2,0));
			float utility4 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility4 == Mathf.Infinity || utility4 == Mathf.NegativeInfinity)
				return utility4;

			newpath = (pathe + new Vector2Int(1,0));
			float utility5 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility5 == Mathf.Infinity || utility5 == Mathf.NegativeInfinity)
				return utility5;

			newpath = (pathe + new Vector2Int(1,-1));
			float utility6 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility6 == Mathf.Infinity || utility6 == Mathf.NegativeInfinity)
				return utility6;

			newpath = (pathe + new Vector2Int(0,-2));
			float utility7 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility7 == Mathf.Infinity || utility7 == Mathf.NegativeInfinity)
				return utility7;

			newpath = (pathe + new Vector2Int(0,-1));
			float utility8 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility8 == Mathf.Infinity || utility8 == Mathf.NegativeInfinity)
				return utility8;

			newpath = (pathe + new Vector2Int(-1,-1));
			float utility9 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility9 == Mathf.Infinity || utility9 == Mathf.NegativeInfinity)
				return utility9;

			newpath = (pathe + new Vector2Int(-2,0));
			float utility10 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility10 == Mathf.Infinity || utility10 == Mathf.NegativeInfinity)
				return utility10;

			newpath = (pathe + new Vector2Int(-1,0));
			float utility11 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility11 == Mathf.Infinity || utility11 == Mathf.NegativeInfinity)
				return utility11;

			newpath = (pathe + new Vector2Int(-1,1));
			float utility12 = minimax (enemy, newpath, pathp, depth - 1, false);
			if (utility12 == Mathf.Infinity || utility12 == Mathf.NegativeInfinity)
				return utility12;


			float maxutlity = Mathf.Max (utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12);
			return maxutlity;

		} else{

			Vector2Int newpath = (pathp + new Vector2Int(0,3));
			float utility1 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility1 == Mathf.Infinity || utility1 == Mathf.NegativeInfinity)
				return utility1;

			newpath = (pathp + new Vector2Int(0,2));
			float utility2 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility2 == Mathf.Infinity || utility2 == Mathf.NegativeInfinity)
				return utility2;

			newpath = (pathp + new Vector2Int(0,1));
			float utility3 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility3 == Mathf.Infinity || utility3 == Mathf.NegativeInfinity)
				return utility3;

			newpath = (pathp + new Vector2Int(1,2));
			float utility4 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility4 == Mathf.Infinity || utility4 == Mathf.NegativeInfinity)
				return utility4;

			newpath = (pathp + new Vector2Int(2,1));
			float utility5 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility5 == Mathf.Infinity || utility5 == Mathf.NegativeInfinity)
				return utility5;

			newpath = (pathp + new Vector2Int(1,1));
			float utility6 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility6 == Mathf.Infinity || utility6 == Mathf.NegativeInfinity)
				return utility6;

			newpath = (pathp + new Vector2Int(3,0));
			float utility7 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility7 == Mathf.Infinity || utility7 == Mathf.NegativeInfinity)
				return utility7;

			newpath = (pathp + new Vector2Int(2,0));
			float utility8 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility8 == Mathf.Infinity || utility8 == Mathf.NegativeInfinity)
				return utility8;

			newpath = (pathp + new Vector2Int(1,0));
			float utility9 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility9 == Mathf.Infinity || utility9 == Mathf.NegativeInfinity)
				return utility9;

			newpath = (pathp + new Vector2Int(2,-1));
			float utility10 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility10 == Mathf.Infinity || utility10 == Mathf.NegativeInfinity)
				return utility10;

			newpath = (pathp + new Vector2Int(1,-1));
			float utility11 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility11 == Mathf.Infinity || utility11 == Mathf.NegativeInfinity)
				return utility11;

			newpath = (pathp + new Vector2Int(1,-2));
			float utility12 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility12 == Mathf.Infinity || utility12 == Mathf.NegativeInfinity)
				return utility12;

			newpath = (pathp + new Vector2Int(0,-3));
			float utility13 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility13 == Mathf.Infinity || utility13 == Mathf.NegativeInfinity)
				return utility13;

			newpath = (pathp + new Vector2Int(0,-2));
			float utility14 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility14 == Mathf.Infinity || utility14 == Mathf.NegativeInfinity)
				return utility14;

			newpath = (pathp + new Vector2Int(0,-1));
			float utility15 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility15 == Mathf.Infinity || utility15 == Mathf.NegativeInfinity)
				return utility15;

			newpath = (pathp + new Vector2Int(-1,-2));
			float utility16 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility16 == Mathf.Infinity || utility16 == Mathf.NegativeInfinity)
				return utility16;

			newpath = (pathp + new Vector2Int(-1,-1));
			float utility17 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility17 == Mathf.Infinity || utility17 == Mathf.NegativeInfinity)
				return utility17;

			newpath = (pathp + new Vector2Int(-2,-1));
			float utility18 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility18 == Mathf.Infinity || utility18 == Mathf.NegativeInfinity)
				return utility18;

			newpath = (pathp + new Vector2Int(-3,0));
			float utility19 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility19 == Mathf.Infinity || utility19 == Mathf.NegativeInfinity)
				return utility19;

			newpath = (pathp + new Vector2Int(-2,0));
			float utility20 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility20 == Mathf.Infinity || utility20 == Mathf.NegativeInfinity)
				return utility20;

			newpath = (pathp + new Vector2Int(-1,0));
			float utility21 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility21 == Mathf.Infinity || utility21 == Mathf.NegativeInfinity)
				return utility21;

			newpath = (pathp + new Vector2Int(-2,1));
			float utility22 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility22 == Mathf.Infinity || utility22 == Mathf.NegativeInfinity)
				return utility22;

			newpath = (pathp + new Vector2Int(-1,2));
			float utility23 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility23 == Mathf.Infinity || utility23 == Mathf.NegativeInfinity)
				return utility23;

			newpath = (pathp + new Vector2Int(-1,1));
			float utility24 = minimax (enemy, pathe, newpath, depth - 1, true);
			if (utility24 == Mathf.Infinity || utility24 == Mathf.NegativeInfinity)
				return utility24;


			float minutility = Mathf.Min (utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12, utility13, utility14, utility15, utility16, utility17, utility18, utility19, utility20, utility21, utility22, utility23, utility24);
			return minutility;
		}
	}

	float terminaltest (Transform enemy, Vector2Int pathe, Vector2Int pathp, int depth, bool isMax) {
		//convert Vector2Int to Vector3 to compare positions
		Vector2 path1 = pathe;
		Vector2 path2 = pathp;
		Vector3 path3 = path1;
		Vector3 path4 = path2;
		//Wall detection
		mask = LayerMask.GetMask ("Wall");//Define what Layers the raycast should look for
		if (isMax) {
			RaycastHit2D hit = Physics2D.Raycast (enemy.position, pathe, (4-depth), mask);
			if (hit.collider != null) {
				return Mathf.NegativeInfinity;
			}
		} else {
			RaycastHit2D hit = Physics2D.Raycast(player.transform.position, pathp,((4-depth)*(3/2)), mask);
			if (hit.collider != null) {
				return Mathf.Infinity;
			}
		}
		//Distance Player- ENemy & Player-Exit
		if (Vector3.Distance((enemy.position + path3),(player.transform.position + path4)) < 0.5)
			return Mathf.Infinity;
		else if (Vector3.Distance((player.transform.position + path4), Exit.transform.position) < 0.5)
			return Mathf.NegativeInfinity;
		else if (depth == 0) {
			float dist1 = Vector3.Distance((enemy.position + path3),(player.transform.position + path4));
			float dist2 = Vector3.Distance((player.transform.position + path4), Exit.transform.position);
			//distance to other enemies, type of road still missing
			return ((-dist1) + (dist2 / 2) + playerenergy);
		}
		else
			return 9991019;
	}
}
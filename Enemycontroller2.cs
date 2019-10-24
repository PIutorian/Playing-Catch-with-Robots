using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller2 : MonoBehaviour {

	private GameObject player;	
	private int playerenergy;
	private int depth = 4; //depth of the game Tree
	public GameObject Exit;		//Has to be defined in the editor for now
	//	private bool Gameover = false; 
	public GameObject[] enemies;
	private LayerMask wallmask;
	private LayerMask energymask;


	void Start () {
		wallmask = LayerMask.GetMask ("Wall");//Define what Layers the raycast should look for
	}

	void Update () {
			

		if (GameManager.instance.playersturn) {		//check if it's the players turn
			return;
		}
		//Get enemy and player objects
		if (enemies.Length == 0) {
			enemies = GameObject.FindGameObjectsWithTag ("enemy");
		}
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("player");
		}

		playerenergy = GameManager.instance.playerenergy;
		//get the current Energy level


		foreach (GameObject enemy in enemies) {

			Vector2Int pathp = new Vector2Int (0, 0);

			Vector2Int finalpath = new Vector2Int (0, 2);
			RaycastHit2D hit = Physics2D.Raycast ((enemy.transform.position), finalpath, finalpath.magnitude, wallmask);
			float Max;
			if (hit.collider != null) {
				Max = Mathf.NegativeInfinity;
			} else {
				Max = minimax (enemy, finalpath, pathp, depth, false, playerenergy);
			}

			if (Max != Mathf.Infinity) {
				
				Vector2Int newpath = new Vector2Int (0, 1);
				hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
				float utility2;
				if (hit.collider != null) {
					utility2 = Mathf.NegativeInfinity;
				} else {
					utility2 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
				}

				if (utility2 > Max) {
					Max = utility2;
					finalpath = newpath;
				}
				if (Max != Mathf.Infinity) {


					newpath = new Vector2Int (1, 1);
					hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
					float utility3;
					if (hit.collider != null) {
						utility3 = Mathf.NegativeInfinity;
					} else {
						utility3 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
					}

					if (utility3 > Max) {
						Max = utility3;
						finalpath = newpath;
					}
					if (Max != Mathf.Infinity) {

						newpath = new Vector2Int (2, 0);
						hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
						float utility4;
						if (hit.collider != null) {
							utility4 = Mathf.NegativeInfinity;
						} else {
							utility4 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
						}

						if (utility4 > Max) {
							Max = utility4;
							finalpath = newpath;
						}
						if (Max != Mathf.Infinity) {

							newpath = new Vector2Int (1, 0);
							hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
							float utility5;
							if (hit.collider != null) {
								utility5 = Mathf.NegativeInfinity;
							} else {
								utility5 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
							}

							if (utility5 > Max) {
								Max = utility5;
								finalpath = newpath;
							}
							if (Max != Mathf.Infinity) {

								newpath = new Vector2Int (1, -1);
								hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
								float utility6;
								if (hit.collider != null) {
									utility6 = Mathf.NegativeInfinity;
								} else {
									utility6 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
								}

								if (utility6 > Max) {
									Max = utility6;
									finalpath = newpath;
								}
								if (Max != Mathf.Infinity) {

									newpath = new Vector2Int (0, -2);
									hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
									float utility7;
									if (hit.collider != null) {
										utility7 = Mathf.NegativeInfinity;
									} else {
										utility7 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
									}

									if (utility7 > Max) {
										Max = utility7;
										finalpath = newpath;
									}
									if (Max != Mathf.Infinity) {

										newpath = new Vector2Int (0, -1);
										hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
										float utility8;
										if (hit.collider != null) {
											utility8 = Mathf.NegativeInfinity;
										} else {
											utility8 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
										}
											
										if (utility8 > Max) {
											Max = utility8;
											finalpath = newpath;
										}
										if (Max != Mathf.Infinity) {

											newpath = new Vector2Int (-1, -1);
											hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
											float utility9;
											if (hit.collider != null) {
												utility9 = Mathf.NegativeInfinity;
											} else {
												utility9 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
											}

											if (utility9 > Max) {
												Max = utility9;
												finalpath = newpath;
											}
											if (Max != Mathf.Infinity) {

												newpath = new Vector2Int (-2, 0);
												hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
												float utility10;
												if (hit.collider != null) {
													utility10 = Mathf.NegativeInfinity;
												} else {
													utility10 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
												}

												if (utility10 > Max) {
													Max = utility10;
													finalpath = newpath;
												}
												if (Max != Mathf.Infinity) {

													newpath = new Vector2Int (-1, 0);
													hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
													float utility11;
													if (hit.collider != null) {
														utility11 = Mathf.NegativeInfinity;
													} else {
														utility11 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
													}

													if (utility11 > Max) {
														Max = utility11;
														finalpath = newpath;
													}
													if (Max != Mathf.Infinity) {

														newpath = new Vector2Int (-1, 1);
														hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
														float utility12;
														if (hit.collider != null) {
															utility12 = Mathf.NegativeInfinity;
														} else {
															utility12 = minimax (enemy, newpath, pathp, depth - 1, false, playerenergy);
														}

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
			if (Max == Mathf.NegativeInfinity)
				Debug.Log ("Player Victory");
			else {
			enemy.transform.position += p1;
			Debug.Log (Max);
			}
		}


		GameManager.instance.playersturn = true;
	}




	/* enemy = position of the current enemy
	 * pathe = theoretical change of position for enemy
	 * pathp = theoretical change of position for player
	 * depth = depth of the Gametree
	 * isMax = defines if it's the max or mini players turn
     * plenergy = the energy the player will have left after executing his turn
	 */

	float minimax(GameObject enemy, Vector2Int pathe, Vector2Int pathp, int depth, bool isMax, int plenergy){
		//Check for end of search tree or gameover condition
		float result = terminaltest (enemy, pathe, pathp, depth, isMax, plenergy);
		if (result != 9991019)
			return result;
		
		if (isMax) {
			//instatntiate the 12 new possible positions (path1-12, wich create 12 new positions on theyr own
			Vector2Int newpath = (pathe + new Vector2Int(0,2));
			//first check for wall collision
			RaycastHit2D hit = Physics2D.Raycast ((enemy.transform.position), newpath, newpath.magnitude, wallmask);
			float utility1;
			if (hit.collider != null) {
				utility1 = Mathf.NegativeInfinity;
			} else {
				//if no collision is detected search continues
				utility1 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility1 == Mathf.Infinity)
					return utility1;
			}
			
			newpath = (pathe + new Vector2Int(0,1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility2;
			if (hit.collider != null) {
				utility2 = Mathf.NegativeInfinity;
			} else {
				utility2 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility2 == Mathf.Infinity)
					return utility2;
			}

			newpath = (pathe + new Vector2Int(1,1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility3;
			if (hit.collider != null) {
				utility3 = Mathf.NegativeInfinity;
			} else {
				utility3 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility3 == Mathf.Infinity)
					return utility3;
			}

			newpath = (pathe + new Vector2Int(2,0));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility4;
			if (hit.collider != null) {
				utility4 = Mathf.NegativeInfinity;
			} else {
			 utility4 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
			if (utility4 == Mathf.Infinity)
				return utility4;
			}

			newpath = (pathe + new Vector2Int(1,0));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility5;
			if (hit.collider != null) {
				utility5 = Mathf.NegativeInfinity;
			} else {
				utility5 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility5 == Mathf.Infinity)
					return utility5;
			}

			newpath = (pathe + new Vector2Int(1,-1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility6;
			if (hit.collider != null) {
				utility6 = Mathf.NegativeInfinity;
			} else {
				utility6 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility6 == Mathf.Infinity)
					return utility6;
			}

			newpath = (pathe + new Vector2Int(0,-2));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility7;
			if (hit.collider != null) {
				utility7 = Mathf.NegativeInfinity;
			} else {
				 utility7 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility7 == Mathf.Infinity)
					return utility7;
			}

			newpath = (pathe + new Vector2Int(0,-1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility8;
			if (hit.collider != null) {
				utility8 = Mathf.NegativeInfinity;
			} else {
				utility8 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility8 == Mathf.Infinity)
					return utility8;
			}

			newpath = (pathe + new Vector2Int(-1,-1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility9;
			if (hit.collider != null) {
				utility9 = Mathf.NegativeInfinity;
			} else {
				utility9 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility9 == Mathf.Infinity)
					return utility9;
			}

			newpath = (pathe + new Vector2Int(-2,0));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility10;
			if (hit.collider != null) {
				utility10 = Mathf.NegativeInfinity;
			} else {
				utility10 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility10 == Mathf.Infinity)
					return utility10;
			}

			newpath = (pathe + new Vector2Int(-1,0));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility11;
			if (hit.collider != null) {
				utility11 = Mathf.NegativeInfinity;
			} else {
				utility11 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility11 == Mathf.Infinity)
					return utility11;
			}

			newpath = (pathe + new Vector2Int(-1,1));
			hit = Physics2D.Raycast (enemy.transform.position, newpath, newpath.magnitude, wallmask);
			float utility12;
			if (hit.collider != null) {
				utility12 = Mathf.NegativeInfinity;
			} else {
				utility12 = minimax (enemy, newpath, pathp, depth - 1, false, plenergy);
				if (utility12 == Mathf.Infinity)
					return utility12;
			}


			float maxutlity = Mathf.Max (utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12);
			return maxutlity;

		} else{

			Vector2Int newpath = (pathp + new Vector2Int(0,3));
			RaycastHit2D hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility1;
			if (hit.collider != null) {
				utility1 = Mathf.Infinity;
			} else {
				utility1 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility1 == Mathf.NegativeInfinity)
					return utility1;
			}

			newpath = (pathp + new Vector2Int(0,2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility2;
			if (hit.collider != null) {
				utility2 = Mathf.Infinity;
			} else {
				utility2 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility2 == Mathf.NegativeInfinity)
					return utility2;
			}

			newpath = (pathp + new Vector2Int(0,1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility3;
			if (hit.collider != null) {
				utility3 = Mathf.Infinity;
			} else {
				utility3 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 1);
				if (utility3 == Mathf.NegativeInfinity)
					return utility3;
			}

			newpath = (pathp + new Vector2Int(1,2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility4;
			if (hit.collider != null) {
				utility4 = Mathf.Infinity;
			} else {
				utility4 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility4 == Mathf.NegativeInfinity)
					return utility4;
			}

			newpath = (pathp + new Vector2Int(2,1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility5;
			if (hit.collider != null) {
				utility5 = Mathf.Infinity;
			} else {
			utility5 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy-3);
			if (utility5 == Mathf.NegativeInfinity)
				return utility5;
			}

			newpath = (pathp + new Vector2Int(1,1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility6;
			if (hit.collider != null) {
				utility6 = Mathf.Infinity;
			} else {
				utility6 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility6 == Mathf.NegativeInfinity)
					return utility6;
			}

			newpath = (pathp + new Vector2Int(3,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility7;
			if (hit.collider != null) {
				utility7 = Mathf.Infinity;
			} else {
				utility7 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility7 == Mathf.NegativeInfinity)
					return utility7;
			}

			newpath = (pathp + new Vector2Int(2,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility8;
			if (hit.collider != null) {
				utility8 = Mathf.Infinity;
			} else {
				utility8 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility8 == Mathf.NegativeInfinity)
					return utility8;
			}

			newpath = (pathp + new Vector2Int(1,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility9;
			if (hit.collider != null) {
				utility9 = Mathf.Infinity;
			} else {
				utility9 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 1);
				if (utility9 == Mathf.NegativeInfinity)
					return utility9;
			}

			newpath = (pathp + new Vector2Int(2,-1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility10;
			if (hit.collider != null) {
				utility10 = Mathf.Infinity;
			} else {
				utility10 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility10 == Mathf.NegativeInfinity)
					return utility10;
			}

			newpath = (pathp + new Vector2Int(1,-1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility11;
			if (hit.collider != null) {
				utility11 = Mathf.Infinity;
			} else {
				utility11 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility11 == Mathf.NegativeInfinity)
					return utility11;
			}

			newpath = (pathp + new Vector2Int(1,-2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility12;
			if (hit.collider != null) {
				utility12 = Mathf.Infinity;
			} else {
				utility12 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility12 == Mathf.NegativeInfinity)
					return utility12;
			}

			newpath = (pathp + new Vector2Int(0,-3));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility13;
			if (hit.collider != null) {
				utility13 = Mathf.Infinity;
			} else {
				utility13 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility13 == Mathf.NegativeInfinity)
					return utility13;
			}

			newpath = (pathp + new Vector2Int(0,-2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility14;
			if (hit.collider != null) {
				utility14 = Mathf.Infinity;
			} else {
				utility14 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility14 == Mathf.NegativeInfinity)
					return utility14;
			}

			newpath = (pathp + new Vector2Int(0,-1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility15;
			if (hit.collider != null) {
				utility15 = Mathf.Infinity;
			} else {
				utility15 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 1);
				if (utility15 == Mathf.NegativeInfinity)
					return utility15;
			}

			newpath = (pathp + new Vector2Int(-1,-2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility16;
			if (hit.collider != null) {
				utility16 = Mathf.Infinity;
			} else {
				utility16 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility16 == Mathf.NegativeInfinity)
					return utility16;
			}

			newpath = (pathp + new Vector2Int(-1,-1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility17;
			if (hit.collider != null) {
				utility17 = Mathf.Infinity;
			} else {
				utility17 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility17 == Mathf.NegativeInfinity)
					return utility17;
			}

			newpath = (pathp + new Vector2Int(-2,-1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility18;
			if (hit.collider != null) {
				utility18 = Mathf.Infinity;
			} else {
				utility18 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility18 == Mathf.NegativeInfinity)
					return utility18;
			}

			newpath = (pathp + new Vector2Int(-3,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility19;
			if (hit.collider != null) {
				utility19 = Mathf.Infinity;
			} else {
				utility19 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility19 == Mathf.NegativeInfinity)
					return utility19;
			}

			newpath = (pathp + new Vector2Int(-2,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility20;
			if (hit.collider != null) {
				utility20 = Mathf.Infinity;
			} else {
				utility20 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility20 == Mathf.NegativeInfinity)
					return utility20;
			}

			newpath = (pathp + new Vector2Int(-1,0));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility21;
			if (hit.collider != null) {
				utility21 = Mathf.Infinity;
			} else {
				utility21 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 1);
				if (utility21 == Mathf.NegativeInfinity)
					return utility21;
			}

			newpath = (pathp + new Vector2Int(-2,1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility22;
			if (hit.collider != null) {
				utility22 = Mathf.Infinity;
			} else {
				utility22 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility22 == Mathf.NegativeInfinity)
					return utility22;
			}

			newpath = (pathp + new Vector2Int(-1,2));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility23;
			if (hit.collider != null) {
				utility23 = Mathf.Infinity;
			} else {
				utility23 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 3);
				if (utility23 == Mathf.NegativeInfinity)
					return utility23;
			}

			newpath = (pathp + new Vector2Int(-1,1));
			hit = Physics2D.Raycast (player.transform.position, newpath, newpath.magnitude, wallmask);
			float utility24;
			if (hit.collider != null) {
				utility24 = Mathf.Infinity;
			} else {
				utility24 = minimax (enemy, pathe, newpath, depth - 1, true, plenergy - 2);
				if (utility24 == Mathf.NegativeInfinity)
					return utility24;
			}


			float minutility = Mathf.Min (utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12, utility13, utility14, utility15, utility16, utility17, utility18, utility19, utility20, utility21, utility22, utility23, utility24);
			return minutility;
		}
	}

	float terminaltest (GameObject enemy, Vector2Int pathe, Vector2Int pathp, int depth, bool isMax, float plenergy) {
		//implement wall detection here
		//converting Vector2Int to Vector3 for adding to Gameobjects
		Vector2 path1 = pathe;
		Vector2 path2 = pathp;
		Vector3 path3 = path1;
		Vector3 path4 = path2;

		float utility = 0;

		wallmask = LayerMask.GetMask ("Wall");//Define what Layers the raycast should look for
		energymask = LayerMask.GetMask ("Energy");

		if (isMax) {
			//check distance to other enemies
			foreach (GameObject otherenemy in enemies) {
				if (enemy.transform.position != otherenemy.transform.position) {
					float diste = Vector3.Distance ((enemy.transform.position + path3), otherenemy.transform.position);
					if (diste < 0.6)
						return Mathf.NegativeInfinity;
					else if (diste < 1.6)
						utility -= 10;
					else if (diste < 2.6)
						utility -= 2;
					else if (diste < 3.6)
						utility -= 1;
				}
			}
		} else {
			RaycastHit2D collect = Physics2D.Raycast(player.transform.position, pathp,((4-depth)*(3/2)), energymask);
			if (collect.collider != null) {
				plenergy += 20;
			}
		}

		if (Vector3.Distance ((enemy.transform.position + path3), (player.transform.position + path4)) < 0.6)
			return Mathf.Infinity;
		else if (Vector3.Distance ((player.transform.position + path4), Exit.transform.position) < 0.6)
			return Mathf.NegativeInfinity;
		else if (plenergy <= 0)
			return Mathf.Infinity;
		else if (depth == 0) {
			float dist1 = Vector3.Distance((enemy.transform.position + path3),(player.transform.position + path4));
			float dist2 = Vector3.Distance((player.transform.position + path4), Exit.transform.position);
			//distance to other enemies, type of road still missing
			utility += ((-dist1) + (dist2 / 2) - plenergy);
			return utility;
		}
		else
			return 9991019;
	}
}
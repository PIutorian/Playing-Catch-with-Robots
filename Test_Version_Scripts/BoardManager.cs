using UnityEngine;
using System;
using System.Collections.Generic; 
using Random = UnityEngine.Random;  

namespace Completed

{

	public class BoardManager : MonoBehaviour
	{
		public int columns;                                         
		public int rows;                                            

		public GameObject[] energytile;                                     
		public GameObject[] buildingtile;                              
		public GameObject roadtile; 
		public GameObject[] concretetile;  
		public GameObject[] invisibletile;
		public GameObject player;
		private Transform boardHolder;                                     

		public void SetupScene ()
		{
			//Creates the outer walls and floor.
			BoardSetup ();
			concretetile = GameObject.FindGameObjectsWithTag ("concrete");
			invisibletile = GameObject.FindGameObjectsWithTag ("wall");
			foreach (GameObject concrete in concretetile) {
				concrete.transform.position = new Vector3 (concrete.transform.position.x * 5, concrete.transform.position.y * 5, concrete.transform.position.z);
			}
			foreach (GameObject wall in invisibletile) {
				wall.transform.position = new Vector3 (wall.transform.position.x * 5, wall.transform.position.y * 5, wall.transform.position.z);
			}
		}
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;

			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = -1; x < columns + 1; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = -1; y < rows + 1; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = concretetile[Random.Range (0,concretetile.Length)];

					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = invisibletile [Random.Range (0, invisibletile.Length)];

					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}
	}
} 



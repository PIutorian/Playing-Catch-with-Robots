using UnityEngine;
using System.Collections;

using System.Collections.Generic;        //Allows us to use Lists. 
using Completed;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
	public bool playersturn = true;							//defining wheter the player or the enemies are moving
	public int playerenergy = 100;									//saving the amount of Energy over multiple levels
	public int level = 1;									//current level/score


	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)

			instance = this;

		else if (instance != this)

			Destroy(gameObject);    

		//Sets this to not be destroyed/reset when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}
		
	void InitGame()
	{
		boardScript.SetupScene();
	}
}
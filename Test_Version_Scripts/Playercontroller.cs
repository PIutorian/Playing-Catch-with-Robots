using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

	public int Energy;	//Amount of Energy (Life) in the Robot (Player)
	public int batteryload; //Amount of Energy in a Batterie
	public GameObject Marker1;
	public GameObject Marker2;
	public GameObject Marker3; //Movementmarker 1-3
	private GameObject Marker1set;
	private GameObject Marker2set;
	private GameObject Marker3set; //Instantiated Markers

	private int countermax = 3; //Maximum walking distance
	private int counter = 0; //Number of markers placed
	private Rigidbody2D rigidb;
	private BoxCollider2D boxcoll;	//Rigidbody and Collider of the Player
	private float deltatime;	//referencetime for cooldown
	private float cooldown = 0.2f; // Cooldown for direction buttons
	private float speed = 0.01f; //movement speed of the player

	private int horizontal = 0;
	private int vertical = 0;		//horizontal and vertical Keyboard input

	void Start (){
		Energy = GameManager.instance.playerenergy;
	}
	void Update(){
		if (!GameManager.instance.playersturn) {		//check if it's the players turn
			return;
		}

		int horizontalinput = (int)(Input.GetAxisRaw ("Horizontal"));
		int verticalinput = (int)(Input.GetAxisRaw ("Vertical"));	//Get any new Input from the Keyboard in 1;0;-1

		if (horizontalinput != 0) {
			verticalinput = 0; //no diagonal movement possible
		}


		if ((horizontalinput != 0 || verticalinput != 0) && (Time.time > deltatime) && (counter <= countermax)) { //Check if there is a new input

			LayerMask mask = LayerMask.GetMask ("Wall");//Define what Layers the raycast should look for

			RaycastHit2D hit = Physics2D.Raycast (new  Vector2 (transform.position.x + horizontal, transform.position.y + vertical), new Vector2 (horizontalinput, verticalinput), 1, mask);
			//Check if there is a wall
			if (hit.collider != null) {
				//	Debug.Log (hit);
				return;
			}

			horizontal += horizontalinput;
			vertical += verticalinput;

			if (counter == 3) {
				if (transform.position + new Vector3 (horizontal, vertical) == Marker2set.transform.position) {
					Destroy (Marker3set);
					counter--;
					Energy++;
				} else {
					horizontal -= horizontalinput;
					vertical -= verticalinput;
					return;
				}
			} else if (counter == 2) {
				if (transform.position + new Vector3 (horizontal, vertical) == Marker1set.transform.position) {
					Destroy (Marker2set);
					counter--;
					Energy++;
				} else if (Energy >= 1) {
					Marker3set = Instantiate (Marker3, transform.position + new Vector3 (horizontal, vertical), Quaternion.identity);
					counter++;
					Energy--;
				}
			} else if (counter == 1) {
				if (horizontal == 0 && vertical == 0) {
					Destroy (Marker1set);
					counter--;
					Energy++;
				} else if (Energy >= 1) {
					Marker2set = Instantiate (Marker2, transform.position + new Vector3 (horizontal, vertical), Quaternion.identity);
					counter++;
					Energy--;
				}

			} else if (counter == 0 && Energy >= 1) {
				Marker1set = Instantiate (Marker1, transform.position + new Vector3 (horizontal, vertical), Quaternion.identity);
				counter++;
				Energy--;
			} 
			deltatime = cooldown + Time.time;
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			//			Debug.Log("Return key was pressed.");
			if (counter == 0)
				return;
			else if (counter >= 1) {

				while (Vector3.Distance (transform.position, Marker1set.transform.position) > 0.001f) {
					float step = speed * Time.deltaTime;
					transform.position = Vector3.MoveTowards (transform.position, Marker1set.transform.position, step); //ugly,  maybe smoother movement?
				}

				Destroy (Marker1set);

				if (counter >= 2) {

					while (Vector3.Distance (transform.position, Marker2set.transform.position) > 0.001f) {
						float step = speed * Time.deltaTime;
						transform.position = Vector3.MoveTowards (transform.position, Marker2set.transform.position, step); //ugly,  maybe smoother movement?
					}

					Destroy (Marker2set);

					if (counter >= 3) {
						while (Vector3.Distance (transform.position, Marker3set.transform.position) > 0.001f) {
							float step = speed * Time.deltaTime;
							transform.position = Vector3.MoveTowards (transform.position, Marker3set.transform.position, step); //ugly,  maybe smoother movement?
							transform.position = Marker3set.transform.position;
						}

						Destroy (Marker3set);
					}
				}
				horizontal = vertical = counter = 0; //reset evrything
				if (Energy <= 0) {
					Debug.Log ("Game Over: out of Energy");
				}
			}
			GameManager.instance.playerenergy = Energy; //Uplad new amount of Energx
			GameManager.instance.playersturn = false; //End players turn, start Enemies
		}
	}
	private void OnTriggerEnter2D (Collider2D other) {
		
		if(other.gameObject.CompareTag("Exit")){
			Debug.Log ("Goal reached!");
  		}
		else if (other.gameObject.CompareTag("Energy"))
		{
			Energy += batteryload;
			Destroy (other.gameObject);
			Debug.Log ("Energy collected!");
		}
		else if(other.gameObject.CompareTag("Enemy"))
		{
			Debug.Log ("Game Over!");
		}
	}
}
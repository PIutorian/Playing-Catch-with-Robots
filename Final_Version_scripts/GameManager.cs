using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private BoardManager boardScript;
    public bool playersturn = true;                         //defining wheter the player or the enemies are moving
    public int playerenergy = 100;							//saving the amount of Energy over multiple levels

    public bool mainroadgenerated = false;
    public bool streetsgenerated = false;
    public bool backgroundgenerated = false;
    public bool enemiesspawnedin = false;
    public bool energyspawnedin = false;
    public bool levelgenerated = false;




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
    }
    private void Update()
    {
        if (mainroadgenerated && streetsgenerated && backgroundgenerated && enemiesspawnedin && energyspawnedin)
        {
            levelgenerated = true;
            print("level generation finished!");
            mainroadgenerated = false;
            streetsgenerated = false;
            backgroundgenerated = false;
            enemiesspawnedin = false;
            energyspawnedin = false;
            levelgenerated = false;
        }
    }
}
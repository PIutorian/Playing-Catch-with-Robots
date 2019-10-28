using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Completed;

public class roadnet : MonoBehaviour
{

    public float facingdirection = 1; //can be either 1(north), 2(east) or 3(west); as the mainroad only travels up, south is ignored.
    public float distancetoedge = 0; //+1 when closer to east side and -1 when closer to the west side
    public float nextspawnx; //dictates the value for the next road to spawn
    public float nextspawny;
    private float height; // borrows the height of the map from the game manager

    private double random;

    //possible MAINroads
    public GameObject roadN;
    public GameObject roadWE;
    public GameObject roadEW;
    public GameObject turnroadE;
    public GameObject turnroadW;
    public GameObject turnroadNE;
    public GameObject turnroadNW;

    //MAINchances
    private double turnWchance;
    private double turnEchance;
    private double turnNEchance;
    private double turnNWchance;
    private double roadWchance;
    private double roadNchance;
    private double roadEchance;

    //SMALLroads generating
    private GameObject[] roadspawnersW;
    private GameObject[] roadspawnersE;
    private float roadinterval;
    public GameObject smallroad;


    //isexit
    private GameObject[] mainroads;
    public GameObject exit;

    void Start()
    {
        height = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<BoardManager>().heightandwidth;
        height = height * 5 - 15;
        InvokeRepeating("Spawnmainroad", 0, 0.05F);
    }

    void Spawnmainroad()
    {
        turnEchance = 0;
        turnWchance = 0;
        turnNEchance = 0;
        turnNWchance = 0;
        roadWchance = 0;
        roadNchance = 0;
        roadEchance = 0;

        if (nextspawny >= height)
        {
            if (facingdirection == 1)
            {
                Instantiate(roadN, new Vector2(transform.position.x + nextspawnx, transform.position.y + nextspawny + 10), roadN.transform.rotation, transform);
                nextspawny += 15;
            }
            if (facingdirection == 2)
            {
                Instantiate(turnroadNE, new Vector2(transform.position.x + nextspawnx + 15, transform.position.y + nextspawny), turnroadNE.transform.rotation, transform);
                nextspawny += 10;
            }
            if (facingdirection == 3)
            {
                Instantiate(turnroadNW, new Vector2(transform.position.x + nextspawnx - 15, transform.position.y + nextspawny), turnroadNW.transform.rotation, transform);
                nextspawny += 10;
            }
            CancelInvoke();
            spawnstreets();
            //spawn exit
            mainroads = GameObject.FindGameObjectsWithTag("mainroad");
            mainroads = mainroads.OrderBy(mainroad => mainroad.transform.position.y).ToArray(); //uses system linq
            GameObject finalroad = mainroads[mainroads.Length - 1];
            Instantiate(exit, finalroad.transform.position, Quaternion.identity);
            //exit spawned
            return;
        }
        if (facingdirection == 1 && nextspawny == 0)
        {
            roadNchance = 1;
        }
        if (facingdirection == 1 && nextspawny != 0)
        {
            turnEchance = 0.125;
            turnWchance = 0.125;
            roadNchance = 0.75;
        }
        if (facingdirection == 2 && distancetoedge < 2)
        {
            turnNEchance = 0.2;
            roadEchance = 0.8;
        }
        if (facingdirection == 3)
        {
            turnNWchance = 0.2;
            roadWchance = 0.8;
        }
        if (distancetoedge <= -2 && facingdirection == 1)
        {
            turnEchance = 0.75;
            roadNchance = 0.25;
        }
        if (distancetoedge <= -2 && facingdirection == 3)
        {
            turnNWchance = 1;
        }
        if (distancetoedge >= 2 && facingdirection == 1)
        {
            turnWchance = 0.75;
            roadNchance = 0.25;
        }
        if (distancetoedge >= 2 && facingdirection == 2)
        {
            turnNEchance = 1;
        }
        random = Random.value;
        if (random < turnEchance)
        {
            Instantiate(turnroadE, new Vector2(transform.position.x + nextspawnx, transform.position.y + nextspawny + 15), turnroadE.transform.rotation, transform);
            facingdirection = 2;
            distancetoedge += 1;
            nextspawnx += 10;
            nextspawny += 15;
            return;
        }
        else if (random < turnEchance + turnWchance)
        {
            Instantiate(turnroadW, new Vector2(transform.position.x + nextspawnx, transform.position.y + nextspawny + 15), turnroadW.transform.rotation, transform);
            facingdirection = 3;
            distancetoedge += -1;
            nextspawnx += -10;
            nextspawny += 15;
            return;
        }
        else if (random < turnEchance + turnWchance + turnNEchance)
        {
            Instantiate(turnroadNE, new Vector2(transform.position.x + nextspawnx + 15, transform.position.y + nextspawny), turnroadNE.transform.rotation, transform);
            facingdirection = 1;
            distancetoedge += 1;
            nextspawny += 10;
            nextspawnx += 15;
            return;
        }
        else if (random < turnEchance + turnWchance + turnNEchance + turnNWchance)
        {
            Instantiate(turnroadNW, new Vector2(transform.position.x + nextspawnx - 15, transform.position.y + nextspawny), turnroadNW.transform.rotation, transform);
            facingdirection = 1;
            distancetoedge += -1;
            nextspawny += 10;
            nextspawnx += -15;
            return;
        }
        else if (random < turnEchance + turnWchance + turnNEchance + turnNWchance + roadWchance)
        {
            Instantiate(roadEW, new Vector2(transform.position.x + nextspawnx - 10, transform.position.y + nextspawny), roadEW.transform.rotation, transform);
            facingdirection = 3;
            distancetoedge += -1;
            nextspawnx += -15;
            return;
        }
        else if (random < turnEchance + turnWchance + turnNEchance + turnNWchance + roadWchance + roadNchance)
        {
            Instantiate(roadN, new Vector2(transform.position.x + nextspawnx, transform.position.y + nextspawny + 10), roadN.transform.rotation, transform);
            facingdirection = 1;
            nextspawny += 15;
            return;
        }
        else if (random < turnEchance + turnWchance + turnNEchance + turnNWchance + roadWchance + roadNchance + roadEchance)
        {
            Instantiate(roadWE, new Vector2(transform.position.x + nextspawnx + 10, transform.position.y + nextspawny), roadWE.transform.rotation, transform);
            facingdirection = 2;
            distancetoedge += 1;
            nextspawnx += 15;
            return;
        }
    }

    void spawnstreets()
    { //spawning streets

        roadinterval = 1;
        roadspawnersW = GameObject.FindGameObjectsWithTag("roadspawnW");
        //roadspawnersW = roadspawnersW.OrderBy(roadspawnerW => roadspawnerW.transform.position.y).ThenBy(roadspawnerW => roadspawnerW.transform.position.x).ToArray();
        roadspawnersW = roadspawnersW.OrderBy(roadspawnerW => Vector2.Distance(this.transform.position, roadspawnerW.transform.position)).ToArray();
        roadspawnersE = GameObject.FindGameObjectsWithTag("roadspawnE");
        roadspawnersE = roadspawnersE.OrderBy(roadspawnerE => Vector2.Distance(this.transform.position, roadspawnerE.transform.position)).ToArray();

        foreach (GameObject roadspawnerW in roadspawnersW)
        {
            if (roadinterval < 1)
            {

                Instantiate(smallroad, roadspawnerW.transform.position, Quaternion.Inverse(roadspawnerW.transform.parent.transform.localRotation), transform);
                roadinterval = ((float)Random.value * 2F) + 2F;

            }
            else
            {
                roadinterval += -1;
            }
        }
        roadinterval = 1;
        foreach (GameObject roadspawnerE in roadspawnersE)
        {
            if (roadinterval < 1)
            {

                Instantiate(smallroad, roadspawnerE.transform.position, Quaternion.Inverse(roadspawnerE.transform.parent.transform.localRotation), transform);
                roadinterval = (Random.value * 2F) + 2F;

            }
            else
            {
                roadinterval += -1;
            }
        }

        GameObject[] streets = GameObject.FindGameObjectsWithTag("street"); //delete uppermost two streets
        foreach (GameObject street in streets)
        {
            if (street.transform.position.y >= nextspawny - 10)
            {
                Destroy(street);
            }
        }

        GameManager.instance.mainroadgenerated = true;
        gameObject.GetComponent<roadcrosscorrect>().Crosscorrect();
        GameObject.FindGameObjectWithTag("gamemanager").GetComponent<BoardManager>().generatetextures();

    }
}

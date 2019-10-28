using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Smallroadnet10 : MonoBehaviour
{
    public float count = 0;
    public float countb = 0;

    public GameObject[] streetspawners;
    public float Randomnumber;
    private float savedrotation;

    private bool turnnearby;
    private bool north;
    private bool south;
    private bool east;
    private bool west;

    private bool spnorth;
    private bool spsouth;
    private bool speast;
    private bool spwest;


    private bool hl; //housetoleft
    private bool hr; //housetoright
    private bool roadahead;
    private bool streetahead;
    private bool houseahead;

    private float distancetomainroad; //don't go further than twenty blocks

    private RaycastHit2D left;
    private RaycastHit2D right;
    private RaycastHit2D straightahead;
    private Vector2 endvectorstraightahead;
    public LayerMask roadnet;
    public LayerMask smallroadnet;
    public LayerMask housing;

    public GameObject street;
    public GameObject streetturnleft;
    public GameObject streetturnright;
    public GameObject deadend;
    public GameObject Tcrossing;


    public float threshholddistance = 35;

    void Update()
    {
       /* bool roadsspawnedin = GameObject.FindGameObjectWithTag("roadnet").GetComponent<roadnet>().roadsspawnedin;
        if (roadsspawnedin)
        {
            InvokeRepeating("streetspawner", 2F, 0.5F);
            enabled = false;
        }*/

    }

    void streetspawner()
    {
        count++;
        if (count > 20)
        {
            CancelInvoke();
            gameObject.GetComponent<streetspritechanger>().spritechanger();
            GameObject.FindGameObjectWithTag("realestate").GetComponent<realestate>().houseplanter();
        }

        streetspawners = GameObject.FindGameObjectsWithTag("streetspawner");

        if (streetspawners != null)
        {
            foreach (GameObject streetspawner in streetspawners)
            {
                countb++;

                //reset variables and bools
                Randomnumber = Random.value;
                hl = false;
                hr = false;
                houseahead = false;
                roadahead = false;
                streetahead = false;

                distancetomainroad = streetspawner.gameObject.GetComponent<streetdetector>().distancetomainroad;
                turnnearby = streetspawner.gameObject.GetComponent<streetdetector>().turnnearby;

                north = streetspawner.gameObject.GetComponent<streetdetector>().north;
                south = streetspawner.gameObject.GetComponent<streetdetector>().south;
                east = streetspawner.gameObject.GetComponent<streetdetector>().east;
                west = streetspawner.gameObject.GetComponent<streetdetector>().west;

                spnorth = streetspawner.gameObject.GetComponent<streetdetector>().spnorth; //see if an additional streetspawner in the vicinity
                spsouth = streetspawner.gameObject.GetComponent<streetdetector>().spsouth;
                speast = streetspawner.gameObject.GetComponent<streetdetector>().speast;
                spwest = streetspawner.gameObject.GetComponent<streetdetector>().spwest;

            

                if (north && south && !east && !west) //if theres a street/road N and S, instantiate a street
                {
                    Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, 0), transform);
                    Destroy(streetspawner);
                    continue;
                }
                if (!north && !south && east && west) //if theres a street/road E and W, instantiate a street
                { Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, 90), transform); 
                    Destroy(streetspawner); 
                    continue;   
                }

                //check for special instance b

                if (west && spsouth)
                { Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, -90), transform); Destroy(streetspawner); continue; }
                if (east && spsouth)
                { Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, 90), transform); Destroy(streetspawner); continue; }
                if (north && spsouth)
                { Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, 0), transform); Destroy(streetspawner); continue; }

                if (west && speast)
                { Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, -90), transform); Destroy(streetspawner); continue; }
                if (south && speast)
                { Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, 0), transform); Destroy(streetspawner); continue; }
                if (north && speast)
                { Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, 180), transform); Destroy(streetspawner); continue; }

                if (south && spwest)
                { Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, -90), transform); Destroy(streetspawner); continue; }
                if (east && spwest)
                { Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, 90), transform); Destroy(streetspawner); continue; }
                if (north && spwest)
                { Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, 180), transform); Destroy(streetspawner); continue; }

                if (west && spnorth)
                { Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, -90), transform); Destroy(streetspawner); continue; }
                if (east && spnorth)
                { Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, 90), transform); Destroy(streetspawner); continue; }
                if (south && spnorth)
                { Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, 0), transform); Destroy(streetspawner); continue; }

                //check if adjacent street is N/S/E/W because rotation cannot be used to remember the direction of the original road; shoot linecasts
                if (north)
                {
                    savedrotation = 180; //the rotation of the to-be instantiated street
                    endvectorstraightahead = new Vector2(streetspawner.transform.position.x, streetspawner.transform.position.y-10); //target coordinate of the linecast
                    left = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x + 5, streetspawner.transform.position.y), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position .x - 5, streetspawner.transform.position.y), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (south)
                {
                    savedrotation = 0;
                    endvectorstraightahead = new Vector2(streetspawner.transform.position.x, streetspawner.transform.position.y + 10);
                    left = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x - 5, streetspawner.transform.position.y), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x + 5, streetspawner.transform.position.y), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (west)
                {
                    savedrotation = -90;
                    endvectorstraightahead = new Vector2(streetspawner.transform.position.x+ 10, streetspawner.transform.position.y);
                    left = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x, streetspawner.transform.position.y + 5), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x, streetspawner.transform.position.y - 5), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (east)
                {
                    savedrotation = 90;
                    endvectorstraightahead = new Vector2(streetspawner.transform.position.x-10, streetspawner.transform.position.y);
                    left = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x, streetspawner.transform.position.y - 5), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner.transform.position, new Vector2(streetspawner.transform.position.x, streetspawner.transform.position .y + 5), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                straightahead = Physics2D.Linecast(streetspawner.transform.position, endvectorstraightahead, housing); //shoot raycast straight (for houses)
                if (straightahead.transform == true) { houseahead = true; }
                straightahead = Physics2D.Linecast(streetspawner.transform.position, endvectorstraightahead, smallroadnet); //shoot raycast straight (for streets)
                if (straightahead.transform == true) { streetahead = true; }
                straightahead = Physics2D.Linecast(streetspawner.transform.position, endvectorstraightahead, roadnet); //shoot raycast straight (for mainroad)
                if (straightahead.transform == true) { roadahead = true; }


                //end of linecast checking

                if (distancetomainroad >= 35)
                {
                    Instantiate(deadend, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    Destroy(streetspawner);
                    continue;
                }


                //start of actual street instantiater

                // in case a house is ahead
                if (houseahead == true)
                {
                    //if houses left and right
                    if (hr == true && hl == true)
                    {
                        continue;
                    }
                    //if house to right
                    if (hr == true)
                    {
                            Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                            Destroy(streetspawner);
                        continue;
                    }
                    //if house to left
                    if (hl == true)
                    {
                            Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                            Destroy(streetspawner);
                        continue;
                    }
                    //if no houses left or right
                    if (Randomnumber < 0.5)
                    {
                        Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        Destroy(streetspawner);
                        continue;
                    }   
                        Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        Destroy(streetspawner);
                    continue;
                }


                // in case road ahead or houses left and right
                if (roadahead == true || streetahead == true || hl == true && hr == true)
                {
                    Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    Destroy(streetspawner);
                    continue;
                }
                // in case all clear ahead but house left
                if (hl == true)
                {
                    if (Randomnumber < 0.4) {
                        Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        Destroy(streetspawner);
                        continue;
                    }
                        Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        Destroy(streetspawner);
                    continue;
                }
                // in case all clear ahead but house right
                if (hr == true)
                {
                    if (Randomnumber < 0.3)
                    {
                        Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        Destroy(streetspawner);
                        continue;
                    }
                    Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    Destroy(streetspawner);
                    continue;
                }
                //and finally, if no colliders are detected
                if (Randomnumber < 0.2 && !turnnearby)
                {
                    Instantiate(streetturnleft, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    Destroy(streetspawner);
                    continue;
                }
                if (Randomnumber < 0.4 && !turnnearby)
                {
                    Instantiate(streetturnright, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    Destroy(streetspawner);
                    continue;
                }
                Instantiate(street, streetspawner.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                Destroy(streetspawner);
                continue;

            }
        }
    }
}

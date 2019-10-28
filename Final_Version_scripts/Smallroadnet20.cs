using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Smallroadnet20 : MonoBehaviour
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

    private bool crosscorrect;


    private float distancetomainroad; //don't go further than twenty blocks

    private RaycastHit2D left;
    private RaycastHit2D right;
    private RaycastHit2D straightahead;
    private Vector2 endvectorstraightahead;
    public LayerMask roadnet;
    public LayerMask smallroadnet;
    public LayerMask housing;
    public LayerMask combinedroadnet;

    public GameObject street;
    public GameObject streetturnleft;
    public GameObject streetturnright;
    public GameObject deadend;
    public GameObject Tcrossingright;
    public GameObject Tcrossingleft;

    private Transform streetspawner1;


    public float threshholddistance = 60;
    private float upperlimit;
    private float lowerlimit;

    private float numberofstreets;
    public Sprite STREET;
    public Sprite STREETTURNRIGHT;
    public Sprite TCROSSING1;
    public Sprite TCROSSING2;
    public Sprite DEADEND;

    private GameObject streetforcorrecting;

    void Update()
    {
        if (GameManager.instance.mainroadgenerated == true)
        {
            InvokeRepeating("Streetnet", 2F, 0.1F);
            enabled = false;
        }
    }

    void Streetnet()
    {

        upperlimit = GameObject.FindGameObjectWithTag("roadnet").GetComponent<roadnet>().nextspawny - 12.5F; //so streets won't go near the upper- and lowermost places of the map
        lowerlimit = 12.5F; //12.5F because streetspawner in the middle of the street

        count++;
        if (count > 30)
        {
            CancelInvoke();
            gameObject.GetComponent<streetcorrector>().Streetcorrector();
            GameObject.FindGameObjectWithTag("roadnet").GetComponent<roadcrosscorrect>().Crosscorrect();
            GameManager.instance.streetsgenerated = true;
            GameObject.FindGameObjectWithTag("realestate").GetComponent<realestate>().houseplanter();
        }

        streetspawners = GameObject.FindGameObjectsWithTag("streetspawner");

        if (streetspawners != null)
        {
            foreach (GameObject streetspawner in streetspawners)
            {
                countb++;
                streetspawner1 = streetspawner.transform;


                //reset variables and bools (1)
                Randomnumber = Random.value;
                hl = false;
                hr = false;
                houseahead = false;
                roadahead = false;
                streetahead = false;
                crosscorrect = false;

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

                Destroy(streetspawner);

                if (count == 1) // sometimes two streets instantiate next to one another in the first round, to fix this we check if two streetspawners are next to each other and eliminate the adjacent street(2)
                {
                    if (spnorth && east) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x + 5F, streetspawner1.transform.position.y), smallroadnet); Destroy(straightahead.collider.gameObject); continue; }
                    if (spnorth && west) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x - 5F, streetspawner1.transform.position.y), smallroadnet); Destroy(straightahead.collider.gameObject); continue; }
                    if (speast && north) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 5F), smallroadnet); Destroy(straightahead.collider.gameObject); continue; }
                    if (speast && south) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 5F), smallroadnet); Destroy(straightahead.collider.gameObject); continue; }

                    if (spwest && north) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 5F), smallroadnet); Destroy(straightahead.collider.gameObject); }
                    if (spwest && south) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 5F), smallroadnet); Destroy(straightahead.collider.gameObject); }
                    if (spsouth && west) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x - 5F, streetspawner1.transform.position.y), smallroadnet); Destroy(straightahead.collider.gameObject); }
                    if (spsouth && east) { straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x + 5F, streetspawner1.transform.position.y), smallroadnet); Destroy(straightahead.collider.gameObject); }
                }



                //here we check if two streets lie adjacent to the streetspawner, and set crosscorrect to true if necessary(3)
                if (north && south && !east && !west)
                { //NS
                    streetforcorrecting = GameObject.Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform) as GameObject; crosscorrect = true;
                }
                if (!north && !south && east && west)
                { //EW
                    streetforcorrecting = GameObject.Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform) as GameObject; crosscorrect = true;
                }
                if (north && !south && east && !west)
                { //NE
                    streetforcorrecting = GameObject.Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform) as GameObject; crosscorrect = true;
                }
                if (north && !south && !east && west)
                { //NW
                    streetforcorrecting = GameObject.Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform) as GameObject; crosscorrect = true;
                }
                if (!north && south && east && !west)
                { //SE
                    streetforcorrecting = GameObject.Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform) as GameObject; crosscorrect = true;
                }
                if (!north && south && !east && west)
                { //SW
                    streetforcorrecting = GameObject.Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform) as GameObject; crosscorrect = true;
                }

                if (crosscorrect == true)
                {
                    // streetforcorrecting.GetComponent<Collider2D>().enabled = false;
                    straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 5F), smallroadnet);
                    straightahead.collider.gameObject.GetComponent<streetcrosscorrect>().Streetcrosscorrect();
                    straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 5F), smallroadnet);
                    straightahead.collider.gameObject.GetComponent<streetcrosscorrect>().Streetcrosscorrect();
                    straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x + 5F, streetspawner1.transform.position.y), smallroadnet);
                    straightahead.collider.gameObject.GetComponent<streetcrosscorrect>().Streetcrosscorrect();
                    straightahead = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x - 5F, streetspawner1.transform.position.y), smallroadnet);
                    straightahead.collider.gameObject.GetComponent<streetcrosscorrect>().Streetcrosscorrect();
                    // streetforcorrecting.GetComponent<Collider2D>().enabled = true;
                    continue;
                }






                //here it is checked if the streetspawner is in the upper/lowerlimit of the map(4)
                if (streetspawner1.transform.position.y <= lowerlimit && east && !turnnearby) { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (streetspawner1.transform.position.y <= lowerlimit && east && turnnearby) { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (streetspawner1.transform.position.y <= lowerlimit && west) { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (streetspawner1.transform.position.y <= lowerlimit && north && !turnnearby) { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }

                if (streetspawner1.transform.position.y >= upperlimit && east && !turnnearby) { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (streetspawner1.transform.position.y >= upperlimit && east && turnnearby) { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (streetspawner1.transform.position.y >= upperlimit && west) { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (streetspawner1.transform.position.y >= upperlimit && south && !turnnearby) { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }
                //we have to add !noturnnearby to avoid two turn after each other; it will plop down a normal street as always when there's a turn nearby


                //If two streetspawners are next to each other, they connect(5)
                if (west && spsouth)
                { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (east && spsouth)
                { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (north && spsouth)
                { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }

                if (west && speast)
                { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (south && speast)
                { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }
                if (north && speast)
                { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }

                if (south && spwest)
                { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (east && spwest)
                { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (north && spwest)
                { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }

                if (west && spnorth)
                { Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                if (east && spnorth)
                { Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (south && spnorth)
                { Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }

                //if too far from mainroad, deadend
                if (distancetomainroad >= threshholddistance)
                {
                    Instantiate(deadend, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }

                //rotation cannot be used to remember the direction of the original road; shoot linecasts(6)
                if (north)
                {
                    savedrotation = 180; //the rotation of the to-be instantiated street
                    endvectorstraightahead = new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 10); //target coordinate of the linecast
                    left = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x + 5, streetspawner1.transform.position.y), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x - 5, streetspawner1.transform.position.y), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (south)
                {
                    savedrotation = 0;
                    endvectorstraightahead = new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 10);
                    left = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x - 5, streetspawner1.transform.position.y), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x + 5, streetspawner1.transform.position.y), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (west)
                {
                    savedrotation = -90;
                    endvectorstraightahead = new Vector2(streetspawner1.transform.position.x + 10, streetspawner1.transform.position.y);
                    left = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 5), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 5), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                if (east)
                {
                    savedrotation = 90;
                    endvectorstraightahead = new Vector2(streetspawner1.transform.position.x - 10, streetspawner1.transform.position.y);
                    left = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y - 5), housing);//shoot raycast left
                    if (left.transform == true) { hl = true; }
                    right = Physics2D.Linecast(streetspawner1.transform.position, new Vector2(streetspawner1.transform.position.x, streetspawner1.transform.position.y + 5), housing);//shoot raycast right
                    if (right.transform == true) { hr = true; }
                }

                straightahead = Physics2D.Linecast(streetspawner1.transform.position, endvectorstraightahead, housing); //shoot raycast straight (for houses)
                if (straightahead.transform == true) { houseahead = true; }
                straightahead = Physics2D.Linecast(streetspawner1.transform.position, endvectorstraightahead, smallroadnet); //shoot raycast straight (for streets)
                if (straightahead.transform == true) { streetahead = true; }
                straightahead = Physics2D.Linecast(streetspawner1.transform.position, endvectorstraightahead, roadnet); //shoot raycast straight (for mainroad)
                if (straightahead.transform == true) { roadahead = true; }


                //end of linecast checking

                //start of actual street instantiater(7)

                // in case a house is ahead
                if (houseahead == true && !turnnearby)
                {
                    //if houses left and right
                    if (hr == true && hl == true)
                    {
                        continue;
                    }
                    //if house to right
                    if (hr == true)
                    {
                        Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    //if house to left
                    if (hl == true)
                    {
                        Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    //if no houses left or right
                    if (Randomnumber < 0.5)
                    {
                        Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }

                //in case street ahead
                if (streetahead)
                {
                    Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    straightahead = Physics2D.Linecast(streetspawner1.transform.position, endvectorstraightahead, smallroadnet);
                    straightahead.collider.gameObject.GetComponent<streetcrosscorrect>().Streetcrosscorrect();
                    continue;
                }

                // in case houses left and right
                if (hl == true && hr == true || roadahead == true)
                {
                    Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }

                // in case all clear ahead but house left
                if (hl == true)
                {
                    if (Randomnumber < 0.3 && !turnnearby)
                    {
                        Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    if (Randomnumber < 0.5 && !turnnearby)
                    {
                        Instantiate(Tcrossingright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }

                    Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                // in case all clear ahead but house right
                if (hr == true)
                {
                    if (Randomnumber < 0.3 && !turnnearby)
                    {
                        Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    if (Randomnumber < 0.3 && !turnnearby)
                    {
                        Instantiate(Tcrossingleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                        continue;
                    }
                    Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                //and finally, if no colliders are detected
                if (Randomnumber < 0.15 && !turnnearby)
                {
                    Instantiate(streetturnleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                if (Randomnumber < 0.3 && !turnnearby)
                {
                    Instantiate(streetturnright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                if (Randomnumber < 0.45 && !turnnearby)
                {
                    Instantiate(Tcrossingleft, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                if (Randomnumber < 0.6 && !turnnearby)
                {
                    Instantiate(Tcrossingright, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                    continue;
                }
                Instantiate(street, streetspawner1.transform.position, Quaternion.Euler(0, 0, savedrotation), transform);
                continue;
            }
        }
    }
}
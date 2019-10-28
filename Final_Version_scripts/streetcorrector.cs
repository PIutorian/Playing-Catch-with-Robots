using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetcorrector : MonoBehaviour
{
    private RaycastHit2D hit;
    public LayerMask smallroadnet;

    private GameObject[] streets;

    private bool north;
    private bool east;
    private bool west;
    private bool south;

    public Sprite Tcrossingright;
    public Sprite crossing;
    public Sprite streetturnright;
    public Sprite deadend;

    public LayerMask roadnet;
    public Sprite standardstreet;
    private bool mainroadnearby;
    private float randomnumber;
    public Sprite varianta;
    public Sprite variantb;
    public Sprite variantc;
    public Sprite variantd;

    public void Streetcorrector()
    {

        streets = GameObject.FindGameObjectsWithTag("street");

        foreach (GameObject street in streets)
        {
            north = false;
            south = false;
            east = false;
            west = false;
            street.GetComponent<SpriteRenderer>().flipX = false;

            //if (street.GetComponent<streetcrosscorrect>().tobecorrected == true || street.GetComponent<streetcrosscorrect>().tobecorrected == false)
            street.GetComponent<BoxCollider2D>().enabled = false;
            hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x, street.transform.position.y + 5F), smallroadnet);
            if (hit.transform == true) { north = true; }
            hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x + 5F, street.transform.position.y), smallroadnet);
            if (hit.transform == true) { east = true; }
            hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x - 5F, street.transform.position.y), smallroadnet);
            if (hit.transform == true) { west = true; }
            hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x, street.transform.position.y - 5F), smallroadnet);
            if (hit.transform == true) { south = true; }

            //NSWE
            if (north && east && west && south)
            {
                hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x, street.transform.position.y + 2.5F), smallroadnet);
                if (hit.transform) { Destroy(street); continue; }
                street.GetComponent<SpriteRenderer>().sprite = crossing;
            }
            street.GetComponent<BoxCollider2D>().enabled = true;

            //SEW
            if (!north && south && west && east) { street.GetComponent<SpriteRenderer>().sprite = Tcrossingright; street.transform.rotation = Quaternion.Euler(0, 0, -90); continue; }

            //NEW
            if (north && !south && west && east) { street.GetComponent<SpriteRenderer>().sprite = Tcrossingright; street.transform.rotation = Quaternion.Euler(0, 0, 90); continue; }

            //NSE
            if (north && south && !west && east) { street.GetComponent<SpriteRenderer>().sprite = Tcrossingright; street.transform.rotation = Quaternion.Euler(0, 0, 0); continue; }

            //NWS
            if (north && south && west && !east) { street.GetComponent<SpriteRenderer>().sprite = Tcrossingright; street.transform.rotation = Quaternion.Euler(0, 0, 180); continue; }

            //NW
            if (north && !south && west && !east) { street.GetComponent<SpriteRenderer>().sprite = streetturnright; street.transform.rotation = Quaternion.Euler(0, 0, -180); continue; }

            //NE
            if (north && !south && !west && east) { street.GetComponent<SpriteRenderer>().sprite = streetturnright; street.transform.rotation = Quaternion.Euler(0, 0, 90); continue; }

            //SE
            if (!north && south && !west && east) { street.GetComponent<SpriteRenderer>().sprite = streetturnright; street.transform.rotation = Quaternion.Euler(0, 0, 0); continue; }

            //SW
            if (!north && south && west && !east) { street.GetComponent<SpriteRenderer>().sprite = streetturnright; street.transform.rotation = Quaternion.Euler(0, 0, -90); continue; }

            //deadend
            if (north && !south && !west && !east) { street.GetComponent<SpriteRenderer>().sprite = deadend; street.transform.rotation = Quaternion.Euler(0, 0, 180); continue; }
            if (!north && south && !west && !east) { street.GetComponent<SpriteRenderer>().sprite = deadend; street.transform.rotation = Quaternion.Euler(0, 0, 0); continue; }
            if (!north && !south && west && !east) { street.GetComponent<SpriteRenderer>().sprite = deadend; street.transform.rotation = Quaternion.Euler(0, 0, -90); continue; }
            if (!north && !south && !west && east) { street.GetComponent<SpriteRenderer>().sprite = deadend; street.transform.rotation = Quaternion.Euler(0, 0, 90); continue; }

        }

        Streetspritechanger();

    }
    private void Streetspritechanger() //change sprites of streets
    {
        streets = GameObject.FindGameObjectsWithTag("street");
        foreach (GameObject street in streets)
        {
            mainroadnearby = false;

            if (street.GetComponent<SpriteRenderer>().sprite == standardstreet)
            {
                hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x, street.transform.position.y + 5F), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x, street.transform.position.y - 5F), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x + 5F, street.transform.position.y), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(street.transform.position, new Vector2(street.transform.position.x - 5F, street.transform.position.y), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }


                if (mainroadnearby == true)
                {
                    street.GetComponent<SpriteRenderer>().sprite = varianta; continue; // make zebracrossing
                }
                randomnumber = Random.value;
                if (randomnumber < 0.025) { street.GetComponent<SpriteRenderer>().sprite = variantb; continue; }
                if (randomnumber < 0.05) { street.GetComponent<SpriteRenderer>().sprite = variantc; continue; }
                // if (randomnumber < 0.075) { street.GetComponent<SpriteRenderer>().sprite = variantd; continue; }
            }
        }
    }
}

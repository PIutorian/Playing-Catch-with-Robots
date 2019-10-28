using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class realestate : MonoBehaviour
{

    private GameObject[] housespawners;
    private GameObject house;
    public LayerMask roadnet;
    public LayerMask housing;
    private RaycastHit2D hit;

    public bool north;
    public bool south;
    public bool east;
    public bool west;
    private Transform reservedforhouse1;

    private float randomnumber;
    private bool housedecided;
    public GameObject house1;
    public GameObject house2;
    public GameObject house3;
    public GameObject house4;
    public GameObject playground;

    public void houseplanter()
    {
        housespawners = GameObject.FindGameObjectsWithTag("house");



        foreach (GameObject reservedforhouse in housespawners)
        {
            north = false;
            south = false;
            east = false;
            west = false;
            housedecided = false;
            randomnumber = Random.value;

            if (randomnumber <= 0.02 && !housedecided) { house = playground; housedecided = true; }
            if (randomnumber <= 0.1 && !housedecided) { house = house4; housedecided = true; }
            if (randomnumber <= 0.33 && !housedecided) { house = house3; housedecided = true; }
            if (randomnumber <= 0.66 && !housedecided) { house = house2; housedecided = true; }
            if (randomnumber <= 1 && !housedecided) { house = house1; housedecided = true; }


            reservedforhouse.GetComponent<Collider2D>().enabled = false;
            hit = Physics2D.Linecast(reservedforhouse.transform.position, new Vector2(reservedforhouse.transform.position.x + 2.5F, reservedforhouse.transform.position.y), housing); // check if adjecent houses
            if (hit.transform == true) { Destroy(reservedforhouse); continue; } //if it hits something, it is inside of another house

            hit = Physics2D.Linecast(reservedforhouse.transform.position, new Vector2(reservedforhouse.transform.position.x + 5F, reservedforhouse.transform.position.y), roadnet); // check if adjecent street to the north
            if (hit.transform == true)
            {
                east = true;
                //  if (hit.collider.gameObject.tag == "mainroad") { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
            }

            hit = Physics2D.Linecast(reservedforhouse.transform.position, new Vector2(reservedforhouse.transform.position.x - 5F, reservedforhouse.transform.position.y), roadnet);
            if (hit.transform == true)
            {
                west = true;
                //  if (hit.collider.gameObject.tag == "mainroad") { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
            }
            hit = Physics2D.Linecast(reservedforhouse.transform.position, new Vector2(reservedforhouse.transform.position.x, reservedforhouse.transform.position.y + 5F), roadnet);
            if (hit.transform == true)
            {
                north = true;
                // if (hit.collider.gameObject.tag == "mainroad") { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }
            }
            hit = Physics2D.Linecast(reservedforhouse.transform.position, new Vector2(reservedforhouse.transform.position.x, reservedforhouse.transform.position.y - 5F), roadnet);
            if (hit.transform == true)
            {
                south = true;
                // if (hit.collider.gameObject.tag == "mainroad") { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }
            }
            reservedforhouse.GetComponent<Collider2D>().enabled = true;

            reservedforhouse1 = reservedforhouse.transform;
            Destroy(reservedforhouse);


            if (south)
            { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 0), transform); continue; }

            randomnumber = Random.value;

            if (east && west && north)
            {
                if (randomnumber <= 0.333) { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
                if (randomnumber <= 0.666) { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }
                Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue;
            }
            if (east && north) { if (randomnumber <= 0.5) { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; } Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }
            if (west && north) { if (randomnumber <= 0.5) { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; } Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }
            if (east && west) { if (randomnumber <= 0.5) { Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; } Instantiate(house, reservedforhouse1.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }

            if (north)
            { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, 180), transform); continue; }
            if (east)
            { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, 90), transform); continue; }
            if (west)
            { Instantiate(house, reservedforhouse.transform.position, Quaternion.Euler(0, 0, -90), transform); continue; }

            if (!north && !south && !east && !west)
            {
                print("no street near house");
            }
        }
    }
}

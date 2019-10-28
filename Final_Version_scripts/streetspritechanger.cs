using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class streetspritechanger : MonoBehaviour
{
    public Sprite standard;
    public Sprite varianta;
    public Sprite variantb;
    public Sprite variantc;
    public Sprite variantd;
    private float randomnumber;
    private RaycastHit2D hit;
    public LayerMask roadnet;

    private bool mainroadnearby;


    private GameObject[] streets;


    public void spritechanger()
    {
        streets = GameObject.FindGameObjectsWithTag("street");
        foreach (GameObject street in streets)
        {
            mainroadnearby = false;

            if (street.GetComponent<SpriteRenderer>().sprite == standard)
            {
                hit = Physics2D.Linecast(street.transform.position, new Vector2(transform.position.x, transform.position.y + 5F), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 5F), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 5F, transform.position.y), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }
                hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 5F, transform.position.y), roadnet);
                if (hit.transform == true) { mainroadnearby = true; }


                if (mainroadnearby == true)
                {
                    street.GetComponent<SpriteRenderer>().sprite = varianta; continue; // make zebracrossing
                }
                randomnumber = Random.value;
                if (randomnumber < 0.05) { street.GetComponent<SpriteRenderer>().sprite = variantb; continue; }
                if (randomnumber < 0.1) { street.GetComponent<SpriteRenderer>().sprite = variantc; continue; }
                // if (randomnumber < 0.15) { street.GetComponent<SpriteRenderer>().sprite = variantd; continue; }
            }
        }
    }
}

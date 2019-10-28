using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetdetector : MonoBehaviour
{
    private RaycastHit2D hit;
    public LayerMask roadnet;
    public LayerMask housing;
    public bool north = false;
    public bool south = false;
    public bool east = false;
    public bool west = false;
    public bool turnnearby = false; //make sure not two left/right turns after each other
    public Sprite leftturn;
    public Sprite rightturn;
    public Sprite Tcross1;
    public Sprite Tcross2;

    public LayerMask streetspawners;
    public bool spnorth = false; //streetspawners, to combat special instance b
    public bool spsouth = false;
    public bool speast = false;
    public bool spwest = false;

    public GameObject[] mainroad;
    public float distancetomainroad;


    private void Awake()
    {


        //check if inside of a street or mainroad
        hit = Physics2D.Linecast(transform.position, new Vector2(0, 2.5F), roadnet);
        if (hit.distance < 2.5F)
        {
            Destroy(gameObject);
        }
        //destroy reservedforhouse if inside one

        else
        {
            //check if the adjacent street is N/S/E/W
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 5F), roadnet); // check if adjecent street to the north
            if (hit.transform == true)
            {
                north = true;
                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == leftturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == rightturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross1 || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross2) { turnnearby = true; } //see if turn nearby to avoid U-turn
            }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 5F), roadnet); // check if adjecent street to the north
            if (hit.transform == true)
            {
                south = true;
                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == leftturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == rightturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross1 || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross2) { turnnearby = true; }
            }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 5F, transform.position.y), roadnet); // check if adjecent street to the north
            if (hit.transform == true)
            {
                east = true;
                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == leftturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == rightturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross1 || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross2) { turnnearby = true; }
            }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 5F, transform.position.y), roadnet); // check if adjecent street to the north
            if (hit.transform == true)
            {
                west = true;
                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == leftturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == rightturn || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross1 || hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == Tcross2) { turnnearby = true; }
            }

            gameObject.GetComponent<Collider2D>().enabled = false;

            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 5F), streetspawners); // check if adjecent street to the north
            if (hit.transform == true) { spnorth = true; }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 5F), streetspawners); // check if adjecent street to the south
            if (hit.transform == true) { spsouth = true; }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x + 5F, transform.position.y), streetspawners); // check if adjecent street to the east
            if (hit.transform == true) { speast = true; }
            hit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x - 5F, transform.position.y), streetspawners); // check if adjecent street to the west
            if (hit.transform == true) { spwest = true; }

            gameObject.GetComponent<Collider2D>().enabled = true;

            mainroad = GameObject.FindGameObjectsWithTag("mainroad");

            distancetomainroad = 100;

            foreach (GameObject mainroad in mainroad)
            {
                if (Vector3.Distance(mainroad.transform.position, transform.position) < distancetomainroad)
                {
                    distancetomainroad = Vector3.Distance(mainroad.transform.position, transform.position);
                }
            }
        }
    }
}

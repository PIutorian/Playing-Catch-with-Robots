using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class roadcrosscorrect : MonoBehaviour
{

    public bool mainroadfixed = false;
    public bool roadsspawnedin;

    private bool roadtoright;
    private bool roadtoleft;
    private GameObject[] mainroad;
    private RaycastHit2D hit;
    private RaycastHit2D hit2;
    public LayerMask roadnet;
    private BoxCollider2D collider;

    public Sprite straightroad;
    public Sprite sprite4;
    public Sprite sprite3;
    public Sprite sprite5;

    private Vector2 start;
    private float desx1 = 0;
    private float desx2 = 0;
    private float desy1 = 0;
    private float desy2 = 0;

    private bool specialtreatment = false;


    public void Crosscorrect()
    {
        mainroad = GameObject.FindGameObjectsWithTag("mainroad");
        mainroad = mainroad.OrderBy(mainroad => Random.value).ToArray();

        foreach (GameObject mainroad in mainroad)
        {

            if (mainroad.GetComponent<SpriteRenderer>().sprite != straightroad)
            {
                continue;
            }


            if (mainroad.GetComponent<BoxCollider2D>() != null)
            {
                collider = mainroad.GetComponent<BoxCollider2D>();
                collider.enabled = false;
            }

            start = mainroad.transform.position;
            if (mainroad.transform.rotation.eulerAngles.z == 180 || mainroad.transform.rotation.eulerAngles.z == 0)
            {
                desx1 = mainroad.transform.position.x;
                desx2 = mainroad.transform.position.x;
                desy1 = mainroad.transform.position.y + 5;
                desy2 = mainroad.transform.position.y - 5;
            }
            else
            {
                desx1 = mainroad.transform.position.x + 5;
                desx2 = mainroad.transform.position.x - 5;
                desy1 = mainroad.transform.position.y;
                desy2 = mainroad.transform.position.y;
            }

            hit = Physics2D.Linecast(new Vector2(start.x, start.y), new Vector2(desx1, desy1), roadnet);
            if (hit.transform == null)
            {
                roadtoleft = false;
            }
            else
            {
                roadtoleft = true;
            }
            hit2 = Physics2D.Linecast(new Vector2(start.x, start.y), new Vector2(desx2, desy2), roadnet);
            if (hit2.transform == null)
            {
                roadtoright = false;
            }
            else
            {
                roadtoright = true;
            }
            if (mainroad.GetComponent<BoxCollider2D>() != null)
            {
                collider.enabled = true;
            }

            /* main road with crack
            if (roadtoleft == false && roadtoright == false && !specialtreatment)
            {
                mainroad.GetComponent<SpriteRenderer>().sprite = sprite5;
                specialtreatment = true;
            }
            */

            if (roadtoleft == true && roadtoright == true)
            {
                mainroad.GetComponent<SpriteRenderer>().sprite = sprite4;
            }
            if (roadtoleft == true && roadtoright == false)
            {
                mainroad.GetComponent<SpriteRenderer>().sprite = sprite3;
                if (mainroad.transform.rotation.eulerAngles.z == 90 || mainroad.transform.rotation.eulerAngles.z == 180)
                {
                    mainroad.GetComponent<SpriteRenderer>().flipY = true;
                    mainroad.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            if (roadtoright == true && roadtoleft == false)
            {
                mainroad.GetComponent<SpriteRenderer>().sprite = sprite3;
                if (mainroad.transform.rotation.eulerAngles.z == 0)
                {
                    mainroad.GetComponent<SpriteRenderer>().flipY = true;
                    mainroad.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        mainroadfixed = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using Completed;

public class energyspawner : MonoBehaviour
{
    private GameObject[] streets;
    private GameObject[] mainroad;
    public GameObject battery;
    private GameObject street;
    private GameObject[] batteries;
    public float numberofbatteries = 0;
    private float randomnumber;
    private GameObject player;

    private int i = 0;
    public float distancetonearestbattery;
    public float distancetomainroad;
    private float mindistancetoplayer = 20;
    private float mindistance = 20;
    private float maxdistance = 35;

    private void Update()
    {
        if (GameManager.instance.streetsgenerated == true)
        {
            Energyspawner();
            GameManager.instance.energyspawnedin = true;
            enabled = false;
        }
    }

    public void Energyspawner()
    {

        streets = GameObject.FindGameObjectsWithTag("street");
        player = GameObject.FindGameObjectWithTag("player");
        streets = streets.OrderBy(street => Vector2.Distance(street.transform.position, player.transform.position)).ToArray();

        while (i < streets.Length) //make sure one is within 20 of player
        {
            street = streets[i];
            if (Vector2.Distance(street.transform.position, player.transform.position) > 18 && Vector2.Distance(street.transform.position, player.transform.position) < 22)
            {
                Instantiate(battery, street.transform.position, Quaternion.identity);
                break;
            }
            i++;
        }

        i = 0;
        streets = streets.OrderBy(street => Random.value).ToArray();

        while (i < streets.Length) //must spawn 10 away from mainroad and within 35 of each other, but not more than one within 20
        {
            distancetonearestbattery = 0;
            distancetomainroad = 0;
            street = streets[i];
            i++;
            mainroad = GameObject.FindGameObjectsWithTag("mainroad");
            batteries = GameObject.FindGameObjectsWithTag("Energy");
            foreach (GameObject battery in batteries) { if (Vector2.Distance(battery.transform.position, street.transform.position) < distancetonearestbattery || distancetonearestbattery == 0) { distancetonearestbattery = Vector2.Distance(battery.transform.position, street.transform.position); } }
            foreach (GameObject mainroad in mainroad) { if (Vector2.Distance(mainroad.transform.position, street.transform.position) < distancetomainroad || distancetomainroad == 0) { distancetomainroad = Vector2.Distance(mainroad.transform.position, street.transform.position); } }

            if (distancetonearestbattery == 0 && Vector2.Distance(player.transform.position, street.transform.position) > mindistancetoplayer)
            {
                Instantiate(battery, street.transform.position, Quaternion.identity);
                numberofbatteries++;
                continue;
            }

            if (Vector2.Distance(street.transform.position, player.transform.position) > mindistancetoplayer && distancetonearestbattery > mindistance && distancetonearestbattery < maxdistance && distancetomainroad > 10)
            {
                Instantiate(battery, street.transform.position, Quaternion.identity);
                numberofbatteries++;
            }
        }
    }
}

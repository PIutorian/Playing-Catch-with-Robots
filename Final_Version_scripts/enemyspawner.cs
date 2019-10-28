using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using Completed;

public class enemyspawner : MonoBehaviour
{
    private float distancetoplayer;
    private float distancetoenemy;
    private GameObject[] roadnet;
    private GameObject[] smallroadnet;


    public float enemycount;
    public float maxenemies;

    public GameObject[] enemies;
    private GameObject player;
    public GameObject enemy;
    private GameObject road;
    private GameObject exit;

    private int offsetx;
    private int offsety;

    private int i = 0;
    private System.Random random;

    private void Update()
    {
        if (GameManager.instance.streetsgenerated == true)
        {
            Enemyspawner();
            GameManager.instance.enemiesspawnedin = true;
            enabled = false;
        }
    }



    void Enemyspawner()
    {
        exit = GameObject.FindGameObjectWithTag("exit");
        player = GameObject.FindGameObjectWithTag("player");

        roadnet = GameObject.FindGameObjectsWithTag("mainroad");
        roadnet = roadnet.OrderBy(mainroad => Random.value).ToArray();

        smallroadnet = GameObject.FindGameObjectsWithTag("street");
        smallroadnet = smallroadnet.OrderBy(street => Random.value).ToArray();

        random = new System.Random();
        offsetx = 1;
        offsety = 0;
        Instantiate(enemy, new Vector2(exit.transform.position.x + offsetx, exit.transform.position.y), Quaternion.identity);
        Instantiate(enemy, new Vector2(exit.transform.position.x - offsetx, exit.transform.position.y), Quaternion.identity);
        enemycount += 2;


        while (i < roadnet.Length) //spawn in maxenemies-4 on the mainroad
        {
            road = roadnet[i];
            if (Random.value < 0.5)
            {
                road = smallroadnet[i];
            }
            i++;

            if (enemycount >= maxenemies)
            {
                continue;
            }

            distancetoplayer = Vector2.Distance(player.transform.position, road.transform.position);
            if (distancetoplayer <= 15)
            {
                continue;
            }

            distancetoenemy = 0;
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            if (enemies != null)
            {
                foreach (GameObject enemy in enemies) { if (distancetoenemy < Vector2.Distance(enemy.transform.position, road.transform.position) || distancetoenemy == 0) { distancetoenemy = Vector2.Distance(enemy.transform.position, road.transform.position); } }
            }


            if (enemies == null || distancetoenemy >= 25)
            {
                offsetx = random.Next(1, 3);
                offsety = random.Next(1, 3);

                if (Random.value > 0.5 && enemycount < maxenemies - 1)
                {
                    Instantiate(enemy, new Vector2(road.transform.position.x + offsetx, road.transform.position.y + offsety), Quaternion.identity);
                    offsetx = random.Next(-2, 1);
                    offsety = random.Next(-2, 1);
                    Instantiate(enemy, new Vector2(road.transform.position.x + offsetx, road.transform.position.y + offsety), Quaternion.identity);
                    enemycount += 2;
                    continue;
                }
                Instantiate(enemy, new Vector2(road.transform.position.x + offsetx, road.transform.position.y + offsety), Quaternion.identity);
                enemycount++;
                continue;
            }
        }
    }
}

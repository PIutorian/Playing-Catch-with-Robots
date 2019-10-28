using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller4 : MonoBehaviour
{

    private GameObject player;
    private int playerenergy;
    public int depth = 4; //depth of the game Tree
    private GameObject Exit;        //Has to be defined
                                    //	private bool Gameover = false; 
    public GameObject[] enemies;
    private LayerMask wallmask;
    private LayerMask energymask;




    void Start()
    {
        wallmask = LayerMask.GetMask("wall");//Define what Layers the raycast should look for
        energymask = LayerMask.GetMask("Energy");
    }

    void Update()
    {

        if (GameManager.instance.playersturn)
        {       //check if it's the players turn
            return;
        }
        //Get enemy and player objects
        if (enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("enemy");
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
        if (Exit == null)
        {
            Exit = GameObject.FindGameObjectWithTag("exit");
        }

        playerenergy = GameManager.instance.playerenergy;
        //get the current Energy level


        foreach (GameObject enemy in enemies)
        {

            //Max is the best value = same as alpha
            float Max = Mathf.NegativeInfinity;
            float beta = Mathf.Infinity;

            enemy.GetComponent<Collider2D>().enabled = false;

            Vector3 pathp = new Vector3(0, 0);

            Vector3 finalpath = new Vector3(0, -3);
            RaycastHit2D hit = Physics2D.Raycast((enemy.transform.position), finalpath, finalpath.magnitude, wallmask);
            if (hit.collider != null)
            {
                Max = Mathf.NegativeInfinity;
            }
            else
            {
                Max = minimax(enemy, finalpath, pathp, depth, Max, beta, false, playerenergy);
            }

            if (!float.IsPositiveInfinity(Max))
            {

                Vector3 newpath = new Vector3(0, 3);
                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                float utility2;
                if (hit.collider != null)
                {
                    utility2 = Mathf.NegativeInfinity;
                }
                else
                {
                    utility2 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                }
                if (utility2 > Max)
                {
                    Max = utility2;
                    finalpath = newpath;
                }
                if (!float.IsPositiveInfinity(Max))
                {


                    newpath = new Vector3(-3, 0);
                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                    float utility3;
                    if (hit.collider != null)
                    {
                        utility3 = Mathf.NegativeInfinity;
                    }
                    else
                    {
                        utility3 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                    }

                    if (utility3 > Max)
                    {
                        Max = utility3;
                        finalpath = newpath;
                    }
                    if (!float.IsPositiveInfinity(Max))
                    {

                        newpath = new Vector3(3, 0);
                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                        float utility4;
                        if (hit.collider != null)
                        {
                            utility4 = Mathf.NegativeInfinity;
                        }
                        else
                        {
                            utility4 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                        }

                        if (utility4 > Max)
                        {
                            Max = utility4;
                            finalpath = newpath;
                        }
                        if (!float.IsPositiveInfinity(Max))
                        {

                            newpath = new Vector3(2, 1);
                            hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                            float utility5;
                            if (hit.collider != null)
                            {
                                utility5 = Mathf.NegativeInfinity;
                            }
                            else
                            {
                                utility5 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                            }

                            if (utility5 > Max)
                            {
                                Max = utility5;
                                finalpath = newpath;
                            }
                            if (!float.IsPositiveInfinity(Max))
                            {

                                newpath = new Vector3(1, 1);
                                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                float utility6;
                                if (hit.collider != null)
                                {
                                    utility6 = Mathf.NegativeInfinity;
                                }
                                else
                                {
                                    utility6 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                }

                                if (utility6 > Max)
                                {
                                    Max = utility6;
                                    finalpath = newpath;
                                }
                                if (!float.IsPositiveInfinity(Max))
                                {

                                    newpath = new Vector3(0, 2);
                                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                    float utility7;
                                    if (hit.collider != null)
                                    {
                                        utility7 = Mathf.NegativeInfinity;
                                    }
                                    else
                                    {
                                        utility7 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                    }

                                    if (utility7 > Max)
                                    {
                                        Max = utility7;
                                        finalpath = newpath;
                                    }
                                    if (!float.IsPositiveInfinity(Max))
                                    {

                                        newpath = new Vector3(2, 0);
                                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                        float utility8;
                                        if (hit.collider != null)
                                        {
                                            utility8 = Mathf.NegativeInfinity;
                                        }
                                        else
                                        {
                                            utility8 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                        }

                                        if (utility8 > Max)
                                        {
                                            Max = utility8;
                                            finalpath = newpath;
                                        }
                                        if (!float.IsPositiveInfinity(Max))
                                        {

                                            newpath = new Vector3(1, 0);
                                            hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                            float utility9;
                                            if (hit.collider != null)
                                            {
                                                utility9 = Mathf.NegativeInfinity;
                                            }
                                            else
                                            {
                                                utility9 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                            }

                                            if (utility9 > Max)
                                            {
                                                Max = utility9;
                                                finalpath = newpath;
                                            }
                                            if (!float.IsPositiveInfinity(Max))
                                            {

                                                newpath = new Vector3(2, -1);
                                                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                float utility10;
                                                if (hit.collider != null)
                                                {
                                                    utility10 = Mathf.NegativeInfinity;
                                                }
                                                else
                                                {
                                                    utility10 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                }

                                                if (utility10 > Max)
                                                {
                                                    Max = utility10;
                                                    finalpath = newpath;
                                                }
                                                if (!float.IsPositiveInfinity(Max))
                                                {

                                                    newpath = new Vector3(1, -2);
                                                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                    float utility11;
                                                    if (hit.collider != null)
                                                    {
                                                        utility11 = Mathf.NegativeInfinity;
                                                    }
                                                    else
                                                    {
                                                        utility11 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                    }

                                                    if (utility11 > Max)
                                                    {
                                                        Max = utility11;
                                                        finalpath = newpath;
                                                    }
                                                    if (!float.IsPositiveInfinity(Max))
                                                    {

                                                        newpath = new Vector3(1, -1);
                                                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                        float utility12;
                                                        if (hit.collider != null)
                                                        {
                                                            utility12 = Mathf.NegativeInfinity;
                                                        }
                                                        else
                                                        {
                                                            utility12 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                        }

                                                        if (utility12 > Max)
                                                        {
                                                            Max = utility12;
                                                            finalpath = newpath;
                                                        }
                                                        if (!float.IsPositiveInfinity(Max))
                                                        {

                                                            newpath = new Vector3(0, 1);
                                                            hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                            float utility13;
                                                            if (hit.collider != null)
                                                            {
                                                                utility13 = Mathf.NegativeInfinity;
                                                            }
                                                            else
                                                            {
                                                                utility13 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                            }

                                                            if (utility13 > Max)
                                                            {
                                                                Max = utility13;
                                                                finalpath = newpath;
                                                            }
                                                            if (!float.IsPositiveInfinity(Max))
                                                            {

                                                                newpath = new Vector3(0, -2);
                                                                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                float utility14;
                                                                if (hit.collider != null)
                                                                {
                                                                    utility14 = Mathf.NegativeInfinity;
                                                                }
                                                                else
                                                                {
                                                                    utility14 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                }

                                                                if (utility14 > Max)
                                                                {
                                                                    Max = utility14;
                                                                    finalpath = newpath;
                                                                }
                                                                if (!float.IsPositiveInfinity(Max))
                                                                {

                                                                    newpath = new Vector3(0, -1);
                                                                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                    float utility15;
                                                                    if (hit.collider != null)
                                                                    {
                                                                        utility15 = Mathf.NegativeInfinity;
                                                                    }
                                                                    else
                                                                    {
                                                                        utility15 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                    }

                                                                    if (utility15 > Max)
                                                                    {
                                                                        Max = utility15;
                                                                        finalpath = newpath;
                                                                    }
                                                                    if (!float.IsPositiveInfinity(Max))
                                                                    {

                                                                        newpath = new Vector3(-1, -2);
                                                                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                        float utility16;
                                                                        if (hit.collider != null)
                                                                        {
                                                                            utility16 = Mathf.NegativeInfinity;
                                                                        }
                                                                        else
                                                                        {
                                                                            utility16 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                        }

                                                                        if (utility16 > Max)
                                                                        {
                                                                            Max = utility16;
                                                                            finalpath = newpath;
                                                                        }
                                                                        if (!float.IsPositiveInfinity(Max))
                                                                        {

                                                                            newpath = new Vector3(-2, -1);
                                                                            hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                            float utility17;
                                                                            if (hit.collider != null)
                                                                            {
                                                                                utility17 = Mathf.NegativeInfinity;
                                                                            }
                                                                            else
                                                                            {
                                                                                utility17 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                            }

                                                                            if (utility17 > Max)
                                                                            {
                                                                                Max = utility17;
                                                                                finalpath = newpath;
                                                                            }
                                                                            if (!float.IsPositiveInfinity(Max))
                                                                            {

                                                                                newpath = new Vector3(-1, -1);
                                                                                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                float utility18;
                                                                                if (hit.collider != null)
                                                                                {
                                                                                    utility18 = Mathf.NegativeInfinity;
                                                                                }
                                                                                else
                                                                                {
                                                                                    utility18 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                }

                                                                                if (utility18 > Max)
                                                                                {
                                                                                    Max = utility18;
                                                                                    finalpath = newpath;
                                                                                }
                                                                                if (!float.IsPositiveInfinity(Max))
                                                                                {

                                                                                    newpath = new Vector3(1, 2);
                                                                                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                    float utility19;
                                                                                    if (hit.collider != null)
                                                                                    {
                                                                                        utility19 = Mathf.NegativeInfinity;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        utility19 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                    }

                                                                                    if (utility19 > Max)
                                                                                    {
                                                                                        Max = utility19;
                                                                                        finalpath = newpath;
                                                                                    }
                                                                                    if (!float.IsPositiveInfinity(Max))
                                                                                    {

                                                                                        newpath = new Vector3(-2, 0);
                                                                                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                        float utility20;
                                                                                        if (hit.collider != null)
                                                                                        {
                                                                                            utility20 = Mathf.NegativeInfinity;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            utility20 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                        }

                                                                                        if (utility20 > Max)
                                                                                        {
                                                                                            Max = utility20;
                                                                                            finalpath = newpath;
                                                                                        }
                                                                                        if (!float.IsPositiveInfinity(Max))
                                                                                        {

                                                                                            newpath = new Vector3(-1, 0);
                                                                                            hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                            float utility21;
                                                                                            if (hit.collider != null)
                                                                                            {
                                                                                                utility21 = Mathf.NegativeInfinity;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                utility21 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                            }

                                                                                            if (utility21 > Max)
                                                                                            {
                                                                                                Max = utility21;
                                                                                                finalpath = newpath;
                                                                                            }
                                                                                            if (!float.IsPositiveInfinity(Max))
                                                                                            {

                                                                                                newpath = new Vector3(-2, 1);
                                                                                                hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                                float utility22;
                                                                                                if (hit.collider != null)
                                                                                                {
                                                                                                    utility22 = Mathf.NegativeInfinity;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    utility22 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                                }

                                                                                                if (utility22 > Max)
                                                                                                {
                                                                                                    Max = utility22;
                                                                                                    finalpath = newpath;
                                                                                                }
                                                                                                if (!float.IsPositiveInfinity(Max))
                                                                                                {

                                                                                                    newpath = new Vector3(-1, 2);
                                                                                                    hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                                    float utility23;
                                                                                                    if (hit.collider != null)
                                                                                                    {
                                                                                                        utility23 = Mathf.NegativeInfinity;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        utility23 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                                    }

                                                                                                    if (utility23 > Max)
                                                                                                    {
                                                                                                        Max = utility23;
                                                                                                        finalpath = newpath;
                                                                                                    }
                                                                                                    if (!float.IsPositiveInfinity(Max))
                                                                                                    {

                                                                                                        newpath = new Vector3(-1, 1);
                                                                                                        hit = Physics2D.Raycast((enemy.transform.position), newpath, newpath.magnitude, wallmask);
                                                                                                        float utility24;
                                                                                                        if (hit.collider != null)
                                                                                                        {
                                                                                                            utility24 = Mathf.NegativeInfinity;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            utility24 = minimax(enemy, newpath, pathp, depth, Max, beta, false, playerenergy);
                                                                                                        }

                                                                                                        if (utility24 > Max)
                                                                                                        {
                                                                                                            Max = utility24;
                                                                                                            finalpath = newpath;
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Vector2 p = finalpath;
            Vector3 p1 = p;
            if (float.IsNegativeInfinity(Max))
                Debug.Log("Player Victory");
            else
            {
                enemy.transform.position += p1;
                Debug.Log(Max);
            }
            enemy.GetComponent<Collider2D>().enabled = true;
        }


        GameManager.instance.playersturn = true;
    }




    /*  enemy = current enemy to be moved
	 * pathe = vector indicating the new theoretical enemy position
	 * pathp = vector indicating the new theoretical player position
	 * depth = depth of the Gametree
	 * isMax = defines if it's the max(enemy) or mini(player) turn
	 * plenergy = Energy the player has theoretically left
	 */

    float minimax(GameObject enemy, Vector3 pathe, Vector3 pathp, int depth, float alpha, float beta, bool isMax, int plenergy)
    {
        //Check for end of search tree or gameover condition
        float result = terminaltest(enemy, pathe, pathp, depth, isMax, plenergy);
        if (result != 9991019)
            return result;

        if (isMax)
        {
            //instatntiate the 24 new possible positions (path1-24, wich create 24 new positions on theyr own)

            Vector3 newpath = (pathe + new Vector3(0, 3));
            RaycastHit2D hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility1;
            //first check for wall collision
            if (hit.collider != null)
            {
                utility1 = Mathf.NegativeInfinity;
            }
            else
            {
                //if there is no wall proceed with evaluation of the new position
                utility1 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility1 > alpha)
            {
                alpha = utility1;
                if (beta <= alpha)
                    return utility1;
            }


            newpath = (pathe + new Vector3(3, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility2;
            if (hit.collider != null)
            {
                utility2 = Mathf.NegativeInfinity;
            }
            else
            {
                utility2 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility2 > alpha)
            {
                alpha = utility2;
                if (beta <= alpha)
                    return utility2;
            }

            newpath = (pathe + new Vector3(0, -3));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility3;
            if (hit.collider != null)
            {
                utility3 = Mathf.NegativeInfinity;
            }
            else
            {
                utility3 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility3 > alpha)
            {
                alpha = utility3;
                if (beta <= alpha)
                    return utility3;
            }

            newpath = (pathe + new Vector3(-3, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility4;
            if (hit.collider != null)
            {
                utility4 = Mathf.NegativeInfinity;
            }
            else
            {
                utility4 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility4 > alpha)
            {
                alpha = utility4;
                if (beta <= alpha)
                    return utility4;
            }

            newpath = (pathe + new Vector3(2, 1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility5;
            if (hit.collider != null)
            {
                utility5 = Mathf.NegativeInfinity;
            }
            else
            {
                utility5 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility5 > alpha)
            {
                alpha = utility5;
                if (beta <= alpha)
                    return utility5;
            }

            newpath = (pathe + new Vector3(1, 1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility6;
            if (hit.collider != null)
            {
                utility6 = Mathf.NegativeInfinity;
            }
            else
            {
                utility6 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility6 > alpha)
            {
                alpha = utility6;
                if (beta <= alpha)
                    return utility6;
            }

            newpath = (pathe + new Vector3(0, 2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility7;
            if (hit.collider != null)
            {
                utility7 = Mathf.NegativeInfinity;
            }
            else
            {
                utility7 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility7 > alpha)
            {
                alpha = utility7;
                if (beta <= alpha)
                    return utility7;
            }


            newpath = (pathe + new Vector3(2, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility8;
            if (hit.collider != null)
            {
                utility8 = Mathf.NegativeInfinity;
            }
            else
            {
                utility8 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility8 > alpha)
            {
                alpha = utility8;
                if (beta <= alpha)
                    return utility8;
            }

            newpath = (pathe + new Vector3(1, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility9;
            if (hit.collider != null)
            {
                utility9 = Mathf.NegativeInfinity;
            }
            else
            {
                utility9 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility9 > alpha)
            {
                alpha = utility9;
                if (beta <= alpha)
                    return utility9;
            }

            newpath = (pathe + new Vector3(2, -1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility10;
            if (hit.collider != null)
            {
                utility10 = Mathf.NegativeInfinity;
            }
            else
            {
                utility10 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility10 > alpha)
            {
                alpha = utility10;
                if (beta <= alpha)
                    return utility10;
            }

            newpath = (pathe + new Vector3(1, -1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility11;
            if (hit.collider != null)
            {
                utility11 = Mathf.NegativeInfinity;
            }
            else
            {
                utility11 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility11 > alpha)
            {
                alpha = utility11;
                if (beta <= alpha)
                    return utility11;
            }

            newpath = (pathe + new Vector3(1, -2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility12;
            if (hit.collider != null)
            {
                utility12 = Mathf.NegativeInfinity;
            }
            else
            {
                utility12 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility12 > alpha)
            {
                alpha = utility12;
                if (beta <= alpha)
                    return utility12;
            }

            newpath = (pathe + new Vector3(0, 1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility13;
            if (hit.collider != null)
            {
                utility13 = Mathf.NegativeInfinity;
            }
            else
            {
                utility13 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility13 > alpha)
            {
                alpha = utility13;
                if (beta <= alpha)
                    return utility13;
            }

            newpath = (pathe + new Vector3(0, -2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility14;
            if (hit.collider != null)
            {
                utility14 = Mathf.NegativeInfinity;
            }
            else
            {
                utility14 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility14 > alpha)
            {
                alpha = utility14;
                if (beta <= alpha)
                    return utility14;
            }

            newpath = (pathe + new Vector3(0, -1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility15;
            if (hit.collider != null)
            {
                utility15 = Mathf.NegativeInfinity;
            }
            else
            {
                utility15 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility15 > alpha)
            {
                alpha = utility15;
                if (beta <= alpha)
                    return utility15;
            }

            newpath = (pathe + new Vector3(-1, -2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility16;
            if (hit.collider != null)
            {
                utility16 = Mathf.NegativeInfinity;
            }
            else
            {
                utility16 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility16 > alpha)
            {
                alpha = utility16;
                if (beta <= alpha)
                    return utility16;
            }

            newpath = (pathe + new Vector3(-1, -1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility17;
            if (hit.collider != null)
            {
                utility17 = Mathf.NegativeInfinity;
            }
            else
            {
                utility17 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility17 > alpha)
            {
                alpha = utility17;
                if (beta <= alpha)
                    return utility17;
            }

            newpath = (pathe + new Vector3(-2, -1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility18;
            if (hit.collider != null)
            {
                utility18 = Mathf.NegativeInfinity;
            }
            else
            {
                utility18 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility18 > alpha)
            {
                alpha = utility18;
                if (beta <= alpha)
                    return utility18;
            }

            newpath = (pathe + new Vector3(1, 2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility19;
            if (hit.collider != null)
            {
                utility19 = Mathf.NegativeInfinity;
            }
            else
            {
                utility19 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility19 > alpha)
            {
                alpha = utility19;
                if (beta <= alpha)
                    return utility19;
            }

            newpath = (pathe + new Vector3(-2, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility20;
            if (hit.collider != null)
            {
                utility20 = Mathf.NegativeInfinity;
            }
            else
            {
                utility20 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility20 > alpha)
            {
                alpha = utility20;
                if (beta <= alpha)
                    return utility20;
            }

            newpath = (pathe + new Vector3(-1, 0));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility21;
            if (hit.collider != null)
            {
                utility21 = Mathf.NegativeInfinity;
            }
            else
            {
                utility21 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility21 > alpha)
            {
                alpha = utility21;
                if (beta <= alpha)
                    return utility21;
            }

            newpath = (pathe + new Vector3(-2, 1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility22;
            if (hit.collider != null)
            {
                utility22 = Mathf.NegativeInfinity;
            }
            else
            {
                utility22 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility22 > alpha)
            {
                alpha = utility22;
                if (beta <= alpha)
                    return utility22;
            }

            newpath = (pathe + new Vector3(-1, 2));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility23;
            if (hit.collider != null)
            {
                utility23 = Mathf.NegativeInfinity;
            }
            else
            {
                utility23 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, false, plenergy);
            }
            if (utility23 > alpha)
            {
                alpha = utility23;
                if (beta <= alpha)
                    return utility23;
            }

            newpath = (pathe + new Vector3(-1, 1));
            hit = Physics2D.Linecast(enemy.transform.position + pathe, enemy.transform.position + newpath, wallmask);
            float utility24;
            if (hit.collider != null)
            {
                utility24 = Mathf.NegativeInfinity;
            }
            else
            {
                utility24 = minimax(enemy, newpath, pathp, depth - 1, alpha, beta, true, plenergy - 2);
            }



            float maxutlity = Mathf.Max(utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12, utility13, utility14, utility15, utility16, utility17, utility18, utility19, utility20, utility21, utility22, utility23, utility24);
            return maxutlity;

        }
        else
        {

            Vector3 newpath = (pathp + new Vector3(0, 3));
            RaycastHit2D hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility1;
            if (hit.collider != null)
            {
                utility1 = Mathf.Infinity;
            }
            else
            {
                utility1 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility1 < beta)
            {
                beta = utility1;
                if (beta <= alpha)
                    return utility1;
            }

            newpath = (pathp + new Vector3(3, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility2;
            if (hit.collider != null)
            {
                utility2 = Mathf.Infinity;
            }
            else
            {
                utility2 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility2 < beta)
            {
                beta = utility2;
                if (beta <= alpha)
                    return utility2;
            }


            newpath = (pathp + new Vector3(0, -3));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility3;
            if (hit.collider != null)
            {
                utility3 = Mathf.Infinity;
            }
            else
            {
                utility3 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility3 < beta)
            {
                beta = utility3;
                if (beta <= alpha)
                    return utility3;
            }

            newpath = (pathp + new Vector3(-3, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility4;
            if (hit.collider != null)
            {
                utility4 = Mathf.Infinity;
            }
            else
            {
                utility4 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility4 < beta)
            {
                beta = utility4;
                if (beta <= alpha)
                    return utility4;
            }

            newpath = (pathp + new Vector3(2, 1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility5;
            if (hit.collider != null)
            {
                utility5 = Mathf.Infinity;
            }
            else
            {
                utility5 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility5 < beta)
            {
                beta = utility5;
                if (beta <= alpha)
                    return utility5;
            }

            newpath = (pathp + new Vector3(1, 1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility6;
            if (hit.collider != null)
            {
                utility6 = Mathf.Infinity;
            }
            else
            {
                utility6 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility6 < beta)
            {
                beta = utility6;
                if (beta <= alpha)
                    return utility6;
            }

            newpath = (pathp + new Vector3(0, 2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility7;
            if (hit.collider != null)
            {
                utility7 = Mathf.Infinity;
            }
            else
            {
                utility7 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility7 < beta)
            {
                beta = utility7;
                if (beta <= alpha)
                    return utility7;
            }

            newpath = (pathp + new Vector3(2, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility8;
            if (hit.collider != null)
            {
                utility8 = Mathf.Infinity;
            }
            else
            {
                utility8 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility8 < beta)
            {
                beta = utility8;
                if (beta <= alpha)
                    return utility8;
            }

            newpath = (pathp + new Vector3(1, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility9;
            if (hit.collider != null)
            {
                utility9 = Mathf.Infinity;
            }
            else
            {
                utility9 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 1);
            }
            if (utility9 < beta)
            {
                beta = utility9;
                if (beta <= alpha)
                    return utility9;
            }

            newpath = (pathp + new Vector3(2, -1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility10;
            if (hit.collider != null)
            {
                utility10 = Mathf.Infinity;
            }
            else
            {
                utility10 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility10 < beta)
            {
                beta = utility10;
                if (beta <= alpha)
                    return utility10;
            }

            newpath = (pathp + new Vector3(1, -1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility11;
            if (hit.collider != null)
            {
                utility11 = Mathf.Infinity;
            }
            else
            {
                utility11 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility11 < beta)
            {
                beta = utility11;
                if (beta <= alpha)
                    return utility11;
            }

            newpath = (pathp + new Vector3(1, -2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility12;
            if (hit.collider != null)
            {
                utility12 = Mathf.Infinity;
            }
            else
            {
                utility12 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility12 < beta)
            {
                beta = utility12;
                if (beta <= alpha)
                    return utility12;
            }

            newpath = (pathp + new Vector3(0, 1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility13;
            if (hit.collider != null)
            {
                utility13 = Mathf.Infinity;
            }
            else
            {
                utility13 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 1);
            }
            if (utility13 < beta)
            {
                beta = utility13;
                if (beta <= alpha)
                    return utility13;
            }

            newpath = (pathp + new Vector3(0, -2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility14;
            if (hit.collider != null)
            {
                utility14 = Mathf.Infinity;
            }
            else
            {
                utility14 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility14 < beta)
            {
                beta = utility14;
                if (beta <= alpha)
                    return utility14;
            }

            newpath = (pathp + new Vector3(0, -1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility15;
            if (hit.collider != null)
            {
                utility15 = Mathf.Infinity;
            }
            else
            {
                utility15 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 1);
            }
            if (utility15 < beta)
            {
                beta = utility15;
                if (beta <= alpha)
                    return utility15;
            }

            newpath = (pathp + new Vector3(-1, -2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility16;
            if (hit.collider != null)
            {
                utility16 = Mathf.Infinity;
            }
            else
            {
                utility16 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility16 < beta)
            {
                beta = utility16;
                if (beta <= alpha)
                    return utility16;
            }

            newpath = (pathp + new Vector3(-1, -1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility17;
            if (hit.collider != null)
            {
                utility17 = Mathf.Infinity;
            }
            else
            {
                utility17 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility17 < beta)
            {
                beta = utility17;
                if (beta <= alpha)
                    return utility17;
            }

            newpath = (pathp + new Vector3(-2, -1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility18;
            if (hit.collider != null)
            {
                utility18 = Mathf.Infinity;
            }
            else
            {
                utility18 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility18 < beta)
            {
                beta = utility18;
                if (beta <= alpha)
                    return utility18;
            }

            newpath = (pathp + new Vector3(1, 2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility19;
            if (hit.collider != null)
            {
                utility19 = Mathf.Infinity;
            }
            else
            {
                utility19 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility19 < beta)
            {
                beta = utility19;
                if (beta <= alpha)
                    return utility19;
            }

            newpath = (pathp + new Vector3(-2, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility20;
            if (hit.collider != null)
            {
                utility20 = Mathf.Infinity;
            }
            else
            {
                utility20 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }
            if (utility20 < beta)
            {
                beta = utility20;
                if (beta <= alpha)
                    return utility20;
            }

            newpath = (pathp + new Vector3(-1, 0));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility21;
            if (hit.collider != null)
            {
                utility21 = Mathf.Infinity;
            }
            else
            {
                utility21 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 1);
            }
            if (utility21 < beta)
            {
                beta = utility21;
                if (beta <= alpha)
                    return utility21;
            }

            newpath = (pathp + new Vector3(-2, 1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility22;
            if (hit.collider != null)
            {
                utility22 = Mathf.Infinity;
            }
            else
            {
                utility22 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
                if (utility22 < beta)
                    return utility22;
                else
                    beta = utility22;
            }

            newpath = (pathp + new Vector3(-1, 2));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility23;
            if (hit.collider != null)
            {
                utility23 = Mathf.Infinity;
            }
            else
            {
                utility23 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 3);
            }
            if (utility23 < beta)
            {
                beta = utility23;
                if (beta <= alpha)
                    return utility23;
            }

            newpath = (pathp + new Vector3(-1, 1));
            hit = Physics2D.Linecast(player.transform.position + pathp, player.transform.position + newpath, wallmask);
            float utility24;
            if (hit.collider != null)
            {
                utility24 = Mathf.Infinity;
            }
            else
            {
                utility24 = minimax(enemy, pathe, newpath, depth - 1, alpha, beta, true, plenergy - 2);
            }


            float minutility = Mathf.Min(utility1, utility2, utility3, utility4, utility5, utility6, utility7, utility8, utility9, utility10, utility11, utility12, utility13, utility14, utility15, utility16, utility17, utility18, utility19, utility20, utility21, utility22, utility23, utility24);
            return minutility;
        }
    }

    float terminaltest(GameObject enemy, Vector3 pathe, Vector3 pathp, int depth, bool isMax, float plenergy)
    {

        float utility = 0;
        //terminal conditions
        if (Vector3.Distance((enemy.transform.position + pathe), (player.transform.position + pathp)) < 0.6)
            return Mathf.Infinity;
        else if (Vector3.Distance((player.transform.position + pathp), Exit.transform.position) < 0.6)
            return Mathf.NegativeInfinity;
        else if (plenergy <= 0)
            return Mathf.Infinity;
        else if (depth == 0)
        {

            float dist1 = Vector3.Distance((enemy.transform.position + pathe), (player.transform.position + pathp));
            float dist2 = Vector3.Distance((player.transform.position + pathp), Exit.transform.position);
            // type of road still missing
            utility += ((-dist1) + (2 * dist2) - plenergy);
            return utility;
        }
        else
            return 9991019;
    }
}
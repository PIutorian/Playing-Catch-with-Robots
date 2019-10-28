using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        public int heightandwidth;

        public GameObject grass;
        public Texture2D grasstexture;
        private double nextspawny;
        private float ymultiplier;

        public void generatetextures()
        {

            nextspawny = GameObject.FindGameObjectWithTag("roadnet").GetComponent<roadnet>().nextspawny; ;
            ymultiplier = (float)0.2 * (float)nextspawny - (float)1; // y=mx+b --> turns it into 29,30,31

            grasstexture = new Texture2D(120, 120);

            for (int y = 0; y < grasstexture.height; y++)
            {
                for (int x = 0; x < grasstexture.height; x++)
                {
                    Color color = new Color(0, (float)0.25 + Random.value / 4, 0, 1F);
                    grasstexture.SetPixel(x, y, color);
                }
            }
            grasstexture.Apply();
            grasstexture.filterMode = FilterMode.Point;
            GameObject Grass = Instantiate(grass, new Vector2(0, 0), Quaternion.identity);

            Grass.GetComponent<SpriteRenderer>().sprite = Sprite.Create(grasstexture, new Rect(0, 0, 100, 100), transform.position);
            Grass.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
            Vector2 vec = Grass.GetComponent<SpriteRenderer>().size;
            Grass.GetComponent<SpriteRenderer>().size += new Vector2(vec.x * 40, vec.y * ymultiplier);
            Grass.transform.position = new Vector2(-102.5F, 2.5F);

            GameManager.instance.backgroundgenerated = true;
        }
    }
}



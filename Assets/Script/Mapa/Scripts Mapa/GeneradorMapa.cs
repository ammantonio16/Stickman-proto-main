using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GeneradorMapa : MonoBehaviour
{
    public Tile tile;
    
    public Tilemap tilemap;

    public RuleTile regla;

    public int mapWidth;
    public int mapHeight;
    
    private int [,] mapData;

    public CelularData cell;
    public PerlinData perlin;
    // Start is called before the first frame update
    void Start()
    {
        this.mapData = this.perlin.GenerateData(this.mapWidth, this.mapHeight);
        this.GenerateTiles(); 
    }

    void GenerateTiles()
    {
        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                if(this.mapData[i,j] == 1)
                {
                    //this.tilemap.SetTile(new Vector3Int(i,j,0),this.tile);
                    this.tilemap.SetTile(new Vector3Int(i,j,0),this.regla);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

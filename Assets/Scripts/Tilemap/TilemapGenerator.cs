using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour // this is just for testing, or simple levels, probably
{
    [SerializeField] Vector2 tilemapSize = new Vector2(10, 6);
    [SerializeField] TileBase groundTile;
    [SerializeField] TileBase tileBorderTile;
    
    public void GenerateTilemap()
    {
        Tilemap groundTilemap = GameObject.FindGameObjectWithTag("GroundTilemap").GetComponent<Tilemap>();
        Tilemap bordersTilemap = GameObject.FindGameObjectWithTag("TileBordersTilemap").GetComponent<Tilemap>();

        for (int x = 0; x < tilemapSize.x; x++)
        {
            for (int y = 0; y < tilemapSize.y; y++)
            {
                groundTilemap.SetTile(new Vector3Int(x, y), groundTile);
                bordersTilemap.SetTile(new Vector3Int(x, y), tileBorderTile);
            }
        }
    }
}

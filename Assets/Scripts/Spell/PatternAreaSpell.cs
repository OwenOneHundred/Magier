using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Spell/Pattern Area Spell")]
public class PatternAreaSpell : Spell
{
    [SerializeField] GameObject shapePrefab;
    [SerializeField] float diceRollImpactMultiplier = 1;
    GameObject shapeObject;

    public override void OnHovered(Vector3Int position, int diceRoll)
    {
        
    }
    public override void OnUnhovered(Vector3Int position, int diceRoll)
    {
        DestroyShapeObject();
    }

    void CreateShapeObject(Vector3Int position, int diceRoll)
    {
        float scaleFactor = 1 + (((diceRoll - 6)/10) * diceRollImpactMultiplier);
        shapeObject = Instantiate(shapePrefab,
            TilemapManager.tilemapManager.groundTilemap.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0),
            Quaternion.identity);

        shapeObject.transform.localScale *= scaleFactor;
    }

    void DestroyShapeObject()
    {
        if (shapeObject != null)
        {
            Destroy(shapeObject);
        }
    }

    public override List<Vector3Int> GetSelectedTiles(Vector3Int position, int diceRoll)
    {
        if (shapeObject == null)
        {
            CreateShapeObject(position, diceRoll);
        }

        Tilemap groundTilemap = TilemapManager.tilemapManager.groundTilemap;

        shapeObject.transform.position = groundTilemap.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

        Collider2D shapeCollider = shapeObject.GetComponent<Collider2D>();
        Bounds bounds = shapeCollider.bounds;

        int countForTest = 0;
        List<Vector3Int> hitTiles = new List<Vector3Int>();
        for (float x = Mathf.Floor(bounds.min.x) + 0.5f; x < bounds.max.x; x++)
        {
            for (float y = Mathf.Floor(bounds.min.y) + 0.5f; y < bounds.max.y; y++)
            {
                countForTest += 1;
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, y), Vector2.zero, 1, 1<<6);
                if (hit.collider != null)
                {
                    hitTiles.Add(groundTilemap.WorldToCell(new Vector3Int(Mathf.FloorToInt(x), Mathf.FloorToInt(y), 0)));
                }
            }
        }
        Debug.Log("out of " + countForTest + ", " + hitTiles.Count + " had their centers covered by the collider.");
        return hitTiles;
    }

    public override void OnCast(Vector3Int position, int diceRoll, TileOwner caster)
    {
        foreach (Vector3Int tilePos in GetSelectedTiles(position, diceRoll))
        {
            TilemapManager.tilemapManager.SetTileOwner(tilePos, caster);
        }
        Destroy(shapeObject, 2);
        shapeObject = null;
    }
}

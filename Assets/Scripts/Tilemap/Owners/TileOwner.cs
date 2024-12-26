using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileOwner
{
    public string name;
    public bool isPlayer = false;
    public TileBase glowTilemapTile;
    public TileBase borderTilemapTile;
    public GameObject prefabToSpawnOnFirstCapturedTile;
    public virtual void OnOwnTile(Vector3Int position)
    {
        return;
    }
}

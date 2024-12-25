using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileData : ScriptableObject
{
    public string tileName;
    public TileBase tile;
    public Tilemap tilemap;
    public abstract void OnClicked(Vector3Int position);
    public abstract void OnHovered(Vector3Int position);
    public abstract void OnUnhovered(Vector3Int position);
    public abstract void OnPlayerClaimed(Vector3Int position);
    public abstract void OnEnemyClaimed(Vector3Int position);
    public abstract void OnBecomeUnclaimed(Vector3Int position);

    public void Awake()
    {
        if (TilemapManager.tilemapManager != null)
        {
            tilemap = TilemapManager.tilemapManager.groundTilemap;
        }
    }

    /// <summary>
    /// Set the colour of a tile.
    /// </summary>
    /// <param name="colour">The desired colour.</param>
    /// <param name="position">The position of the tile.</param>
    /// <param name="tilemap">The tilemap the tile belongs to.</param>
    public void SetTileColour(Color colour, Vector3Int position)
    {
        // Flag the tile, inidicating that it can change colour.
        // By default it's set to "Lock Colour".
        tilemap.SetTileFlags(position, TileFlags.None);

        // Set the colour.
        tilemap.SetColor(position, colour);
    }
}

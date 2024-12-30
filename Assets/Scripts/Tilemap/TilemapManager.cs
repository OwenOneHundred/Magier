using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Class that oversees all general tilemap behaviors and information.
/// </summary>
public class TilemapManager : MonoBehaviour
{
    [System.NonSerialized] public TileBase currentHoveredTile;
    [System.NonSerialized] public TileData currentHoveredTiledata;
    [System.NonSerialized] public Vector3Int currentHoveredTilePosition;

    Dictionary<Vector3Int, TileOwner> ownedPositions = new();

    public Tilemap groundTilemap;
    public Tilemap tileBordersTilemap;
    public Tilemap glowTilemap;

    public List<TileData> allTileData;

    public static TilemapManager tilemapManager; // public static reference to this to be used from everywhere

    

    [SerializeField] TileOwner playerTileOwner;
    [SerializeField] TileOwner enemyTileOwner;
    [SerializeField] TileOwner noOwner;

    void Awake()
    {
        if (tilemapManager == null || tilemapManager == this) // if tilemap manager already exists, destroy this one
        {
            tilemapManager = this;
            CreateFreshTiledataSOs();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GetComponent<TilemapGenerator>().GenerateTilemap(); // for testing, make a little tilemap
    }

    void Update()
    {
        SetMousePositionInfo();
    }

    void SetMousePositionInfo() // update data based on where mouse is now
    {
        Vector3Int newTilePosition = groundTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (currentHoveredTilePosition == newTilePosition) { return; } // do nothing if selected tile is same
        if (currentHoveredTiledata != null)
        {
            currentHoveredTiledata.OnUnhovered(currentHoveredTilePosition); // unhover old position
        }
        
        currentHoveredTilePosition = newTilePosition; // set position to new position
        currentHoveredTile = groundTilemap.GetTile(currentHoveredTilePosition); // set tile to new tile
        currentHoveredTiledata = GetTileData(currentHoveredTile); // set tile data to new tile data

        if (currentHoveredTiledata != null)
        {
            HoverTile(currentHoveredTilePosition); // hover new position
        }
    }

    public void HoverTile(Vector3Int tilePosition)
    {
        TileData hoveredTile = GetTileData(groundTilemap.GetTile(tilePosition));
        if (hoveredTile == null) { return; }
        hoveredTile.OnHovered(tilePosition);
    }

    public void UnhoverTile(Vector3Int tilePosition)
    {
        TileData hoveredTile = GetTileData(groundTilemap.GetTile(tilePosition));
        if (hoveredTile == null) { return; }
        hoveredTile.OnUnhovered(tilePosition);
    }
    
    /// <summary>
    /// Sets owner of tile, calls owner.OnOwnTile(position), and adds to dict of owned tiles. 
    /// </summary>
    /// <param name="position">Grid position of tile</param>
    /// <param name="playerEnemy">True if player, false if enemy</param>
    /// <param name="overwriteOwner">If tile already has an owner, set owner anyway</param>
    public void SetTileOwner(Vector3Int position, TileOwnerIndex tileOwnerIndex, bool overwriteOwner = false)
    {
        bool tileHasOwner = ownedPositions.ContainsKey(position); // check if owned already
        if (tileHasOwner && !overwriteOwner) { return; } // if already owned and !overwrite, return

        TileOwner newTileOwner = TileOwnerIndexToOwner(tileOwnerIndex);
        SetTileOwnerInOwnedPositionsList(position, tileOwnerIndex); // add or remove from list of owned tiles

        newTileOwner.OnOwnTile(position);
        tileBordersTilemap.SetTile(position, newTileOwner.borderTilemapTile);
        glowTilemap.SetTile(position, newTileOwner.glowTilemapTile);
    }

    private void SetTileOwnerInOwnedPositionsList(Vector3Int position, TileOwnerIndex tileOwnerIndex)
    {
        if (ownedPositions.ContainsKey(position))
        {
            if (tileOwnerIndex == TileOwnerIndex.none)
            {
                ownedPositions.Remove(position);
            }
            else
            {
                ownedPositions[position] = TileOwnerIndexToOwner(tileOwnerIndex);
            }
        }
        else 
        {
            if (tileOwnerIndex != TileOwnerIndex.none)
            {
                ownedPositions.Add(position, TileOwnerIndexToOwner(tileOwnerIndex));
            }
        }
    }

    public void ReverseTileOwner(Vector3Int position)
    {
        if (!ownedPositions.TryGetValue(position, out TileOwner tileOwner)) { return; }

        bool isCurrentlyPlayerTile = tileOwner == playerTileOwner;

        SetTileOwner(position, isCurrentlyPlayerTile ? TileOwnerIndex.enemy : TileOwnerIndex.player, true);
    }

    public void NeutralizeTileOwner(Vector3Int position)
    {
        if (!ownedPositions.TryGetValue(position, out TileOwner tileOwner)) { return; }

        SetTileOwner(position, TileOwnerIndex.none, true);
    }

    public void NeutralizeOnlyOneTileOwner(Vector3Int position, TileOwnerIndex toNeutralize)
    {
        if (!ownedPositions.TryGetValue(position, out TileOwner tileOwner)) { return; }
        if (tileOwner != TileOwnerIndexToOwner(toNeutralize)) { return; }

        SetTileOwner(position, TileOwnerIndex.none, true);
    }

    void CreateFreshTiledataSOs()
    {
        for (int i = 0; i < allTileData.Count; i++)
        {
            allTileData[i] = Instantiate(allTileData[i]);
        }
    }

    public TileData GetTileData(TileBase tileBase)
    {
        TileData result = allTileData.FirstOrDefault(x => x.tile == tileBase);
        return result == null ? allTileData[0] : result;
    }

    public enum TileOwnerIndex
    {
        none, player, enemy
    }

    private TileOwner TileOwnerIndexToOwner(TileOwnerIndex tileOwnerIndex)
    {
        if (tileOwnerIndex == TileOwnerIndex.none)
        {
            return noOwner;
        }
        else if (tileOwnerIndex == TileOwnerIndex.player)
        {
            return playerTileOwner;
        }
        else
        {
            return enemyTileOwner;
        }
    }
}

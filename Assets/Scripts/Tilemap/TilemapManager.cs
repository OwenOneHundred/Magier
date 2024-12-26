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
    public List<TileOwner> allOwners;

    public static TilemapManager tilemapManager; // public static reference to this to be used from everywhere

    public Spell spellToTest;
    public Vector3Int positionToTestSpell = new Vector3Int(3, 3);

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
        spellToTest.OnHovered(positionToTestSpell, 6);
        spellToTest.OnCast(positionToTestSpell, 6, allOwners[0]);
        //spellToTest.OnUnhovered(positionToTestSpell, 6);
    }

    void Update()
    {
        SetCurrentHoveredTile();
    }

    void SetCurrentHoveredTile() // update data based on where mouse is now
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
            currentHoveredTiledata.OnHovered(newTilePosition); // hover new position
        }
    }

    /// <summary>
    /// Sets owner of tile, calls owner.OnOwnTile(position), and adds to dict of owned tiles. 
    /// </summary>
    /// <param name="position">Grid position of tile</param>
    /// <param name="ownerName">name of the owner. Player, etc</param>
    public void SetTileOwner(Vector3Int position, string ownerName = "Player")
    {
        TileOwner tileOwner = GetOwnerByName(ownerName);
        ownedPositions.Add(position, tileOwner);
        tileOwner.OnOwnTile(position);
        tileBordersTilemap.SetTile(position, tileOwner.borderTilemapTile);
        glowTilemap.SetTile(position, tileOwner.glowTilemapTile);
    }
    
    /// <summary>
    /// Sets owner of tile, calls owner.OnOwnTile(position), and adds to dict of owned tiles. 
    /// </summary>
    /// <param name="position">Grid position of tile</param>
    /// <param name="owner">name of the owner. Player, etc</param>
    public void SetTileOwner(Vector3Int position, TileOwner tileOwner)
    {
        ownedPositions.Add(position, tileOwner);
        tileOwner.OnOwnTile(position);
        tileBordersTilemap.SetTile(position, tileOwner.borderTilemapTile);
        glowTilemap.SetTile(position, tileOwner.glowTilemapTile);
    }

    public TileOwner GetOwnerByName(string name)
    {
        TileOwner toReturn = allOwners.FirstOrDefault(x => x.name == name);
        if (toReturn == null) 
        {
            Debug.LogWarning("Tried to get owner: " + name +
                " but no owner of that name exists in allOwners list. Returned allOwners[0].");
            return allOwners[0];
        }
        else 
        {
            return toReturn;
        }   
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
        return allTileData.FirstOrDefault(x => x.tile == tileBase);
    }
}

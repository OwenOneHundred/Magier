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

    public Tilemap groundTilemap;
    public Tilemap tileBordersTilemap;

    public List<TileData> allTileData;

    public static TilemapManager tilemapManager; // public static reference to this to be used from everywhere

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

        InputManager.inputManager.onClick += OnClick; // On click, call this class's onclick method
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

    public void OnClick(CallbackContext context) // on click
    {
        if (currentHoveredTiledata != null) // if currently hovering a tile (not nothing)
        {
            currentHoveredTiledata.OnClicked(currentHoveredTilePosition); // call that tile's tiledata's OnClicked
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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager tilemapManager;
    [System.NonSerialized] public TileBase currentHoveredTile;
    [System.NonSerialized] public TileData currentHoveredTiledata;
    [System.NonSerialized] public Vector3Int currentHoveredTilePosition;
    void Awake()
    {
        if (tilemapManager == null || tilemapManager == this)
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
        GetComponent<TilemapGenerator>().GenerateTilemap(); // for testing

        InputManager.inputManager.onClick += OnClick;
    }

    void Update()
    {
        SetCurrentHoveredTile();
    }

    void SetCurrentHoveredTile()
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

    public void OnClick()
    {
        if (currentHoveredTiledata != null)
        {
            currentHoveredTiledata.OnClicked(currentHoveredTilePosition);
        }
    }

    public List<TileData> allTileData;
    void CreateFreshTiledataSOs()
    {
        for (int i = 0; i < allTileData.Count; i++)
        {
            allTileData[i] = Instantiate(allTileData[i]);
        }
    }

    public Tilemap groundTilemap;
    public Tilemap tileBordersTilemap;

    public TileData GetTileData(TileBase tileBase)
    {
        return allTileData.FirstOrDefault(x => x.tile == tileBase);
    }
}

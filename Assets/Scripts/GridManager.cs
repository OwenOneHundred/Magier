using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _gridParent;

    private Tile[,] _tiles;

    private void Start()
    {
        GenerateGrid();
    }

    // Generates a grid of tiles.
    private void GenerateGrid()
    {
        _tiles = new Tile[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var isOffset = (x + y) % 2 == 1;

                // Instantiate tile and initialize it.
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, _gridParent);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(isOffset);

                _tiles[x, y] = spawnedTile;
            }
        }

        Camera.main.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (position.x >= 0 && position.x < _width && position.y >= 0 && position.y < _height)
        {
            return _tiles[(int)position.x, (int)position.y];
        }
        return null;
    }
}
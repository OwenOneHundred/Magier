using UnityEngine;

/// <summary>
/// Basic tile with no special properties. This will be the default tile.
/// </summary>
[CreateAssetMenu(menuName = "Tile/Default Tile")]
public class DefaultTile : TileData
{
    public override void OnClicked(Vector3Int position)
    {
        Debug.Log("Clicked tile " + this.name);
    }
    public override void OnHovered(Vector3Int position)
    {
        SetTileColour(Color.gray, position);
    }
    public override void OnUnhovered(Vector3Int position)
    {
        SetTileColour(Color.white, position);
    }
    public override void OnPlayerClaimed(Vector3Int position)
    {

    }
    public override void OnEnemyClaimed(Vector3Int position)
    {

    }
    public override void OnBecomeUnclaimed(Vector3Int position)
    {

    }
}

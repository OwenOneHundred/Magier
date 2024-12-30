using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Abstract class for spells. Determines what spells do when casted, their costs, etc.
/// </summary>
public abstract class Spell : ScriptableObject, IHasPrice
{
    public int manaCost = 5;
    [SerializeField] private int price;
    protected bool hovered = false;
    public TileCaptureType tileCaptureType = TileCaptureType.capture;
    public abstract void OnCast(Vector3Int position, int diceRoll, bool player);
    public abstract List<Vector3Int> GetSelectedTiles(Vector3Int position, int diceRoll);
    public abstract void WhileHovering(Vector3Int position, int diceRoll);
    public abstract void OnUnhovered(Vector3Int position);

    public enum TileCaptureType
    {
        capture, neutralize, swap, removeEnemy
    }

    public int GetPrice()
    {
        return price;
    }
}

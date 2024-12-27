using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Abstract class for spells. Determines what spells do when casted, their costs, etc.
/// </summary>
public abstract class Spell : ScriptableObject
{
    public int manaCost = 5;
    public int price;
    protected bool hovered = false;
    public abstract void OnCast(Vector3Int position, int diceRoll, TileOwner caster);
    public abstract List<Vector3Int> GetSelectedTiles(Vector3Int position, int diceRoll);
    public abstract void WhileHovering(Vector3Int position, int diceRoll);
    public abstract void OnUnhovered(Vector3Int position);
}

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Abstract class for spells. Determines what spells do when casted, their costs, etc.
/// </summary>
public abstract class Spell : ScriptableObject
{
    public int manaCost = 5;
    public int price;

    public abstract void OnCast(TileBase tileBase);
}

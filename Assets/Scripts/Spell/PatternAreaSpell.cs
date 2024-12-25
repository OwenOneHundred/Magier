using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Spell/Pattern Area Spell")]
public class PatternAreaSpell : Spell
{
    [TextArea] [SerializeField]
    [Tooltip("Represents the pattern of tiles affected by the spell.\n" +
    "Spell position is center of array with even numbers rounded down. (4x4 center is (2, 2), 5x5 center is (3, 3)).\n" +
    "X: Hit by spell.\nO: Not hit by spell.")]

    string spellPattern;
    public override void OnCast(TileBase tileBase)
    {
        
    }
}

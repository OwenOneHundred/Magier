using UnityEngine;

public class TileEffect : ScriptableObject
{
    /// <summary>
    /// How long the effect lasts. Current turn counts, so a length of 2 lasts this turn and the opponent's turn.
    /// </summary>
    public int turnLength = 2;
    public int turnCount = 0;
    public bool protectedFromPlayer = false;
    public bool protectedFromEnemy = false;

}

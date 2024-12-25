using System.Collections;
using UnityEngine;

/// <summary>
/// Handles logic that takes turns between player and opponent.
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;
    void Awake()
    {
        if (turnManager == null || turnManager == this) // if tilemap manager already exists, destroy this one
        {
            turnManager = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    public GameState state = GameState.playerTurn;

    void Update()
    {
        if (state == GameState.playerTurn)
        {

        }
    }

    void EndEnemyTurn()
    {

    }
    
    void StartEnemyTurn()
    {
        state = GameState.enemyTurn;
    }
    
    void EndPlayerTurn()
    {

    }

    void StartPlayerTurn()
    {
        state = GameState.playerTurn;
    }

    public enum GameState
    {
        playerTurn, enemyTurn, neither
    }
}

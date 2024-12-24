using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameState state = GameState.playerTurn;

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

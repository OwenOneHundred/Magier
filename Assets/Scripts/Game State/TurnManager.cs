using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Handles logic that takes turns between player and opponent.
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;
    private IngameUIController ingameUIController;
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

    void Start()
    {
        ingameUIController = GameObject.FindAnyObjectByType<IngameUIController>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current.IsPointerOverGameObject(PointerInputModule.kMouseLeftId))
            {
                Debug.Log("state: " + state + " over UI object?: " + EventSystem.current.IsPointerOverGameObject());
                
                // if player turn and not clicking on UI
                if (state == GameState.playerTurn && !EventSystem.current.IsPointerOverGameObject())
                {
                    TryCastSpell();
                }
            }
        }
    }

    void TryCastSpell()
    {
        Debug.Log("try cast spell");
        if (ingameUIController.selectedSpell == null) { return; }

        if (ingameUIController.currentMana < ingameUIController.selectedSpell.manaCost) { return; }

        Vector3Int castTilePos = TilemapManager.tilemapManager.currentHoveredTilePosition;
        ingameUIController.selectedSpell.OnCast(castTilePos,
            UnityEngine.Random.Range(1, 7), // TODO replace when we have dice that can be rolled
            TilemapManager.tilemapManager.GetOwnerByName("Player"));
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

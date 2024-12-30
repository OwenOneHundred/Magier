using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Handles logic that takes turns between player and opponent.
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;
    private IngameUIController ingameUIController;
    public Dice diceToTest;
    public GameObject diceGameObject;
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
        if (ingameUIController.selectedSpell == null) { return; }
        if (!EventSystem.current.IsPointerOverGameObject(PointerInputModule.kMouseLeftId)) // mouse not over UI
        {
            if (Mouse.current.leftButton.wasPressedThisFrame) // mouse not over UI - clicking
            {
                if (state == GameState.playerTurn && !EventSystem.current.IsPointerOverGameObject())
                {
                    TryCastSpell();
                }
            }
            else // mouse not over UI - not clicking
            {
                ingameUIController.selectedSpell.WhileHovering(TilemapManager.tilemapManager.currentHoveredTilePosition, 6);
            }
        }
        else // mouse over UI
        {
            ingameUIController.selectedSpell.OnUnhovered(TilemapManager.tilemapManager.currentHoveredTilePosition);
        }
    }

    void TryCastSpell() // casts a spell, also rolls a die
    {
        Debug.Log("try cast spell");
        if (ingameUIController.selectedSpell == null) { return; }

        if (ingameUIController.currentMana < ingameUIController.selectedSpell.manaCost) { return; }

        // this should be moved into the turn start logic
        int rolledNumber = Instantiate(diceGameObject, Vector3.zero, Quaternion.identity) // creates die number, rolls die
            .GetComponent<DiceObjectRoller>()
            .Roll(diceToTest);

        Vector3Int castTilePos = TilemapManager.tilemapManager.currentHoveredTilePosition;
        ingameUIController.selectedSpell.OnCast(castTilePos,
            rolledNumber, 
            true); // true means spell was casted by player
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

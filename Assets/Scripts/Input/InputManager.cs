using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Class that recieves player input and invokes events that other classes use.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager inputManager;
    void Awake()
    {
        if (inputManager == null || inputManager == this)
        {
            inputManager = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public Action onClick;

    public void OnClick(CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            onClick?.Invoke();
        }       
    }
}

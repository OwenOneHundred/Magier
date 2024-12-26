using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class CursorController : MonoBehaviour
{
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite clickingSprite;
    [SerializeField] Color baseColor;
    [SerializeField] Color disabledColor;
    Image image;
    Camera mainCamera;
    RectTransform canvasRect;
    RectTransform rect;
    private bool disabled = false;
    public bool Disabled
    {
        get { return disabled; }
        set
        {
            image.color = value ? disabledColor : baseColor;
            disabled = value;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        canvasRect = transform.root.GetComponent<RectTransform>();
        InputManager.inputManager.onClick += OnClick;
        mainCamera = Camera.main;
        rect = GetComponent<RectTransform>();
    }

    void OnClick(CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            image.sprite = clickingSprite;
        }
        else 
        {
            image.sprite = baseSprite;
        }
    }

    void SetPositionToMouse()
    {
        Vector2 ViewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*0.5f)),
        ((ViewportPosition.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*0.5f)));

        //now you can set the position of the ui element
        rect.anchoredPosition = WorldObject_ScreenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SetPositionToMouse();
    }
}

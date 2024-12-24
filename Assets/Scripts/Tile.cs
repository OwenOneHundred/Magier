using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    public bool IsOwned { get; private set; }

    // Initializes the tile's color based on its offset status.
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    // Highlights the tile when the mouse enters it.
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    // Removes highlight when the mouse exits.
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    // Sets ownership of the tile.
    public void SetOwnership(Sprite sprite)
    {
        if (IsOwned) return;

        _renderer.sprite = sprite;
        IsOwned = true;
    }
}
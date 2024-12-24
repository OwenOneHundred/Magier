using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    [SerializeField] List<Owner> allOwners; // list of all owners, assigned in inspector.
    // this is on every tile, so it would be slightly slow if you had 1000+ tiles or 40+ owners
    // Should be ordered by most to least common owner for very slight performance boost

    public Owner CurrentOwner { get; private set; }

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

    // Sets ownership of the tile by owner name.
    public bool SetOwnership(string ownerName)
    {
        if (CurrentOwner != null) return false;

        Owner newOwner = allOwners.First(x => x.name == ownerName);

        _renderer.sprite = newOwner.tileSprite;
        CurrentOwner = newOwner;

        return true;
    }

    [System.Serializable]
    public class Owner // owner class. Holds a name and a sprite. Could be expanded to have effects, etc
    {
        public string name = "";
        public Sprite tileSprite;
    }
}
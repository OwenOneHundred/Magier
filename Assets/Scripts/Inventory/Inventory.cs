using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Lumins { get; private set; }
    public List<Spell> spells;
    public Dice dice;


    Inventory inventory;
    void Awake()
    {
        if (inventory == null || inventory == this) // if tilemap manager already exists, destroy this one
        {
            inventory = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }


}

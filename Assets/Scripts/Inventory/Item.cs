using UnityEngine;

public abstract class Item : ScriptableObject, IHasPrice
{
    [SerializeField] private int price;
    
    public int GetPrice()
    {
        return price;
    }

    public abstract void Use();
}

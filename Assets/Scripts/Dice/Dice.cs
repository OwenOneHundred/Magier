using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Die")]
public class Dice : ScriptableObject, IHasPrice
{
    [SerializeField] int price;
    [SerializeField] private List<DieFace> dieFaces = new List<DieFace>();
    public List<Sprite> rollAnimSprites;
    
    public int RollDie()
    {
        int randomIndex = Random.Range(0, dieFaces.Count);
        return dieFaces[randomIndex].value;
    }
    
    public Sprite GetFaceSprite(int value)
    {
        foreach(DieFace face in dieFaces)
        {
            if(face.value == value)
            {
                return face.faceSprite;
            }
        }
        return null;
    }

    public int GetPrice()
    {
        return price;
    }

    [System.Serializable]
    public class DieFace
    {
        public int value;
        public Sprite faceSprite;
    }
}
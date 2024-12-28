using System.Collections;
using UnityEngine;

public class DiceObjectRoller : MonoBehaviour
{
    [SerializeField] float bounceTime = 1.2f;
    [SerializeField] float rollAnimFrameLength = 0.08f;
    [SerializeField] float speedOnBounce = 20;
    SpriteRenderer sr;
    public int Roll(Dice die)
    {
        sr = GetComponent<SpriteRenderer>();

        int numberRolled = die.RollDie();
        StartCoroutine(RollAnimation(die, numberRolled));
        return numberRolled;
    }

    IEnumerator RollAnimation(Dice die, int numberRolled)
    {
        float frameTimer = 0;
        float overallTimer = 0;
        int spriteNumber = 0;
        float velocity = speedOnBounce;
        while (overallTimer < bounceTime)
        {
            if (frameTimer > rollAnimFrameLength)
            {
                spriteNumber += 1;
                frameTimer = 0;
                if (spriteNumber >= die.rollAnimSprites.Count) 
                {
                    spriteNumber = 0;
                }
            }
            sr.sprite = die.rollAnimSprites[spriteNumber];

            transform.position += velocity * Time.deltaTime * Vector3.up;
            velocity -= ((speedOnBounce * 2) / bounceTime) * Time.deltaTime;
            
            frameTimer += Time.deltaTime;
            overallTimer += Time.deltaTime;
            yield return null;
        }

        sr.sprite = die.GetFaceSprite(numberRolled);
        Destroy(gameObject, 2);
    }
}

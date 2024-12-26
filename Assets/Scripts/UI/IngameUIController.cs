using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Has a public variable that contains what spell is currently selected.
/// Changes what spell is selected when UI button is pressed.
/// Also has a public reference to the mana slider, and a variable for mana that constantly increases.
/// </summary>
public class IngameUIController : MonoBehaviour 
{
    [SerializeField] public Slider manaSlider;
    [SerializeField] public int currentMana = 0;
    [SerializeField] public int maxMana = 20;
    [SerializeField] public int manaRegen = 5;
    
    // Contains the current selected spell. (once you do that)
    [SerializeField] private Spell selectedSpell;

    [SerializeField] private List<Spell> spellList = new List<Spell> ();

    // Called by Unity Events on the buttons, sets the current selected spell (once you do both of those things)
    public void SetSelectedSpell(int buttonIndex)
    {
        selectedSpell = spellList[buttonIndex]; 
    }

    public void OnTurnStart()
    {
        currentMana += manaRegen;
        UpdateManaDisplay();
    }

    // Make mana variable higher, with cap of 20. then update the slider
    private void UpdateManaDisplay()
    {
        manaSlider.value = currentMana / maxMana;
    }

}

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
    public Slider manaSlider;
    public int currentMana = 0;
    public int maxMana = 20;
    public int manaRegen = 5;
    
    // Contains the current selected spell.
    public Spell selectedSpell;
    int selectedButtonIndex = -1;
    [SerializeField] private List<Spell> spellList = new List<Spell> ();

    // Called by Unity Events on the buttons, sets the current selected spell
    public void SetSelectedSpell(int buttonIndex)
    {
        if (selectedButtonIndex == buttonIndex) // if you press the same button again, deselect spell
        {
            selectedButtonIndex = -1;
            selectedSpell = null;
            return;
        }

        selectedSpell = spellList[buttonIndex];
        selectedButtonIndex = buttonIndex; 
    }

    public void OnTurnStart()
    {
        currentMana += manaRegen;
        UpdateManaDisplay();
    }

    private void UpdateManaDisplay()
    {
        manaSlider.value = currentMana / maxMana;
    }

}

using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Has a public variable that contains what spell is currently selected.
/// Changes what spell is selected when UI button is pressed.
/// Also has a public reference to the mana slider, and a variable for mana that constantly increases.
/// </summary>
public class IngameUIController : MonoBehaviour
{
    // Contains the current selected spell. (once you do that)
    public Spell selectedSpell;
    List<Spell> availableSpells;

    public void OnPressSpellButton(int buttonIndex)
    {
        {
            selectedSpell = availableSpells[buttonIndex];
        }
    }

    // Called by Unity Events on the buttons, sets the current selected spell (once you do both of those things)
    public void SetSelectedSpell(int buttonIndex)
    {

    }

    // Make mana variable higher, with cap of 20. then update the slider
    void Update()
    {
        
    }
}

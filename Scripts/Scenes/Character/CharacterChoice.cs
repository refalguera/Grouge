using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoice : MonoBehaviour {

    [HideInInspector]
    public bool stella, luis;
  
    //After chosen the character is activated a boolean corresponds to the character and made the rendering to the screen of the first phase. 
    //The object that loads the script is not destroyed so you know which character was selected for play
    private void Awake()
    {
        stella = false;
        luis = false;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CharacterChoiceStella()
    {
        stella = true;
        Application.LoadLevel(1);
    }

    public void CharacterChoiceLuis()
    {
        luis = true;
        Application.LoadLevel(1);
    }
}

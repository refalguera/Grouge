using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoice : MonoBehaviour {

    [HideInInspector]
    public bool stella, luis;
  
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

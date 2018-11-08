using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    private AudioSource _buttonsound;

	public void Start_Game()
    {
        _buttonsound = GameObject.Find("Start_Button").GetComponent<AudioSource>();
        _buttonsound.Play();
        Application.LoadLevel(4);
    }
}

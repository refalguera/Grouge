using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    private AudioSource _buttonsound;

	//If the "start" button is pressed it redirects to the instruction screen
	public void Start_Game()
    {
        _buttonsound = GameObject.Find("Start_Button").GetComponent<AudioSource>();
        _buttonsound.Play();
        Application.LoadLevel(4);
    }
}

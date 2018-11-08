using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(LoadingScene());
	}
	
   //The instruction screen stays for 10 seconds. After this time the rendering is done for the character selection screen
	IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(10f);
        Application.LoadLevel(5);
    }
}

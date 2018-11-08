using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_Scene : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(LoadingScene());
	}
	
	IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(10f);
        Application.LoadLevel(5);
    }
}

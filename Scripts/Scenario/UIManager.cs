using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _capsuleScoreImage;

    [SerializeField]
    private Sprite[] _lifeBarImage;

    public GameObject life;

    [SerializeField]
    private Material _mymaterial;

    [SerializeField]
    private Text _time;

    [HideInInspector]
    public int minutes;
    [HideInInspector]
    public int seconds;

    private GameManager _gamemanager;
   
    //Modifies the colors of the capsule particles present on the canvas
    public void ChangeColorCapsulesParticulesScore(int i)
    {
        _capsuleScoreImage[i].GetComponent<Image>().material = _mymaterial;
    }

    //Modify the number of lives image on the canvas
    public void ChangeLifeBar(int _life)
    {
        if(_life <= 1)
        {
            life.GetComponent<Image>().sprite = _lifeBarImage[_life];
        }
        else
        {
            life.GetComponent<Image>().sprite = _lifeBarImage[_life];
            _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _gamemanager.gameover = true;
        }
    }

    //Calculate playing time and show on canvas
    public void Timer()
    {
        minutes = (int)Time.time / 60;
        seconds = (int)Time.time % 60;

        if (minutes != 10)
        {
            _time.text = "Time : " + minutes.ToString() + ":" + seconds.ToString();
        }
        else
        {
            _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _gamemanager.gameover = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public bool gameover;

    [HideInInspector]
    public bool startgame;

    [HideInInspector]
    public int _capsuleCount;

    [SerializeField]
    private GameObject _stellaPrefab;
    [SerializeField]
    private GameObject _luisPrefab;

    private UIManager _UIManager;
    [SerializeField]
    private GameObject _BodySourceManager;
    [SerializeField]
    private GameObject _CustomGestureManager;

    [SerializeField]
    private Camera _cameraMovemment;

    [SerializeField]
    private GameObject _spawnManager;

    private void Awake()
    {
        CharacterChoice _choice = GameObject.Find("CharacterChoice").GetComponent<CharacterChoice>();

         if(_choice!= null)
        {
             if(_choice.stella)
            {
                _stellaPrefab.SetActive(true);
            }
             else if (_choice.luis)
            {
                _luisPrefab.SetActive(true);
            }
        }

        ActivateObjects();
    }
    private void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
       

        gameover = false;
        startgame = false;
    }
    
    public void Update()
    {
        if (startgame == true)
        {
            //Start the game
         
            if (gameover == false)
            {
                //As the game occurs the time is counted
                _UIManager.Timer();
            }
            else if (gameover == true)
            {
                //If you finish the game, it is analyzed how many capsules were captured, if it was 9, the player wins. 
                //Otherwise lose.
                if (_capsuleCount == 9)
                {
                  // Display winner screen
                    Application.LoadLevel(3);
                }
                else
                {
                   // Display gameover screen
                    Application.LoadLevel(2);
                }
            }
      
        }
    }

    // Activate camera movement, spawn objects and the AI scripts
    private void ActivateObjects()
    {
        AtivateCamera();
        AtivateGestureFinder();
    }
    
    private void AtivateCamera()
    {
        _spawnManager.GetComponent<SpawnManager>().enabled = true;
        _cameraMovemment.GetComponent<CameraMovemment>().enabled = true;
    }

    private void AtivateGestureFinder()
    {
        _BodySourceManager.GetComponent<BodySourceManager>().enabled = true;
        _CustomGestureManager.GetComponent<CustomGestureManager>().enabled = true;
    }
}


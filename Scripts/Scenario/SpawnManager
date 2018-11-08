using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {


    [SerializeField]
    private GameObject _blocoPrefab;

    [SerializeField]
    private GameObject _blocoPrefab_2;


    public List<GameObject> _listaenemys;
    public List<GameObject> _listacapsulas;
    private List<GameObject> _listablocos_1;
    private List<GameObject> _listablocos_2;
    public List<GameObject> _powerUp;
    public List<GameObject> _lifeHeart;


    //    private GameManager _gameManager;

    // Use this for initialization
    void Start () {

      //  _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _listablocos_1 = new List<GameObject>();
        _listablocos_2 = new List<GameObject>();

        StartSpawnRoutines();
	}

   public void BlocosDamage()
    {
        if(_listablocos_1.Count > 0)
        {
            _listablocos_1.RemoveAt(0);
            ReposicionaBloco();
        }    

    }

    public void Blocos_2_Damage()
    {
        if (_listablocos_2.Count > 0)
        {
            _listablocos_2.RemoveAt(0);
            ReposicionaBloco_2();
        }
    }

    public void StartSpawnRoutines()
    {
        EnemySpawnRoutine();
        CapsulaSpawnRoutine();
        BlocoSpawnRoutine();
        PowerUpRoutine();
        LifeRoutine();
    }
	
    private void EnemySpawnRoutine()
    {
      //  while (!_gameManager.gameover)
       // {
            for (int i = 0; i < _listaenemys.Count; i++)
            {
            Instantiate(_listaenemys[i], _listaenemys[i].transform.position, Quaternion.identity);
            }
        //
    }

    private void CapsulaSpawnRoutine()
    {
        
        for (int i = 0; i < _listacapsulas.Count; i++)
        {
            Instantiate(_listacapsulas[i], _listacapsulas[i].transform.position, Quaternion.identity);
        }
    }

    private void BlocoSpawnRoutine()
    {

        for (int i = 0; i <= _listablocos_2.Count; i++)
        {
            GameObject novoObj = CriaNovoBloco(new Vector3(19.20469f, -2.54f + (i * 0.787f), 0),_blocoPrefab);
            _listablocos_1.Add(novoObj);
            novoObj = CriaNovoBloco(new Vector3(33.32f, -2.54f + (i * 0.787f), 0),_blocoPrefab_2);
            _listablocos_2.Add(novoObj);
        }
    }

    private void PowerUpRoutine()
    {
        for(int i = 0; i < _powerUp.Count; i++)
        {
            Instantiate(_powerUp[i], _powerUp[i].transform.position, Quaternion.identity);
        }
    }

    private void LifeRoutine()
    {
        for (int i = 0; i <2; i++)
        {
            Instantiate(_lifeHeart[i], _lifeHeart[i].transform.position, Quaternion.identity);
        }
    }

    GameObject CriaNovoBloco(Vector3 posicao,GameObject bloco)
    {
        GameObject novobloco = Instantiate(bloco);

        novobloco.transform.position = posicao;
        return novobloco;
    }

    void ReposicionaBloco()
    {
        for (int i = 0; i <= _listablocos_1.Count; i++)
        {
            _listablocos_1[i].transform.position = new Vector2 (_listablocos_1[i].transform.position.x,  _listablocos_1[i].transform.position.y -0.77f);
        }
    }

    void ReposicionaBloco_2()
    {
        for (int i = 0; i <= _listablocos_2.Count; i++)
        {
            _listablocos_2[i].transform.position = new Vector2(_listablocos_2[i].transform.position.x, _listablocos_2[i].transform.position.y - 0.77f);
        }
    }
}

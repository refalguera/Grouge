using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

   
    public List<GameObject> _enemiesList;
    public List<GameObject> _capsulesList;
    public List<GameObject> _shieldsList;
    public List<GameObject> _lifeHeartList;


    void Start()
    {
       
        EnemySpawnRoutine();
        CapsulaSpawnRoutine();
        ShieldSpwanRoutine();
        LifeRoutine();
    }

	//Spawn Enemies prefabs
     private void EnemySpawnRoutine()
    {
       
        for (int i = 0; i < _enemiesList.Count; i++)
        {
            Instantiate(_enemiesList[i], _enemiesList[i].transform.position, Quaternion.identity);
        }
        
    }
  //Spawn capsule particles prefabs
   private void CapsulaSpawnRoutine()
    {

        for (int i = 0; i < _capsulesList.Count; i++)
        {
            Instantiate(_capsulesList[i], _capsulesList[i].transform.position, Quaternion.identity);
        }
    }

	//Spawn shield activator prefabs
     private void ShieldSpwanRoutine()
    {
        for (int i = 0; i < _shieldsList.Count; i++)
        {
            Instantiate(_shieldsList[i], _shieldsList[i].transform.position, Quaternion.identity);
        }
    }
	
       //Spawn lives prefabs
   private void LifeRoutine()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(_lifeHeartList[i], _lifeHeartList[i].transform.position, Quaternion.identity);
        }
    }


}

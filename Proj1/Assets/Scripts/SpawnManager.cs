using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Create a coroutine of type IEnumerator to yeild code execution
    //Run while some condition is true

    IEnumerator SpawnRoutine()
    {
        //while loop, some condition is true
        while(_stopSpawning == false)
        {
            //create a new variable of type Vector3 to store a random spawn location
            Vector3 posToSpawn = new Vector3(Random.Range(-7.5f, 7.5f), 8f, 0);

            //Spawn gameobject
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            //yield for x seconds
            yield return new WaitForSeconds(10f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

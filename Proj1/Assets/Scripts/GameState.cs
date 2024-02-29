using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Constants;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning;
    public int _enemiesKilled;
    public static bool _gameIsActive;

    // Start is called before the first frame update
    void Start()
    {
        _enemiesKilled = 0;
        _gameIsActive = true;
        StartCoroutine(spawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    // spawnEnemyRoutine() periodically spawns enemies
    // Pre: Game state is active
    // Post: an enemy spawns somewhere on the border
    IEnumerator spawnEnemyRoutine() {
        int _randNum = 0;
        float _spawnX = 0;
        float _spawnY = 0;
        Vector3 _spawnVector = new Vector3(0, 0, 0);
        while (!_stopSpawning) {
            _randNum = Random.Range(1, 4);
            _spawnX = Random.Range(MIN_X, MIN_Y);
            _spawnY = Random.Range(MIN_Y, MAX_Y);
            switch(_randNum) {
                case 1:
                    _spawnVector = new Vector3(_spawnX, MIN_Y);
                break;

                case 2:
                    _spawnVector = new Vector3(_spawnX, MAX_Y);
                break;

                case 3:
                    _spawnVector = new Vector3(MIN_X, _spawnY);
                break;

                case 4:
                    _spawnVector = new Vector3(MAX_X, _spawnY);
                break;
            }
            Instantiate(_enemyPrefab, _spawnVector, Quaternion.identity, _enemyContainer.GetComponent<Transform>());
        }
        yield return new WaitForSeconds(Random.Range(9, 15));
    }


    // playerDeath() resets variables and stops enemies
    // Pre: player runs out of health
    // Post: all necessary variables reset, death screen printed to player
    public static void playerDeath() {
        printDeathScreen();
        _gameIsActive = false;
    }

    // printDeathScreen() changes the UI to a death screen with options to return to main menu or play again
    // Pre: player runs out of health
    // Post: death screen output to player
    public static void printDeathScreen() {
        
    }
}

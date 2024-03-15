using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject _enemyPrefab1;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;
    [SerializeField] private GameObject _enemyContainer;
    UIManager _uIManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameIsActive = true;
        _uIManager = uiCanvas.GetComponent<UIManager>();
        StartCoroutine(spawnEnemyRoutine());
    }

    void Update() {
        if (Input.GetKeyDown(RESTART_GAME_BUTTON) && _gameIsActive == false) {
            SceneManager.LoadScene(0);
        }
    }
    

    // spawnEnemyRoutine() periodically spawns enemies
    // Pre: Game state is active
    // Post: an enemy spawns somewhere on the border
    IEnumerator spawnEnemyRoutine() {
        while (_gameIsActive) {
            Instantiate(_enemyPrefab1, randomSpawnVector(), Quaternion.identity, _enemyContainer.transform);
            yield return new WaitForSeconds(Random.Range(2, 3));
            if (_uIManager.GetScore() > 50) {
                if (Random.Range(1, 2) == 1) {
                    Instantiate(_enemyPrefab2, randomSpawnVector(), Quaternion.identity, _enemyContainer.transform);
                }
            }
            yield return new WaitForSeconds(Random.Range(2, 3));
            if (_uIManager.GetScore() > 100) {
                if (Random.Range(1, 2) == 1) {
                    Instantiate(_enemyPrefab3, randomSpawnVector(), Quaternion.identity, _enemyContainer.transform);                
                }
            }
            yield return new WaitForSeconds(Random.Range(2, 3));
        }
    }

    
    Vector3 randomSpawnVector() {
        int _randNum = 0;
        float _spawnX = 0.0f;
        float _spawnY = 0.0f;
        Vector3 _spawnVector = Vector3.zero;
        _randNum = Random.Range(1, 4);
        _spawnX = Random.Range(MIN_X, MAX_X);
        _spawnY = Random.Range(MIN_Y, MAX_Y);
        switch(_randNum) {
            case 1:
                _spawnVector = new Vector3(_spawnX, MIN_Y, 0);
            break;

            case 2:
                _spawnVector = new Vector3(_spawnX, MAX_Y, 0);
            break;

            case 3:
                _spawnVector = new Vector3(MIN_X, _spawnY, 0);
            break;

            case 4:
                _spawnVector = new Vector3(MAX_X, _spawnY, 0);
            break;
        }
        return _spawnVector;
    }

    public void GameOver() {
        _gameIsActive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Constants;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject _enemyPrefab1;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;
    [SerializeField] private GameObject _enemyContainer;
    UIManager _uIManager;
    private bool _stopSpawning;
    public int _enemiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        _enemiesKilled = 0;
        _uIManager = uiCanvas.GetComponent<UIManager>();
        StartCoroutine(spawnEnemyRoutine());
    }
    

    // spawnEnemyRoutine() periodically spawns enemies
    // Pre: Game state is active
    // Post: an enemy spawns somewhere on the border
    IEnumerator spawnEnemyRoutine() {
        int _randNum = 0;
        float _spawnX = 0;
        float _spawnY = 0;
        Vector3 _spawnVector = Vector3.zero;
        while (_gameIsActive) {
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
            Instantiate(_enemyPrefab1, _spawnVector, Quaternion.identity, _enemyContainer.transform);
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (_uIManager.GetScore() > 25) {
                Instantiate(_enemyPrefab2, _spawnVector * -1, Quaternion.identity, _enemyContainer.transform);
            }
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (_uIManager.GetScore() > 70) {
                Instantiate(_enemyPrefab2, _spawnVector * -1, Quaternion.identity, _enemyContainer.transform);                
            }
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

}

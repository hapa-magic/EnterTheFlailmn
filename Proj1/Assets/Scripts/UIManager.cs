using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImg;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _currentSpeed;
    [SerializeField] private TMP_Text _topSpeed;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private GameObject _player;
    [SerializeField] private TMP_Text _throwBallText;
    [SerializeField] private TMP_Text _increaseRotateText;
    private GameState _gameManager;
    private int _score;

    void Start()
    {
        _offeredIncreaseRotationYet = false;
        _offeredThrowYet = false;
        _scoreText.text = "Score: " + 0;
        _currentSpeed.text = "Current speed:" + MIN_ROTATION;
        _topSpeed.text = "Top speed: " + MIN_ROTATION;
        _restartText.gameObject.SetActive(false);
        _throwBallText.gameObject.SetActive(false);
        _increaseRotateText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameState>();
    }


    // updateScore() changes the score to reflect the enemies killed
    // Pre: enemy killed, player score passed
    // Post: score updated in UI
    public void UpdateScore(int playerScore)
    {
        _score = playerScore;
        _scoreText.text = "Score: " + playerScore.ToString();
        if (_offeredThrowYet == false && playerScore > 50) {
            _throwBallText.gameObject.SetActive(true);
            _offeredThrowYet = true;
        }
        if (_offeredIncreaseRotationYet == false && playerScore > 100) {
            _increaseRotateText.gameObject.SetActive(true);
            _offeredIncreaseRotationYet = true;
        }
    }

    public void EndThrowBallText() {
        _throwBallText.gameObject.SetActive(false);
    }

    public void EndIncreaseRotateText() {
        _increaseRotateText.gameObject.SetActive(false);
    }

    public void UpdateCurrentSpeed(float speed) {
        _currentSpeed.text = "Current speed: " + speed;
    }

    public void UpdateTopSpeed(float topSpeed) {
        _topSpeed.text = "Top speed: " + topSpeed;
    }

    public int GetScore() {
        return _score;
    }

    // updateLives() is called whenever the player takes damage
    // Pre: player collides with enemy
    // Post: sprite UI changes

    public void UpdateLives(int currentLives) {
        _livesImg.sprite = _liveSprites[currentLives];
        if(currentLives < 1) {
            GameOverSequence(); 
        }
    }


    public IEnumerator GameOverFlickerRoutine() {
        while(true) {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f); 
        }
    }

    public void GameOverSequence() {
        _increaseRotateText.gameObject.SetActive(false);
        _throwBallText.gameObject.SetActive(false);
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameIsActive = false;
        StartCoroutine(GameOverFlickerRoutine());
    }
}

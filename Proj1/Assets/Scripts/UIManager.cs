using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImg;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _currentSpeed;
    [SerializeField] private TMP_Text _topSpeed;
    [SerializeField] private GameObject _player;
    private GameState _gameManager;
    private int _score;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _currentSpeed.text = "Current speed:" + MIN_ROTATION;
        _topSpeed.text = "Top speed: " + MIN_ROTATION;
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameState>();
    }


    // updateScore() changes the score to reflect the enemies killed
    // Pre: enemy killed, player score passed
    // Post: score updated in UI
    public void UpdateScore(int playerScore)
    {
        _score += playerScore;
        _scoreText.text = "Score: " + playerScore.ToString();
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

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
    }
}

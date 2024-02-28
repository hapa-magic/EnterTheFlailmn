using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImg;
    [SerializeField] private TMP_Text _restartText;
    private GameState _gameManager;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameState>();
    }


    // updateScore() changes the score to reflect the enemies killed
    // Pre: enemy killed, player score passed
    // Post: score updated in UI
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    // updateLives() is called whenever the player takes damage
    // Pre: player collides with enemy
    // Post: sprite UI changes

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
    }
}

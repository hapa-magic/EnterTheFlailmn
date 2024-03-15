using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Constants;

public class Player : MonoBehaviour
{
    private int _startingZ = 0;
    private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private GameObject rotator;
    [SerializeField] private GameObject _objects;
    [SerializeField] private float _jumpTime;
    [SerializeField] private GameObject UIManager;
    [SerializeField] private GameObject _spikeBall;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private AudioClip _wooshClip;
    private AudioSource _audioSource;
    float horizontalInput;
    float verticalInput;
    private bool _isVulnerable;
    private bool _jumpIsActive;
    private int _playerHealth;
    private float _currentTopSpeed;
    private bool _alternateKeyPressed;
    UIManager _UIManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _playerHoldingBall = true;
        startPosition();
        _alternateKeyPressed = false;
        _UIManager = UIManager.GetComponent<UIManager>();
        _playerHealth = 3;
        _movementSpeed = 4f;
        _currentTopSpeed = (float)MIN_ROTATION;
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            Debug.LogError("AudioSource is NULL"); 
        }
        else { 
            _audioSource.clip = _wooshClip; 
        }
    }

    // Update is called once per frame
    void Update() {
        if (_gameIsActive) {
            if (_playerHoldingBall) {
                rotatePlayer();
                if (Input.GetKeyDown(RELEASE_BALL)) {
                    releaseBall();
                }
                else {
                    checkForSpin();
                }
            }
            movePlayer();
        }
    }


    // movePlayer() take controller input and translates playerObject
    // Pre: game is active, update() is called, other movement patterns not active
    // Post: player character moves in desired direction
    void movePlayer()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, _startingZ);
        transform.Translate(direction * _movementSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), 
                                         Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y));
    }


    // pickUpBall() brings the spikeBall object back to the player
    // Pre: _playHoldingBall is false
    // Post: _playerHoldingBall true, ball is a child of the rotate GameObject
    public void pickUpBall() {
        _movementSpeed = 4f;
        _playerHoldingBall = true;
        Vector3 targ = _spikeBall.transform.position;
        targ.z = 0f;

        Vector3 objectPos = rotator.transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        rotator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        _spikeBall.transform.parent = rotator.transform;
        _spikeBall.GetComponent<SpriteRenderer>().sprite = _spikeBall.GetComponent<SpikeBall>()._initSprite;
        setBallPosition();
    }


    // startPosition() initiates player position at a default location
    // Pre: game loaded
    // Post: Player instantiated at default location
    void startPosition() {
        rotator.transform.position = new Vector3Int(0, 0, _startingZ);
    }


    // rotatePlayer() takes _rotationSpeed and rotates playerSprite and spikeBall object (spikeBall will 
    // rotate as it is a child of playerSprite). Distance between player and spikeBall will mutate
    // Pre: _playerHoldingBall set to true, update() is called, game active
    // Post: playerSprite and spikeBall rotate around playerParent object
    void rotatePlayer() {
        rotator.transform.Rotate(0, 0, 360 * Time.deltaTime * _rotationSpeed);
    }


    // setRotationSpeed() sets the _rotationSpeed variable as a fixed number
    // Pre: ball is picked up, game starts
    // Post: spikeBall begins to rotate
    void setRotationSpeed() {
        _rotationSpeed = STARTING_ROTATION_SPEED;
        _UIManager.UpdateCurrentSpeed(_rotationSpeed);
    }


    // setRotationSpeed() sets the _rotationSpeed as the number passed through
    // Pre: float passed
    // Post: float changed
    void setRotationSpeed(float increase) {
        _rotationSpeed = increase;
        _UIManager.UpdateCurrentSpeed(_rotationSpeed);
    }


    // increaseRotation() mutates the _rotationSpeed variable
    // Pre: player input
    // Post: _rotationSpeed changed
    public void increaseRotation() {
        _rotationSpeed += INCREASE_ROTATION;
        _UIManager.UpdateCurrentSpeed(_rotationSpeed);
        if (_rotationSpeed > 3) {
            _spikeBall.GetComponent<SpriteRenderer>().sprite = _spikeBall.GetComponent<SpikeBall>()._glowBallSprite;
        }
        _UIManager.EndIncreaseRotateText();
    }


    // setBallY() mutates spikeBall’s Y value in relation to parentObject
    // Pre: ball is picked up, or rotation speed is mutated
    // Post: spikeBall orbits closer or farther to player
    void setBallPosition() {
        _spikeBall.transform.localPosition = new Vector3(0, (float)MIN_BALL_LEASH, 0);
    }


    // releaseBall() throws the spike ball in its current tradjectory
    // Pre: _playerHoldingBall is true, release button pressed
    // Post: _playerHoldingBall set to false, releaseBall() called
    void releaseBall() {
        _playerHoldingBall = false;
        GetComponentInChildren<SpikeBall>()._releaseTriggered = true;
        GetComponentInChildren<SpikeBall>()._speed = calcBallSpeed();
        setRotationSpeed();
        _spikeBall.transform.passBallToObjects(this.transform.position, rotator.transform, _objects.transform);
        _movementSpeed = 6f;
        _UIManager.EndThrowBallText();
        _audioSource.Play();
    }


    float calcBallSpeed() {
        float _ballSpeed = (_rotationSpeed * (float)MIN_BALL_LEASH * 2 * 3.142f);
        return _ballSpeed;
    }


    // endJump() resolves the jump action
    // Pre: end of jump reached
    // Post: game set back to normal
    void endJump() {
        _isVulnerable = true;
        _jumpIsActive = false;
    }


    // damage() deals an amount of damage to player
    // Pre: enemy collides with player
    // Post: player health is lowered
    public void damage(int damagePoints) {
        _playerHealth -= damagePoints;
        _UIManager.UpdateLives(_playerHealth);
    }

    // checkForSpin() assess if spin should increase or not
    // Pre: player has ball
    // Post: player spins faster (maybe)
    void checkForSpin() {
        if (!_alternateKeyPressed) {
            if (Input.GetKeyDown(ROTATE_KEY_1)) {
                _alternateKeyPressed = true;
            }
        }
        else {
            if (Input.GetKeyDown(ROTATE_KEY_2)) {
                if (_rotationSpeed < _currentTopSpeed) {
                    increaseRotation();
                    _alternateKeyPressed = false;
                }
            }
        }
    }
        

    // powerUp() increases the current top speed
    // Pre: player character collides with power up
    // Post: _currentTopSpeed increased
    public void powerUp() {
        if (_currentTopSpeed < MAX_ROTATION) {
            _currentTopSpeed += POWERUP_INCREMENT;
            _UIManager.UpdateTopSpeed(_currentTopSpeed);
        }
    }
}

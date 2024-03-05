using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using static Constants;

public class SpikeBall : MonoBehaviour
{
    private float _deceleration;
    private Vector3 _direction;
    private Vector3 _lastPOS;
    public bool _releaseTriggered;
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _releaseTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_releaseTriggered) {
            _deceleration = 1f;
            _releaseTriggered = false;
        }
        if (!_playerHoldingBall) {
            freeBallMovement();
            decayBallSpeed();
        }
        if (transform.position.x < MIN_X) {
            if (_direction.x < 0) {
                _direction = new Vector3(_direction.x * -1, _direction.y, 0);
            }
        }
        if (transform.position.x > MAX_X) {
            if (_direction.x > 0) {
                _direction = new Vector3(_direction.x * -1, _direction.y, 0);
            }
        }
        if (transform.position.y < MIN_Y) {
            if (_direction.y < 0) {
                _direction = new Vector3(_direction.x, _direction.y * -1, 0);
            }
        }
        if (transform.position.y > MAX_Y) {
            if (_direction.y > 0) {
                _direction = new Vector3(_direction.x, _direction.y * -1, 0);
            }
        }
    }


    // OnCollisionEnter2D() bounces the ball off an object
    // Pre: ball collides with an object
    // Post: ball bounces off in a new direction
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("Ball has triggwered event");
        }
        //If (other is edge or enemy) {
            //Calculate the angle of incidence
            //Set _direction based on angle of reflection
        //}
    }


    // setReleaseTradjectory() 
    // TODO: Write this function, also start tracking the _lastPOS
    public void setReleaseTradjectory(float degrees) {
        Vector3 tempVector = sinCos(degrees * -1);
        _direction = new Vector3(-1 * tempVector.y, tempVector.x, 0).normalized;
    }


    // freeBallMovement() moves the ball across a vector 
    // Pre: player is not holding ball
    // Post: spikeBall moves around without being leashed to player
    void freeBallMovement() {
        transform.Translate(_direction * _deceleration * _speed * Time.deltaTime);
    }

    // decayBallSpeed() reduces the speed of the ball as it travels
    // Pre: 
    // Post: 
    void decayBallSpeed() {
        _deceleration *= .99f;
    }
}

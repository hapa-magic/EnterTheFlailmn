using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class SpikeBall : MonoBehaviour
{
    private float _speed;
    private Vector3 _direction;
    private Vector3 _lastPOS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lastPOS = transform.position;
        if (!_playerHoldingBall) {
            freeBallMovement();
            decayBallSpeed();
        }
    }


    // OnCollisionEnter2D() bounces the ball off an object
    // Pre: ball collides with an object
    // Post: ball bounces off in a new direction
    void OnCollisionEnter2D(Collision2D other) {
        //If (other is edge or enemy) {
            //Calculate the angle of incidence
            //Set _direction based on angle of reflection
        //}
    }


    // setReleaseTradjectory() 
    // TODO: Write this function, also start tracking the _lastPOS
    void setReleaseTradjectory() {}

    // setDirection()

    // freeBallMovement() moves the ball across a vector 
    // Pre: player is not holding ball
    // Post: spikeBall moves around without being leashed to player
    void freeBallMovement() {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    // decayBallSpeed() reduces the speed of the ball as it travels
    // Pre: 
    // Post: 
    void decayBallSpeed() {
        //_speed *= (float)Math.Pow(.99f, Time.deltaTime);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants 
{
    public const float MIN_X = -8.55f;
    public const float MAX_X = 8.55f;
    public const float MIN_Y = -5.7f;
    public const float MAX_Y = 5.7f;
    public const double MIN_BALL_LEASH = 1.5;
    public const double MAX_BALL_LEASH = 2.5f;
    public const double MIN_ROTATION = 1;
    public const double MAX_ROTATION = 5;
    public const KeyCode ROTATE_KEY_1 = KeyCode.M;
    public const KeyCode ROTATE_KEY_2 = KeyCode.N;
    public const KeyCode RESTART_GAME_BUTTON = KeyCode.R;
    public const KeyCode QUIT_GAME = KeyCode.Escape;
    public const int INIT_ROTATION_SPEED = 1;
    public const float INCREASE_ROTATION = 0.2f;
    public const float POWERUP_INCREMENT = 0.5f;
    public const KeyCode RELEASE_BALL = KeyCode.Z;
    public static bool _playerHoldingBall = true;
    public const float STARTING_ROTATION_SPEED = 1.0f;
    public static bool _gameIsActive = true;


    // passBallToObjects() changes the parent of an object and sets the launch vector
    public static void passBallToObjects(this Transform child, Vector3 oldPOS, Transform oldParent, Transform newParent) {
        child.parent = newParent;
        Vector2 sinAdjustment = sinCos(oldParent.rotation.eulerAngles.z);
        child.localPosition = oldPOS + new Vector3(sinAdjustment.x * -1, sinAdjustment.y, 0) * (float)MIN_BALL_LEASH;
        child.gameObject.GetComponent<SpikeBall>().setReleaseTradjectory(oldParent.rotation.eulerAngles.z);
        if (child.GetComponent<SpriteRenderer>().sprite = child.GetComponent<SpikeBall>()._initSprite) {
            child.GetComponent<SpriteRenderer>().sprite = child.GetComponent<SpikeBall>()._greenBallSprite;
        }
        else {
            child.GetComponent<SpriteRenderer>().sprite = child.GetComponent<SpikeBall>()._glowGreenBallSprite;
        }
        child.localRotation = Quaternion.identity;
        child.localScale = new Vector3(1.5f, 1.5f, 1);
    }
    //public static void passBallToPlayer() {

    public static Vector3 sinCos(float degree) {
        return new Vector3(Mathf.Sin(degree * Mathf.Deg2Rad), Mathf.Cos(degree * Mathf.Deg2Rad), 0);
    }
}

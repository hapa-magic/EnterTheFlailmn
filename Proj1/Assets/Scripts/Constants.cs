using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants 
{
    public const float MIN_X = -3.7f;
    public const float MAX_X = 3.7f;
    public const float MIN_Y = -3.05f;
    public const float MAX_Y = 2.6f;
    public const double MIN_BALL_LEASH = 0.75;
    public const double MAX_BALL_LEASH = 2.5;
    public const double MIN_ROTATION = 1;
    public const double MAX_ROTATION = 5;
    public const KeyCode JUMP_BUTTON = KeyCode.Space;
    public const KeyCode ROTATE_KEY_1 = KeyCode.M;
    public const KeyCode ROTATE_KEY_2 = KeyCode.N;
    public const KeyCode RESTART_GAME_BUTTON = KeyCode.R;
    public const KeyCode QUIT_GAME = KeyCode.Escape;
    public const int INIT_ROTATION_SPEED = 1;
    public const float INCREASE_ROTATION = 0.2f;
    public const float POWERUP_INCREMENT = 0.5f;
    public const KeyCode RELEASE_BALL = KeyCode.Z;
    public static bool _playerHoldingBall = true;


    // passBallToObjects() changes the parent of an object and sets the launch vector
    public static void passBallToObjects(this Transform child, Vector3 oldPOS, Transform oldParent, Transform newParent) {
        child.parent = newParent;
        child.localPosition = oldPOS + sinCos(oldParent.rotation.eulerAngles.z * -1) * (float)MIN_BALL_LEASH;
        child.gameObject.GetComponent<SpikeBall>().setReleaseTradjectory(oldParent.rotation.eulerAngles.z);
        child.localRotation = Quaternion.identity;
        child.localScale = new Vector3(0.75f, 0.75f, 1);
    }
    //public static void passBallToPlayer() {

    public static Vector3 sinCos(float degree) {
        return new Vector3(Mathf.Sin(degree / 180.0f * 3.14f), Mathf.Cos(degree / 180.0f * 3.14f), 0);
    }
}

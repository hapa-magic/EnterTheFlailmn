using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _enemyJumpSpeed = 1.0f;
    [SerializeField] private float _jumpTime = 3f;
    [SerializeField] private float _initVelocity = 1.5f;
    private Transform _enemySprite;
    private Transform _enemyShadow;
    private Animator _anim;
    private BoxCollider2D _collider;
    private GameObject _playerGO;
    private Transform _player;
    private AudioSource _audioSource;
    private bool _canDamage;
    private bool _alwaysOn;
    

    // Start is called before the first frame update
    void Start()
    {
        //_anim = GetComponent<Animator>();
        //_collider = GetComponent<BoxCollider2D>();
        _playerGO = GameObject.Find("Player");
        _player = _playerGO.transform;
        //_audioSource = GetComponent<AudioSource>();
        _enemySprite = transform.GetChild(0);
        _enemyShadow = transform.GetChild(1);
        _canDamage = true;
        _alwaysOn = true;
        StartCoroutine(enemyMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }


    // enemyMovement() find a vector for the player and moves towards it
    // Pre: enemy exists
    // Post: enemy moves towards player
    IEnumerator enemyMovement() {
        while (_alwaysOn) {
            Vector3 whereToMove = _player.position - transform.position;
            Debug.Log(whereToMove);
            float savedTime = Time.time;
            while (Time.time - savedTime <= _jumpTime) {
                _canDamage = false;
                transform.Translate(whereToMove.normalized * _speed * Time.deltaTime);
                _enemySprite.transform.Translate(Vector3.up * Time.deltaTime * _enemyJumpSpeed);
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(1f);
            savedTime = Time.time;
            while (_enemySprite.position.y > _enemyShadow.position.y) {
                float accel = Mathf.Pow(_initVelocity, Time.time - savedTime);
                _enemySprite.transform.Translate(Vector3.down * accel * Time.deltaTime);
            }
            _canDamage = true;
            yield return new WaitForSeconds(2f);
        }
    }


}
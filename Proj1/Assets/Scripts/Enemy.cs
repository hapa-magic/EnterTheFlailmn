using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _enemyJumpSpeed;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _initVelocity = 1.5f;
    private Transform _enemySprite;
    private Transform _enemyShadow;
    private Animator _anim;
    private BoxCollider2D _collider;
    private GameObject _playerGO;
    private Transform _player;
    private AudioSource _audioSource;
    private bool _canDamage;
    private bool _isAlive;
    

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
        _isAlive = true;
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
        while (_isAlive) {
            Vector3 whereToMove = findTarget();
            float savedTime = Time.time;
            _canDamage = false;
            while (Time.time - savedTime <= _jumpTime) {
                transform.Translate(whereToMove.normalized * _speed * Time.deltaTime);
                _enemySprite.transform.Translate(Vector3.up * Time.deltaTime * _enemyJumpSpeed);
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(.3f);
            savedTime = Time.time;
            float savedX = transform.position.x;
            while (Time.time - savedTime <= .5f) {
                transform.position = new Vector3(savedX + Mathf.Sin((Time.time - savedTime) * 8 * Mathf.PI) * .2f, transform.position.y, 0);
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(.3f);
            float accel = 2f;
            while (_enemySprite.position.y > transform.position.y) {
                _enemySprite.transform.Translate(Vector3.down * accel * Time.deltaTime);
                if (_enemySprite.transform.position.y < transform.position.y) {
                    _enemySprite.position = transform.position;
                }
                accel *= 1.25f;
                yield return new WaitForSeconds(0.01f);
            }
            _canDamage = true;
            yield return new WaitForSeconds(1.5f);
        }
    }

    Vector3 findTarget() {
        Vector3 whereToMove = _player.position - transform.position;
        Debug.Log(whereToMove);
        return whereToMove;
    }

    public void damage() {
        if (_canDamage == true) {
            StartCoroutine(destroyEnemy());
        }
    }

    IEnumerator destroyEnemy() {
        for (int i = 0; i < 3; ++i) {
            _isAlive = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }

}
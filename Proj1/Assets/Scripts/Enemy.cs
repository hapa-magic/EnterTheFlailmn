using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _enemyJumpSpeed = 1.0f;
    [SerializeField] private float _jumpTime = 3f;
    [SerializeField] private float _initVelocity = 1.5f;
    private Transform _enemySprite;
    private Transform _enemyShadow;
    private Animator _anim;
    private BoxCollider2D _collider;
    private Transform _player;
    private AudioSource _audioSource;
    private bool _canDamage;
    private bool _alwaysOn;
    

    // Start is called before the first frame update
    void Start()
    {
        //_anim = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _player = GameObject.Find("Player").transform;
        Debug.Log(_player);
        //_audioSource = GetComponent<AudioSource>();
        _enemySprite = this.gameObject.transform.GetChild(0);
        _enemyShadow = this.gameObject.transform.GetChild(1);
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
            while (Time.time - savedTime >= _jumpTime) {
                _canDamage = false;
                transform.Translate(whereToMove * _speed * Time.deltaTime);
                _enemySprite.transform.Translate(Vector3.up * Time.deltaTime * _enemyJumpSpeed);
            }
            new WaitForSeconds(1f);
            savedTime = Time.time;
            while (_enemySprite.position.y > _enemyShadow.position.y) {
                float accel = Mathf.Pow(_initVelocity, Time.time - savedTime);
                _enemySprite.transform.Translate(Vector3.down * accel * Time.deltaTime);
            }
            _canDamage = true;
            new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);

    }
}
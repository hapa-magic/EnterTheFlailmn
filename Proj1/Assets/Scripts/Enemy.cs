using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Animator _anim;
    private BoxCollider2D _collider;
    private Player _player;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-7.0f, 7.0f);
            transform.position = new Vector3(randomX, 8.0f, 0);
        }
    }

}
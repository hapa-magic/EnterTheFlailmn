using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollision : MonoBehaviour
{
    [SerializeField] public GameObject _grandParent;
    private Player _playerParent;
    // Start is called before the first frame update
    void Start()
    {
        _playerParent = _grandParent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // OnCollisionEnter2D(Collider2D other) picks up the _spikeBall if it has been dropped or gets a power up
    // Pre: Player collides with object that has a collider
    // Post: player picks up ball or powers up
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("SpikeBall")) {
            _playerParent.pickUpBall();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PowerUp")) {
            _playerParent.powerUp();
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCollision : MonoBehaviour
{
    [SerializeField] public GameObject _grandParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // OnCollisionEnter2D(Collider2D other) picks up the _spikeBall if it has been dropped or gets a power up
    // Pre: Player collides with object that has a collider
    // Post: player picks up ball or powers up
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision detected!!!!!!!!!!!!!!!");
        if (other.gameObject.CompareTag("Enemy")) {
            _grandParent.GetComponent<Player>().damage(1);
        }
        else if (other.gameObject.CompareTag("SpikeBall")) {
            Debug.Log("Picked the ball up!");
            _grandParent.GetComponent<Player>().pickUpBall();
        }
        else if (other.gameObject.CompareTag("PowerUp")) {
            _grandParent.GetComponent<Player>().powerUp();
        }
    }
}

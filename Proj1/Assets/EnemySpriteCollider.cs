using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpriteCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (GetComponentInParent<Enemy>()._canDamage == true) {
                other.gameObject.GetComponentInParent<Player>().damage(1);
            }
        }
    }
}

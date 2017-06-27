using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    public float moveSpeed;
    public GameObject laserImpact;

    private void FixedUpdate() {
        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(laserImpact, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Destroy(gameObject);

        if (other.CompareTag("Enemy")) {
            GameManager.instance.KillEnemy(other.GetComponent<EnemyController>());
        } else if (other.CompareTag("Player")) {
            GameManager.instance.KillPlayer();
        } else if (other.CompareTag("Shield")) {
            GameManager.instance.ActivateShield(false);
        } else if (other.tag.StartsWith("Boss")) {
            BossManager.instance.DamagePart(other.gameObject);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

}

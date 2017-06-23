using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

    public PowerupType powerupType;
    public float moveSpeed;

    private void FixedUpdate() {
        transform.position = new Vector3(transform.position.x - moveSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (powerupType == PowerupType.Shield) {
                GameManager.instance.ActivateShield(true);
            } else if (powerupType == PowerupType.Speed) {
                GameManager.instance.ActivateSpeedPowerup(true);
            } else if (powerupType == PowerupType.DoubleShot) {
                GameManager.instance.ActivateDoubleShotPowerup(true);
            } else if (powerupType == PowerupType.ExtraLife) {
                GameManager.instance.AddLife();
            }
            Destroy(gameObject);
        }
    }
    
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

}

[System.Serializable]
public enum PowerupType { Shield, Speed, DoubleShot, ExtraLife };

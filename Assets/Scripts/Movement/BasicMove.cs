using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float moveSpeedX = 2f;
    public float moveSpeedY = 0f;
    public float rotateSpeed = 0f;

	protected Rigidbody2D rb2D;

	private void Start () {
		rb2D = GetComponent<Rigidbody2D>();	
	}
	
	private void FixedUpdate () {
		rb2D.velocity = new Vector2(-moveSpeedX, moveSpeedY);
		transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + Random.Range(rotateSpeed/2,rotateSpeed)  * Time.fixedDeltaTime); 
	}

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.instance.KillPlayer();
        }
        GetComponent<DestroyWithExplosion>().Explode();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Shield")) {
            other.gameObject.SetActive(false);
        }
        GetComponent<DestroyWithExplosion>().Explode();
    }
    
}

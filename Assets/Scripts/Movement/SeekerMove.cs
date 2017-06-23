using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMove : BasicMove {
    
    private Transform target;

    private void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        target = GameManager.instance.FindTarget("Player");
    }
	
	private void FixedUpdate () {
        float yDir = 1f;
        if (target != null) {
            yDir = transform.position.y <= target.position.y ? 1f : -1f;
        }
        float newYSpeed = moveSpeedY * yDir;

		rb2D.velocity = new Vector2(-moveSpeedX, newYSpeed);

        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + Random.Range(rotateSpeed / 2, rotateSpeed) * Time.fixedDeltaTime);
    }
}

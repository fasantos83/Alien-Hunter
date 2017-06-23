using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMove : BasicMove {

	public float turnDelay = 1f;
    
    private Boundary boundary;
    private float yDir = 1;
    private float turnCounter = 0f;

	private void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        boundary = GameManager.instance.boundary;
    }
	
	private void FixedUpdate () {
        //Countdown to turn to the other 'y' direction
        turnCounter -= Time.fixedDeltaTime;
        if(turnCounter <= 0) {
            yDir *= -1f;
            turnCounter = Random.Range(turnDelay/2, turnDelay*2);
        }

		rb2D.velocity = new Vector2(-moveSpeedX, moveSpeedY * yDir);
        float newY = Mathf.Clamp(transform.position.y, boundary.bottomRight.position.y, boundary.topLeft.position.y);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + Random.Range(rotateSpeed/2,rotateSpeed)  * Time.fixedDeltaTime); 
	}
    
}

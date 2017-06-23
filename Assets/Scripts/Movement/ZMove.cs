using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMove : BasicMove {

    private Transform target;
    private float yDir = 0f;
    private float yTarget = 0f;
    private bool changeSides = false;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        target = GameManager.instance.FindTarget("Player");
        yDir = transform.position.y > 0 ? -1f : 1f;
        yTarget = transform.position.y * -1f;
    }

    private void FixedUpdate () {
        float newYSpeed = 0f;
        if (ChangeSides()) {
            newYSpeed = moveSpeedY * yDir;
        }
        rb2D.velocity = new Vector2(-moveSpeedX, newYSpeed);

        float newY = Mathf.Clamp(transform.position.y, Mathf.Abs(yTarget) * -1f, Mathf.Abs(yTarget));
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + Random.Range(rotateSpeed / 2, rotateSpeed) * Time.fixedDeltaTime);
    }

    private bool ChangeSides() {
        if (!changeSides && transform.position.x - target.position.x <= 3){
            changeSides = true;
        }

        return changeSides;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}

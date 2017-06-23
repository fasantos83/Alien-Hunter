using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5f;
    public GameObject shield;

    private Animator anim;
	private Rigidbody2D rb2D;
	private Boundary boundary;

	private void Start () {
		rb2D = GetComponent<Rigidbody2D> ();	
		anim = GetComponent<Animator>();
        boundary = GameManager.instance.boundary;
	}
	
	private void FixedUpdate () {
		Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		rb2D.velocity = moveInput * speed;

		float newX = Mathf.Clamp(transform.position.x, boundary.topLeft.position.x, boundary.bottomRight.position.x);
		float newY = Mathf.Clamp(transform.position.y, boundary.bottomRight.position.y, boundary.topLeft.position.y);
		transform.position = new Vector3(newX, newY, transform.position.z);

		anim.SetFloat("Movement", moveInput.y);
	}

    public void ActivateShield(bool toggle) {
        shield.SetActive(toggle);
    }

    public void Explode() {
        gameObject.GetComponent<DestroyWithExplosion>().Explode();
        ActivateShield(false);
        GameManager.instance.ActivateSpeedPowerup(false);
        GameManager.instance.ActivateDoubleShotPowerup(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

    public GameObject laser;
    public Transform laserSpawn;
    public float fireDelay = 0.3f;
    protected float fireCounter = 0f;

    public Transform doubleShotSpawn1;
    public Transform doubleShotSpawn2;
    private bool doubleShot = false;

    private void Update() {
        if ((CanFire() && AttackButtonPressed()) || AttackButtonDown()) {
            if (doubleShot) {
                Instantiate(laser, doubleShotSpawn1.position, doubleShotSpawn1.rotation);
                Instantiate(laser, doubleShotSpawn2.position, doubleShotSpawn2.rotation);
            } else {
                Instantiate(laser, laserSpawn.position, laserSpawn.rotation);
            }
        }
    }

    private bool CanFire() {
        bool canFire = false;
        if (fireCounter > 0) {
            fireCounter -= Time.deltaTime;
        } else {
            fireCounter = fireDelay;
            canFire = true;
        }
        return canFire;
    }

    private bool AttackButtonPressed() {
        return (transform.CompareTag("Player") && Input.GetButton("Fire1")) || transform.CompareTag("Enemy");
    }

    private bool AttackButtonDown() {
        return (transform.CompareTag("Player") && Input.GetButtonDown("Fire1"));
    }

    public void ActivateDoubleShot(bool toggle) {
        doubleShot = toggle;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    public GameObject laser1;
    public GameObject laser2;

    public Transform laserSpawn1;
    public Transform laserSpawn2;
    public Transform laserSpawn3;
    public Transform laserSpawn4;

    public float fireDelay = 1f;
    protected float fireCounter1 = 0f;
    protected float fireCounter2 = 0f;

    public void Fire() {
        if (fireCounter1 > 0) {
            fireCounter1 -= Time.deltaTime;
        } else {
            fireCounter1 = fireDelay;
            Instantiate(laser1, laserSpawn1.position, laserSpawn1.rotation);
            Instantiate(laser1, laserSpawn4.position, laserSpawn4.rotation);
        }
    }

    public void Fire2() {
        if (fireCounter2 > 0) {
            fireCounter2 -= Time.deltaTime;
        } else {
            fireCounter2 = fireDelay;
            Instantiate(laser2, laserSpawn2.position, laserSpawn2.rotation);
            Instantiate(laser2, laserSpawn3.position, laserSpawn3.rotation);
        }
    }
}

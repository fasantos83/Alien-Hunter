using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public static BossManager instance;

    public Transform topBoundary;
    public Transform bottomBoundary;

    public GameObject boss;
    public float moveSpeedX;
    public float moveSpeedY;

    private int currentPhase = 0;

    private bool moveVerticaly = false;
    private bool moveUp = false;

    // Modify to array and check phases
    private int bossParts = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void FixedUpdate() {
        if (boss != null) {
            MoveVerticaly();
            switch (currentPhase) {
                case 0:
                    Vector3 target = new Vector3(topBoundary.position.x, boss.transform.position.y, boss.transform.position.z);
                    float speed = moveSpeedX * Time.fixedDeltaTime;
                    boss.transform.position = Vector3.MoveTowards(boss.transform.position, target, speed);

                    if (boss.transform.position.x == target.x) {
                        currentPhase++;
                        moveVerticaly = true;
                    }
                    break;
                case 1:
                    GetComponent<BossAttack>().Fire();
                    break;
                case 2:
                    GetComponent<BossAttack>().Fire();
                    GetComponent<BossAttack>().Fire2();
                    break;
            }
        }
    }

    public void DamagePart(GameObject bossPart) {
        if (currentPhase >= 1) {
            bossPart.GetComponent<BossPartController>().TakeDamage();
            if (bossParts == 1) {
                currentPhase++;
            }else if(bossParts == 0) {
                GameManager.instance.LevelComplete();
            }
        }
    }

    public void SetBossPart(int count) {
        bossParts += count;
    }

    public void MoveVerticaly() {
        if (moveVerticaly) {
            // Check if needs to change movement direction;
            if (boss.transform.position.y == topBoundary.position.y) {
                moveUp = false;
            } else if (boss.transform.position.y == bottomBoundary.position.y) {
                moveUp = true;
            }

            float yTarget = moveUp ? topBoundary.position.y : bottomBoundary.position.y;
            Vector3 target = new Vector3(boss.transform.position.x, yTarget, boss.transform.position.z);
            float speed = moveSpeedY * Time.fixedDeltaTime;
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, target, speed);
        }
    }
}

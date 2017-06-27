using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartController : MonoBehaviour {

    public int life;
    public GameObject bossPart;

    private void Start() {
        BossManager.instance.SetBossPart(1);
    }

    public void TakeDamage() {
        life--;
        if (life == 0) {
            GetComponent<DestroyWithExplosion>().Explode();
            BossManager.instance.SetBossPart(-1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int score = 0;

    public void Explode() {
        gameObject.GetComponent<DestroyWithExplosion>().Explode();
    }
}

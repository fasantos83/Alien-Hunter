using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithExplosion : MonoBehaviour {

    public ParticleSystem explosion;
    public Color colorMin;
    public Color colorMax;

    public void Explode() {
        Instantiate(explosion, transform.position, transform.rotation);
        ParticleSystem.MainModule explosionSettings = explosion.main;
        explosionSettings.startColor = new ParticleSystem.MinMaxGradient(colorMin, colorMax);
        explosion.Play();
        if (gameObject.CompareTag("Player")) {
            gameObject.SetActive(false);
        } else {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Boundary boundary;

    public Transform playerSpawnPoint;
    public Transform waveSpawnPoint;
    public GameObject[] waves;
    public float[] waveDelays;

    public GameObject[] powerups;
    public int powerupDropRate = 15;
    public float powerupDuration = 5f;
    public float speedPowerupMultiplier = 2f;

    public GameObject gameOverScreen;
    public GameObject[] lifeIcons;
    public int playerLives = 3;
    public int maxPlayerLives = 5;

    public Text scoreText;

    private int currentScore = 0;
    private bool spawnWave = true;
    private int currentWave = 0;
    private float timer = 0f;
    private float speedPowerupTimer = 0f;
    private float doubleShotPowerupTimer = 0f;

    private float playerSpeed = 0f;
    private PlayerController playerController;
    private BasicAttack playerBasicAttack;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
	}

    private void Start() {
        if(waveDelays.Length > 0) {
            timer = waveDelays[currentWave];
        }
        GameObject player = FindTarget("Player").gameObject;
        if (player != null) {
            playerController = player.GetComponent<PlayerController>();
            if(playerController != null) {
                playerSpeed = playerController.speed;
            }
            playerBasicAttack = player.GetComponent<BasicAttack>();
        }
        UpdateLifeDisplay();
        UpdateScoreText();
    }


    private void Update() {
        if (spawnWave) {            
            if(timer > 0) {
                timer -= Time.deltaTime;
            } else {
                Instantiate(waves[currentWave], waveSpawnPoint.position, waveSpawnPoint.rotation);
                currentWave++;
                if(currentWave >= waveDelays.Length) {
                    spawnWave = false;
                } else {
                    timer = waveDelays[currentWave];
                }
            }
        }

        if (speedPowerupTimer > 0) {
            speedPowerupTimer -= Time.deltaTime;
            if (speedPowerupTimer <= 0) {
                ActivateSpeedPowerup(false);
            }
        }

        if (doubleShotPowerupTimer > 0) {
            doubleShotPowerupTimer -= Time.deltaTime;
            if (doubleShotPowerupTimer <= 0) {
                ActivateDoubleShotPowerup(false);
            }
        }
    }

    public Transform FindTarget(string tag) {
        Transform target = null;
        GameObject player = GameObject.FindGameObjectWithTag(tag);
        if (player != null) {
            target = player.transform;
        } else {
            target = transform;
        }
        return target;
    }

    public void AddScore(int score) {
        currentScore += score;
        UpdateScoreText();
    }

    public void UpdateScoreText() {
        string text = currentScore.ToString();
        while (text.Length < 9) {
            text = "0" + text;
        }
        scoreText.text = text;
    }

    public void DropPowerup(Vector3 targetPosition) {
        int roll = Random.Range(0, 100);
        if(roll < powerupDropRate) {
            Instantiate(powerups[Random.Range(0, powerups.Length)], targetPosition, new Quaternion(0f, 0f, 0f, 0f));
        }
    }

    public void ActivateShield(bool toggle) {
        playerController.ActivateShield(toggle);
    }

    public void ActivateSpeedPowerup(bool toggle) {
        speedPowerupTimer = toggle ? powerupDuration : 0f;
        playerController.speed = playerSpeed * (toggle ? speedPowerupMultiplier : 1);
    }

    public void ActivateDoubleShotPowerup(bool toggle) {
        doubleShotPowerupTimer = toggle ? powerupDuration : 0f;
        playerBasicAttack.ActivateDoubleShot(toggle);
    }

    public void UpdateLifeDisplay() {
        for (int i = 0; i < maxPlayerLives; i++) {
            lifeIcons[i].SetActive(i < playerLives);
        }
    }

    public void AddLife() {
        if(playerLives < maxPlayerLives) {
            playerLives++;
            UpdateLifeDisplay();
        }
    }

    public void KillEnemy(EnemyController enemyController) {
        enemyController.Explode();
        AddScore(enemyController.score);
        DropPowerup(enemyController.transform.position);
    }
    
    public void KillPlayer() {
        playerLives--;
        UpdateLifeDisplay();
        playerController.Explode();

        if (playerLives > 0) {
            RespawnPlayer();
        } else {
            gameOverScreen.SetActive(true);
        }
    }

    public void RespawnPlayer() {
        playerController.transform.position = playerSpawnPoint.position;
        playerController.gameObject.SetActive(true);
        playerController.ActivateShield(true);
    }
}

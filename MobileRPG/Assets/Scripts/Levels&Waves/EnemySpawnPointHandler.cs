using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointHandler : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject spawnEnemy;
    public int currentWave;
    GameObject gameManager;
    GameObject currentWaveEnemiesHolder;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveEnemiesHolder = GameObject.Find("CurrentWaveEnemies");
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        // Check what the current wave is and assign the correct enemy that needs to be spawned
        currentWave = gameManager.GetComponent<LevelAndWaveHandler>().currentWave;
        if (gameManager != null) {
            if (currentWave == 1) {
                spawnEnemy = enemy1;
            } else if (currentWave == 2) {
                spawnEnemy = enemy2;
            } else if (currentWave == 3) {
                spawnEnemy = enemy3;
            }
        } else {
            Debug.Log(gameObject.name + " can not find GameManager");
        }
    }

    public void SpawnEnemy() {
        GameObject spawnedEnemy = Instantiate(spawnEnemy, transform.position,transform.rotation);
        spawnedEnemy.transform.parent = currentWaveEnemiesHolder.transform;
    }
}

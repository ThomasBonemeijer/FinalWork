using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectHandler : MonoBehaviour
{
    public Transform enemySpawnPointHolder;
    public List<GameObject> enemySpawnPoints;
    public bool waveIsActive = false;
    public Canvas spawnObjectCanvas;

    // Start is called before the first frame update
    void Start()
    {
        FillEnemySpawnPointsList();
    }

    // Update is called once per frame
    void Update()
    {
        // if (waveIsActive == true) {
        //     spawnObjectCanvas.enabled = false;
        // } else {
        //     if (Vector3.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position) < 5f) {
        //         spawnObjectCanvas.enabled = true;
        //     }
        // }
    }

    // fills the list with children from the enemy spawn point holder.
    void FillEnemySpawnPointsList() {
        foreach (Transform child in enemySpawnPointHolder) {
            enemySpawnPoints.Add(child.gameObject);
        }
    }

    // spawns the enemies by calling the "SpawnEnemy()" of each of the list objects
    public void SpawnEnemies() {
        if (waveIsActive == false) {
                foreach(GameObject enemy in enemySpawnPoints) {
                enemy.GetComponent<EnemySpawnPointHandler>().SpawnEnemy();
            }
        }
        waveIsActive = true;
    }
}

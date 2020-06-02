using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectHandler : MonoBehaviour
{
    public Transform enemySpawnPointHolder;
    public GameObject boss;
    public Transform bossSpawnLocation;
    public List<GameObject> enemySpawnPoints;
    public bool hasBeenActivated = false;
    public bool hasBeenPlaced = false;
    public bool waveIsActive;
    public bool hasBeenDefeated = false;
    public Canvas spawnObjectCanvas;
    public Image spawnItemImg;
    public Image spawnItemShadowImg;
    public Sprite spawnItemPlaceSprite;
    public Sprite spawnItemInactiveSprite;
    public Sprite spawnItemActiveSprite;
    public GameObject normalSprite;
    public GameObject defeatedSprite;
    private GameObject player;
    GameObject currentWaveEnemiesHolder;
    

    // Start is called before the first frame update
    void Start()
    {
        currentWaveEnemiesHolder = GameObject.Find("CurrentWaveEnemies");
        FillEnemySpawnPointsList();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetSpawnObjectArt();

        if(hasBeenActivated == false && hasBeenPlaced == false) {
            checkPlayer(transform.position, player.transform.position, 8f, player.GetComponent<PlayerResourceHandler>().hasWaveSpawnObject);
        }

        if(hasBeenPlaced == true) {
            SetSpawnItemArt();
        }

        checkIfWaveHasBeenCompleted();
    }

    void checkPlayer(Vector3 obj1, Vector3 obj2, float maxDist, bool hasObject) {
        if (player != null) {
            if (Vector3.Distance(obj1, obj2) < maxDist && hasObject == true) {
                spawnObjectCanvas.enabled = true;
                spawnItemImg.sprite = spawnItemPlaceSprite;
            } else {
                spawnObjectCanvas.enabled = false;
            }
        }
    }

    void SetSpawnItemArt() {
        if (hasBeenDefeated == false) {
            spawnItemImg .enabled = true;
            if (waveIsActive == false) {
                spawnItemImg.sprite = spawnItemInactiveSprite;
            } else {
                spawnItemImg.sprite = spawnItemActiveSprite;
            }
        } else {
            spawnItemImg.enabled = false;
        }
    }
    
    void SetSpawnObjectArt() {
        if (hasBeenDefeated == false) {
            normalSprite.SetActive(true);
            defeatedSprite.SetActive(false);
        } else {
            normalSprite.SetActive(false);
            defeatedSprite.SetActive(true);
        }
    }

    // fills the list with children from the enemy spawn point holder.
    void FillEnemySpawnPointsList() {
        foreach (Transform child in enemySpawnPointHolder) {
            enemySpawnPoints.Add(child.gameObject);
        }
    }

    // spawns the enemies by calling the "SpawnEnemy()" of each of the list objects
    public void SpawnEnemies() {
        hasBeenPlaced = true;
        if (waveIsActive == false) {
                foreach(GameObject enemy in enemySpawnPoints) {
                enemy.GetComponent<EnemySpawnPointHandler>().SpawnEnemy();
            }
        }
        waveIsActive = true;
    }

    public void SpawnBoss() {
        Instantiate(boss, bossSpawnLocation.position, Quaternion.identity);
        waveIsActive = true;
    }

    void checkIfWaveHasBeenCompleted() {
        if (waveIsActive == true && currentWaveEnemiesHolder.transform.childCount == 0) {
            waveIsActive = false;
            if (GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave < GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().bossWave) {
                GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave += 1;
                StartCoroutine(SpawnNextWave(3));
            } else {
                Debug.Log("Level Complete!");
                hasBeenDefeated = true;
            }
        }
    }

    IEnumerator SpawnNextWave(float time) {
     yield return new WaitForSeconds(time);
     if (GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave != GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().bossWave) {
         SpawnEnemies();
     } else {
         SpawnBoss();
     }
 }
}

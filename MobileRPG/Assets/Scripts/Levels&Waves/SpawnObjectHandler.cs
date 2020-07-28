using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectHandler : MonoBehaviour
{
    public Sprite completionSprite;
    public Canvas enterBtnCanvas;
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
    int currentWave;
    int bossWave;
    int completeionWave;
    bool hasSpawnedBoss = false;
    GameObject theSpawnedBoss;
    bool hasKilledBoss;
    float bossHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        // hasBeenDefeated = GameObject.Find ("Player").
        currentWaveEnemiesHolder = GameObject.Find("CurrentWaveEnemies");
        FillEnemySpawnPointsList();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetWaveInfo();
        if (hasKilledBoss == false) {
            CheckBossHealth();
        }
        SetSpawnObjectArt();

        if(hasBeenActivated == false && hasBeenPlaced == false) {
            checkPlayer(transform.position, player.transform.position, 8f, player.GetComponent<PlayerResourceHandler>().hasWaveSpawnObject);
        }

        if(hasBeenPlaced == true) {
            SetSpawnItemArt();
        }

        checkIfWaveHasBeenCompleted();
    }

    void SetWaveInfo() {
        var theGameManagerWaveScript = GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>();
        if (theGameManagerWaveScript != null) {
            currentWave = theGameManagerWaveScript.currentWave;
            bossWave = theGameManagerWaveScript.bossWave;
            completeionWave = theGameManagerWaveScript.completionWave;
        }
    }

    void CheckBossHealth () {
        if (hasSpawnedBoss == true) {
            if (theSpawnedBoss == null) {
                ShowUIText("Boss defeated!");
                hasBeenDefeated = true;
                GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Enter the cavern (Level 2)");
                hasKilledBoss = true;
            }
        }
    }

    void checkPlayer(Vector3 obj1, Vector3 obj2, float maxDist, bool hasObject) {
        if (player != null) {
            if (Vector3.Distance(obj1, obj2) < maxDist) {
                if (hasObject == true) {
                    spawnObjectCanvas.enabled = true;
                    spawnItemImg.sprite = spawnItemPlaceSprite;
                } else {
                    if (player.GetComponent<PlayerHandler>().currentObjective != "Find something to place on the altar") {
                        GameObject.Find("CurrenObjectiveHolder").GetComponent<ObjectiveHandler>().SetObjective("Find something to place on the altar");
                    }
                }
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
            enterBtnCanvas.enabled = true;
            spawnObjectCanvas.enabled = true;
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

    void checkIfWaveHasBeenCompleted() {
        if (waveIsActive == true && currentWaveEnemiesHolder.transform.childCount == 0) {
            waveIsActive = false;
            // if (currentWave < bossWave) {
            //     GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave += 1;
            //     StartCoroutine(SpawnNextWave(3));
            // }
            if (currentWave < bossWave) {
                StartCoroutine(SpawnNextWave(3));
            }
        }
    }

    IEnumerator SpawnNextWave(float time) {
        ShowUIText("Wave Complete!");
        GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave += 1;
        yield return new WaitForSeconds(time);
        if (currentWave < completeionWave) {
            if (currentWave != bossWave) {
                ShowUIText("Wave " + GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave + " has started!");
                SpawnEnemies();
            } else {
                ShowUIText("Wave " + GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().currentWave + ": BOSS WAVE has started! (good luck)");
                SpawnBoss();
            }
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
        if (waveIsActive == false) {
            theSpawnedBoss = Instantiate(boss, bossSpawnLocation.position, Quaternion.identity);
            hasSpawnedBoss = true;
        }
        waveIsActive = true;
    }

    void ShowUIText(string theText) {
        var theMainUI = GameObject.Find("MainUI").GetComponent<MainUIHandler>();
        if (theMainUI != null) {
            theMainUI.ShowScreenText(theText);
        } else {
            // Debug.LogError("Couldnt find the main ui component!");
        }
    }

    public void CompleteLevel() {
        GameObject.Find("GameManager").GetComponent<CanvasHandler>().hasCompletedLevel = true;
        GameObject.Find("GameManager").GetComponent<CanvasHandler>().OpenNotificationScreen(completionSprite);
    }
}

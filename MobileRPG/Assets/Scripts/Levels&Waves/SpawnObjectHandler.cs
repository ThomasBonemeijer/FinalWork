using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectHandler : MonoBehaviour
{
    public Transform enemySpawnPointHolder;
    public List<GameObject> enemySpawnPoints;
    public bool hasBeenActivated = false;
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
    

    // Start is called before the first frame update
    void Start()
    {
        FillEnemySpawnPointsList();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        setSpawnObjectArt();

        if(hasBeenActivated == false) {
            checkPlayer(transform.position, player.transform.position, 8f, player.GetComponent<PlayerResourceHandler>().hasWaveSpawnObject);
        }
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
    
    void setSpawnObjectArt() {
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
        if (waveIsActive == false) {
                foreach(GameObject enemy in enemySpawnPoints) {
                enemy.GetComponent<EnemySpawnPointHandler>().SpawnEnemy();
            }
        }
        waveIsActive = true;
    }
}

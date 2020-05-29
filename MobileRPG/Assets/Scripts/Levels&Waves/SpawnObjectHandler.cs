using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectHandler : MonoBehaviour
{
    public Transform enemySpawnPointHolder;
    public List<GameObject> enemySpawnPoints;
    public bool summonIsActive = false;
    public bool waveIsActive = false;
    public bool wavesHaveBeenCleared = false;
    public Canvas spawnObjectCanvas;
    public Image spawnItemImg;
    public Image spawnItemShadowImg;
    public Sprite spawnItemInactiveSprite;
    public Sprite spawnItemActiveSprite;
    public GameObject normalSprite;
    public GameObject defeatedSprite;
    

    // Start is called before the first frame update
    void Start()
    {
        FillEnemySpawnPointsList();
    }

    // Update is called once per frame
    void Update()
    {
        if (wavesHaveBeenCleared == false) {
            normalSprite.SetActive(true);
            defeatedSprite.SetActive(false);
            if (summonIsActive == true) {
                spawnObjectCanvas.enabled = true;
                if (waveIsActive == true) {
                    // spawnObjectCanvas.enabled = false;
                    spawnItemImg.sprite = spawnItemActiveSprite;
                } else {
                    spawnItemImg.sprite = spawnItemInactiveSprite;
                    // if (Vector3.Distance(GameObject.Find("Player").transform.position, gameObject.transform.position) < 5f) {
                    //     spawnObjectCanvas.enabled = true;
                    // }
                }
            } else {
                spawnObjectCanvas.enabled = false;
            }
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

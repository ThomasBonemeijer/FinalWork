using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatHandler : MonoBehaviour
{
    public bool isSkipTutorialBoat;
    public string sceneName;
    public BoxCollider2D boxCollider;
    public Canvas btnCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPos();
    }

    void CheckPlayerPos() {
        if (boxCollider.bounds.Contains(GameObject.Find("Player").transform.position)) {
            btnCanvas.enabled = true;
        } else {
            btnCanvas.enabled = false;
        }
    }

    public void SwitchScene() {
        if (sceneName != "") {
            if (SceneManager.GetActiveScene().name != "Tutorial") {
                GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
                GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().loadScene(sceneName);
            } else {
                if (isSkipTutorialBoat == true) {
                    
                } else {
                    GameObject.Find("Checkpoint").GetComponent<CheckPointHandler>().isActiveCheckpoint = false;
                    GameObject.Find("Player").GetComponent<PlayerHandler>().lastCheckPointPosition = new Vector3(0, 0, 0);
                    GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
                    GameObject.Find("GameManager").GetComponent<LevelAndWaveHandler>().loadScene(sceneName);
                }
            } 
        }
    }
}

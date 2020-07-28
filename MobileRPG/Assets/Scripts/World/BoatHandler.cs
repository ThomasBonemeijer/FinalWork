using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatHandler : MonoBehaviour
{
    public Sprite skipTutorialWarningNote;
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
            if (SceneManager.GetActiveScene().name != "Level1") {
                btnCanvas.enabled = true;
            }
        } else {
            btnCanvas.enabled = false;
        }
    }

    public void SwitchScene() {
        var genSettingsScript = GameObject.Find("GenSettings").GetComponent<GenSettingsScript>();
        if (sceneName != "") {
            if (SceneManager.GetActiveScene().name != "Tutorial") {
                genSettingsScript.LoadScene(sceneName);
            } else {
                if (isSkipTutorialBoat == true) {
                    var gameManagerScript = GameObject.Find("GameManager").GetComponent<CanvasHandler>();
                    gameManagerScript.OpenChoiceScreen(gameObject, skipTutorialWarningNote);
                } else {
                    GameObject.Find("Checkpoint").GetComponent<CheckPointHandler>().isActiveCheckpoint = false;
                    GameObject.Find("Player").GetComponent<PlayerHandler>().lastCheckPointPosition = new Vector3(0, 0, 0);
                    genSettingsScript.LoadScene(sceneName);
                }
            } 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAndWaveHandler : MonoBehaviour
{
    public string currentScene;
    public int currentWave;
    public int bossWave;
    public int completionWave;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        currentWave = GameObject.Find("Player").GetComponent<PlayerHandler>().currentWave;
        if (currentWave == 0) {
            currentWave = 1;
        }
        completionWave = bossWave +1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string scene) {
        GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
        SceneManager.LoadScene(scene);
    }

    public void ResetLevel() {
        currentWave = 0;
        // GameObject.Find("Player").GetComponent<PlayerHandler>().ResetPlayer();
        loadScene(currentScene);
    }

}

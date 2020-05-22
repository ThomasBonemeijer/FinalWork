using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAndWaveHandler : MonoBehaviour
{
    public string currentScene;
    public int currentWave;
    public GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}

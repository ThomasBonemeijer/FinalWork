using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnApplicationPause() {
    //     player.GetComponent<PlayerHandler>().SavePlayer();
    // }

    // void OnApplicationQuit() {
    //     player.GetComponent<PlayerHandler>().SavePlayer();
    // }

    public void SaveAndExit() {
        Time.timeScale = 1;
        player.GetComponent<PlayerHandler>().SavePlayer();
        Application.Quit();
    }

    public void ResetPlayer() {
        Time.timeScale = 1;
        player.GetComponent<PlayerHandler>().ResetPlayer();
    }
}

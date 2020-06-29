using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerHandler>().LoadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame() {
        player.GetComponent<PlayerHandler>().ResetPlayer();
        SceneManager.LoadScene("Tutorial");
    }

    public void Continue() {
        if (player.GetComponent<PlayerHandler>().currentLevel != "") {
            SceneManager.LoadScene(player.GetComponent<PlayerHandler>().currentLevel);
        } else {
            Debug.LogError("Player has no progression");
        }
    }
}

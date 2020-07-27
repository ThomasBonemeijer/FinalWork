using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public Color activeColor;
    public Color inactiveColor;
    // public Button NGBtn;
    public Button ConBtn;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerHandler>().LoadPlayer();
        Invoke("CheckPlayerInfo", .01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckPlayerInfo() {
        if (player != null) {
            if (player.GetComponent<PlayerHandler>().currentLevel != "Level1") {
                ConBtn.GetComponent<Image>().color = inactiveColor;
                ConBtn.enabled = false;
            } else {
                ConBtn.GetComponent<Image>().color = activeColor;
                ConBtn.enabled = true;
            }
        }
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

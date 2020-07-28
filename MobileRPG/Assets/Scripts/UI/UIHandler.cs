using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public Sprite saveAndExitWarningNote;
    public GameObject genSettings;
    public GameObject gameManager;
    public GameObject player;
    public Canvas deathUI;
    public Canvas MainUI;
    public Canvas BagUI;
    public GameObject BagUIGO;
    public Canvas NotificationUI;
    public Canvas ChoiceUI;
    public GameObject craftingSlot1;
    public GameObject craftingSlot2;
    public GameObject knifeLoadoutSlot;
    public GameObject gunLoadoutSlot;
    public Transform tempDragParent;
    public Joystick leftJoyStick;
    public Joystick rightJoyStick;
    public Image healthBarFill;
    public GameObject outputSlot;

    // Start is called before the first frame update
    void Start()
    {
        genSettings = GameObject.Find("GenSettings");
    }
    
    public void ClearTheInvSlots() {
        gameManager.GetComponent<CanvasHandler>().ClearInvSlots();
    }

    public void LoadMainMenu() {
        
        Time.timeScale = 1;
        // gameManager.GetComponent<LevelAndWaveHandler>().loadScene("MainMenu");
        GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
        genSettings.GetComponent<GenSettingsScript>().LoadScene("MainMenu");
    }
    
    public void SaveAndExitP1() {
        gameManager.GetComponent<CanvasHandler>().OpenChoiceScreen(gameObject, saveAndExitWarningNote);
    }

    public void SaveAndExit() {
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
        // Debug.Log("SavedAndExited!");
        gameManager.GetComponent<GameHandler>().SaveAndExit();
    }

    public void ResetThePlayer() {
        player.GetComponent<PlayerHandler>().SoftResetPlayer();
        gameManager.GetComponent<CanvasHandler>().HideDeathScreen();
    }
}

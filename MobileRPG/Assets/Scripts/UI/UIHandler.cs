using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player;
    public Canvas MainUI;
    public Canvas BagUI;
    public GameObject BagUIGO;
    public Canvas NotificationUI;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearTheInvSlots() {
        gameManager.GetComponent<CanvasHandler>().ClearInvSlots();
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        gameManager.GetComponent<LevelAndWaveHandler>().loadScene("MainMenu");
    }

    public void SaveAndExit() {
        Time.timeScale = 1;
        gameManager.GetComponent<GameHandler>().SaveAndExit();
    }
}

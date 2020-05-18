using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIHandler : MonoBehaviour
{
    public GameObject gameManager;
    public Canvas MainUI;
    public Canvas BagUI;
    public GameObject BagUIGO;
    public Canvas notificationScreen;
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
}

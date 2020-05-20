using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainUIHandler : MonoBehaviour
{
    public GameObject UIRoot;
    GameObject player;
    public GameObject ammoCountHolder;
    public GameObject fuelCountHolder;
    public Image ammoImage;
    public Image fuelImage;
    public TMP_Text ammoCountTxt;
    public TMP_Text fuelCountTxt;

    // Start is called before the first frame update
    void Start()
    {
        player = UIRoot.GetComponent<UIHandler>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            ammoCountTxt.text = "X" + player.GetComponent<PlayerResourceHandler>().ammoCount.ToString();
            fuelCountTxt.text = "X" + player.GetComponent<PlayerResourceHandler>().fuelCount.ToString();
        } else {
            Debug.LogError("MainUIHandler script cant find player");
        }
    }

    public void TogglePlayerLantern() {
        player.GetComponent<PlayerHandler>().toggelLantern();
    }
}

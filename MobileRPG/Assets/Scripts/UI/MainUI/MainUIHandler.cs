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
    public Image playerLivesImage;
    public Image ammoImage;
    public Image fuelImage;
    public TMP_Text playerLivesTxt;
    public TMP_Text ammoCountTxt;
    public TMP_Text fuelCountTxt;
    public Sprite lanternOnImg;
    public Sprite lanternOffImg;
    public Sprite playerHead;
    public Sprite playerSkull;

    // Start is called before the first frame update
    void Start()
    {
        player = UIRoot.GetComponent<UIHandler>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            ammoCountTxt.text = player.GetComponent<PlayerResourceHandler>().ammoCount.ToString();
            fuelCountTxt.text = player.GetComponent<PlayerResourceHandler>().fuelCount.ToString();
            if (player.GetComponent<PlayerHandler>().lives < 0) {
                playerLivesTxt.text = "0";
            } else {
                playerLivesTxt.text = player.GetComponent<PlayerHandler>().lives.ToString();
            }
            if(player.GetComponent<PlayerHandler>().lanternIsOn == true) {
                fuelImage.sprite = lanternOnImg;
            } else {
                fuelImage.sprite = lanternOffImg;
            }
            if (player.GetComponent<PlayerHandler>().lives < 0) {
                playerLivesImage.sprite = playerSkull;
            } else {
                playerLivesImage.sprite = playerHead;
            }
        } else {
            Debug.LogError("MainUIHandler script cant find player");
        }
    }

    public void TogglePlayerLantern() {
        player.GetComponent<PlayerHandler>().toggelLantern();
    }
}

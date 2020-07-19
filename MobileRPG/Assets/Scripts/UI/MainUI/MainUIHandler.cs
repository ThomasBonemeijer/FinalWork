using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class MainUIHandler : MonoBehaviour
{
    public GameObject UIRoot;
    GameObject player;
    int ammountOfHealthPotions;
    public GameObject ammoCountHolder;
    public GameObject fuelCountHolder;
    public Image healPotionImage;
    public TMP_Text healPotionCountTxt;
    public Image playerLivesImage;
    CinemachineVirtualCamera theCamera;
    public Image ammoImage;
    public Image fuelImage;
    public TMP_Text playerLivesTxt;
    public TMP_Text ammoCountTxt;
    public TMP_Text fuelCountTxt;
    public Sprite lanternOnImg;
    public Sprite lanternOffImg;
    public Sprite playerHead;
    public Sprite playerSkull;
    public int currentCamSetting;
    public List<int> cameraSettings;
    public Image camBtnImage;
    public List<Sprite> camIconsImages;
    public TMP_Text screenTextTxt;
    public Animator screenTextAnimator;

    // Start is called before the first frame update
    void Start()
    {
        theCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        player = UIRoot.GetComponent<UIHandler>().player;
        currentCamSetting = 1;
        ChangeCamBtnIcon();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            ammountOfHealthPotions = player.GetComponent<PlayerHandler>().healthPotionsCount;
            healPotionCountTxt.text = "X" + ammountOfHealthPotions.ToString();
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
            if (player.GetComponent<PlayerHandler>().healthPotionsCount > 0) {
                healPotionImage.enabled = true;
                healPotionCountTxt.enabled = true;
            } else {
                healPotionImage.enabled = false;
                healPotionCountTxt.enabled = false;
            }
        } else {
            Debug.LogError("MainUIHandler script cant find player");
        }
    }

    public void TogglePlayerLantern() {
        player.GetComponent<PlayerHandler>().toggelLantern();
    }

    public void UsePotion() {
        Debug.Log("Potion used!");
            if(player.GetComponent<PlayerHandler>().health < player.GetComponent<PlayerHandler>().maxHealth) {
                for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i].name == "HealthPotion") {
                    Inventory.instance.Remove(Inventory.instance.items[i]);
                    player.GetComponent<PlayerHandler>().HealPlayer(false, 0, 50, "HealthPotion");
                    return;
                }
            }
        } else {
            Debug.Log("Player is already at full health");
        }
    }

    public void SetCamera() {
        ShowScreenText("CAM CHANGED!");
        if (theCamera != null) {
            if (currentCamSetting < 2) {
                currentCamSetting += 1;
                ChangeCamBtnIcon();
            } else {
                currentCamSetting = 0;
                ChangeCamBtnIcon();
            }
        }
    }

    void ChangeCamBtnIcon() {
        theCamera.m_Lens.OrthographicSize = cameraSettings[currentCamSetting];
        Debug.Log(currentCamSetting); 
        if (currentCamSetting == 0) {
            camBtnImage.sprite = camIconsImages[0];
        } else if (currentCamSetting == 1) {
            camBtnImage.sprite = camIconsImages[1];
        } else if (currentCamSetting == 2) {
            camBtnImage.sprite = camIconsImages[2];
        }
    }

    public void ShowScreenText(string theText) {
        screenTextTxt.text = theText;
        if (screenTextAnimator != null) {
            screenTextAnimator.SetTrigger("ShowText");
        }
    }
}

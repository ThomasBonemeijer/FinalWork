﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerHandler : MonoBehaviour
{
    public string currentLevel;
    public int currentWave;
    public Vector3 spawnPoint;
    public int health;
    public string currentWeapon = "none";
    public bool lanternIsOn = false;
    public List<string> playerInventoryList;
    public GameObject gameManager;
    public Sprite mainHandFrontDefault;
    public Sprite offHandFrontDefault;
    public Sprite mainHandBackDefault;
    public Sprite offHandBackDefault;
    public GameObject mainHandFront;
    public GameObject offHandFront;
    public GameObject mainHandBack;
    public GameObject offHandBack;
    public bool hasFuel = false;
    public List<Sprite> playerItems;
    public List<GameObject> weapons;
    Joystick rightJoystick;
    public GameObject attackPoint;
    public List<GameObject> ammo;
    Image healthBarFill;
    public GameObject attackHand;
    public GameObject hitLight;
    public GameObject shootEffect;

    // Start is called before the first frame update
    void Start()
    {   
        LoadPlayer();
        rightJoystick = GameObject.Find("UI").GetComponent<UIHandler>().rightJoyStick;
        healthBarFill = GameObject.Find("UI").GetComponent<UIHandler>().healthBarFill;
        fillHands();
        InvokeRepeating("Attack", 0f, .5f);
        spawnPoint = GameObject.Find("PlayerSpawnPoint").transform.position;
        currentLevel = gameManager.GetComponent<LevelAndWaveHandler>().currentScene;
        transform.position = spawnPoint;
    }

    void Update() {
        RotateShootpoint();
        SetHealthAndStats();
        fillHands();
    }

    public void SetPlayerInv() {
        playerInventoryList.Clear();
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (playerInventoryList.Count <= Inventory.instance.space) {
                playerInventoryList.Add(Inventory.instance.items[i].name);
            } else {
                return;
            }
        }
    }

    public void SetGameHandlerInventory() {
        for (int i = 0; i < playerInventoryList.Count; i++) {
            var listOfItemsScript = gameManager.GetComponent<ListOfItems>();
            listOfItemsScript.setReturnItem(playerInventoryList[i]);
            Item ItemToAdd = listOfItemsScript.returnItem;
            Inventory.instance.Add(ItemToAdd);
        }
    }

    public void SetPlayerWeapon (string weapon) {
        currentWeapon = weapon;
        fillHands();
    }

    public void toggelLantern() {
        if(GetComponent<PlayerResourceHandler>().fuelCount > 0) {
            lanternIsOn = !lanternIsOn;
            Debug.Log("lantern has fuel current lantern status = " + lanternIsOn);
        } else {
            Debug.Log("lantern has no fuel and current lantern status = " + lanternIsOn);
        }
    }

    // changes what the player is holding
    public void fillHands() {
        var listOfItemsScript = gameManager.GetComponent<ListOfItems>();
        SpriteRenderer offHandFrontSprite = offHandFront.transform.GetComponent<SpriteRenderer>();
        SpriteRenderer offHandBackSprite = offHandBack.transform.GetComponent<SpriteRenderer>();

        if (GetComponent<PlayerResourceHandler>().fuelCount > 0) {
            hasFuel = true;
        } else {
            hasFuel = false;
        }

        // check if player has fuel for his lantern and handle accordingly
        if (hasFuel == true && lanternIsOn == true) {
            GetComponent<Light2D>().enabled = true;
            offHandFrontSprite.sprite = playerItems[0];
            offHandFrontSprite.sortingOrder = 21;

            offHandBackSprite.sprite = playerItems[1];
        } else {
            GetComponent<Light2D>().enabled = false;
            offHandFrontSprite.sprite = offHandFrontDefault;
            offHandFrontSprite.sortingOrder = 10;

            offHandBackSprite.sprite = offHandBackDefault;
        }

        // check what weapon the player is holding and handle accordingly
        if (currentWeapon == "knife") {
            foreach(GameObject weapon in weapons) {
                if (weapon.name.Contains("Knife")) {
                    weapon.SetActive(true);
                    weapon.GetComponent<SpriteRenderer>().enabled = true;
                } else {
                    weapon.GetComponent<SpriteRenderer>().enabled = false;
                    weapon.SetActive(false);
                }
            }
        } else if (currentWeapon == "gun") {
            foreach(GameObject weapon in weapons) {
                if (weapon.name.Contains("Gun")) {
                    weapon.SetActive(true);
                    weapon.GetComponent<SpriteRenderer>().enabled = true;
                } else {
                    weapon.GetComponent<SpriteRenderer>().enabled = false;
                    weapon.SetActive(false);
                }
            }
        } else if (currentWeapon == "none") {
            foreach(GameObject weapon in weapons) {
                weapon.SetActive(false);
            }
        }
    }

    public void RotateShootpoint() {
        // player facing up
        if (rightJoystick.Vertical > 0) {
            if (currentWeapon == "knife") {
                attackPoint.transform.localPosition = new Vector3(0.57f, -0.14f, 0f);
            } else if (currentWeapon == "gun") {
                attackPoint.transform.localPosition = new Vector3(0.69f, 0.02f, 0f);
            }
            attackHand.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
        // player facing down
        } else if (rightJoystick.Vertical < 0) {
            if (currentWeapon == "knife") {
                attackPoint.transform.localPosition = new Vector3(-0.63f, -0.47f, 0f);
            } else if (currentWeapon == "gun") {
                attackPoint.transform.localPosition = new Vector3(-0.64f, -1.17f, 0f);
            }
            attackHand.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 40;
        }

        if (rightJoystick.Direction == new Vector2(0, 0)) {
            attackPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }

        float angle = Mathf.Atan2(rightJoystick.Horizontal, rightJoystick.Vertical) * Mathf.Rad2Deg; 
        attackPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        
    }

    void Attack() {
        if (currentWeapon == "gun" && rightJoystick.Direction != new Vector2(0, 0) && GetComponent<PlayerResourceHandler>().ammoCount > 0){
            Instantiate(shootEffect, attackPoint.transform.position, attackPoint.transform.rotation);
            Instantiate(ammo[0], attackPoint.transform.position, attackPoint.transform.rotation);
            Debug.Log("Shoot!");
            GetComponent<PlayerResourceHandler>().ammoCount -= 1;
        } else if (currentWeapon == "knife" && rightJoystick.Direction != new Vector2(0, 0)) {
            attackHand.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void SetHealthAndStats() {
        healthBarFill.fillAmount = ((float)health/100);
    }

    public void takeDamage (int damage) {
        health -= damage;
        StartCoroutine(takeHit(0f, .2f));
    }

    IEnumerator takeHit(float time1, float time2)
    {
        yield return new WaitForSeconds(time1);
        hitLight.SetActive(true);
        yield return new WaitForSeconds(time2);
        hitLight.SetActive(false);
    }

    public void SavePlayer() {
        SetPlayerInv();
        SaveSystem.SavePlayer(this, GetComponent<PlayerResourceHandler>());
        Debug.Log("Player saved!");
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;
        currentLevel = data.currentLevel;
        currentWave = data.currentWave;

        GetComponent<PlayerResourceHandler>().fuelCount = data.fuelCount;
        GetComponent<PlayerResourceHandler>().ammoCount = data.ammoCount;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        playerInventoryList = data.playerInventoryList;
        SetGameHandlerInventory();

        Debug.Log("Player loaded!");
    }

    public void ResetPlayer() {
        health = 100;
        // level = 1;
        GetComponent<PlayerResourceHandler>().fuelCount = 0;
        GetComponent<PlayerResourceHandler>().ammoCount = 0;
        if (spawnPoint != null) {
            transform.position = spawnPoint;
        } else {
            Debug.LogError("No spawnpoint found");
            transform.position = new Vector3(0, 0, 0);
        }
        SavePlayer();
    }
}

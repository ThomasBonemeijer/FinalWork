using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public bool playerIsDead;
    public string currentLevel;
    public int currentWave;
    public Vector3 spawnPoint;
    public int maxHealth = 100;
    public int health;
    public int lives = 3;
    public Vector3 lastCheckPointPosition;
    public int healthPotionsCount;
    public Transform infoPoint;
    public GameObject damageCanvas;
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
    public bool hasLeftTablet = false;
    public bool hasRightTablet = false;

    // Start is called before the first frame update
    void Start()
    {   
        LoadPlayer();
        rightJoystick = GameObject.Find("UI").GetComponent<UIHandler>().rightJoyStick;
        healthBarFill = GameObject.Find("UI").GetComponent<UIHandler>().healthBarFill;
        InvokeRepeating("Attack", 0f, .5f);
        SetPlayerState();

        if (lastCheckPointPosition != new Vector3(0, 0, 0)) {
            transform.position = lastCheckPointPosition;
        } else {
            transform.position = spawnPoint;
        }
    }

    void Update() {
        if (playerIsDead != true) {
            RotateShootpoint();
            SetHealthAndStats();
            fillHands();
            SetPlayerState();
        }
    }

    public void CheckInventory() {
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            Debug.Log(Inventory.instance.items[i]);
            if (Inventory.instance.items[i].name == "LeftTabletHalf") {
                hasLeftTablet = true;
            }
            
            if (Inventory.instance.items[i].name == "RightTabletHalf") {
                hasRightTablet = true;
            }
        }
    }

    void SetPlayerState() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            spawnPoint = GameObject.Find("PlayerSpawnPoint").transform.position;
            currentLevel = gameManager.GetComponent<LevelAndWaveHandler>().currentScene;
            currentWave = gameManager.GetComponent<LevelAndWaveHandler>().currentWave;
        }
    }

    public void SetPlayerInv() {
        playerInventoryList.Clear();
        Debug.Log("Setting inv boii");
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
        if(hasFuel == true) {
            lanternIsOn = !lanternIsOn;
            Debug.Log("lantern has fuel current lantern status = " + lanternIsOn);
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
            lanternIsOn = false;
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

    public void SetInventoryValues() {
        healthPotionsCount = 0;
        // foreach(string item in playerInventoryList) {
        //     if (item == "HealthPotion") {
        //         healthPotionsCount += 1;
        //     }
        // }
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (Inventory.instance.items[i].name == "HealthPotion") {
                healthPotionsCount += 1;
            }
        }
    }

    public void takeDamage (int damage) {
        if (playerIsDead != true) {
            var instantiatedDamageCanvas = Instantiate(damageCanvas, infoPoint.transform.position, Quaternion.identity);
            instantiatedDamageCanvas.GetComponent<DamageInfoCanvas>().ShowDamageNumbers(damage);
            if (health <= 0) {
                PlayerHasDied();
            } else {
                health -= damage;
                StartCoroutine(takeHit(0f, .2f));
            }
        }
    }

    IEnumerator takeHit(float time1, float time2)
    {
        yield return new WaitForSeconds(time1);
        hitLight.SetActive(true);
        yield return new WaitForSeconds(time2);
        hitLight.SetActive(false);
    }

    public void SavePlayer() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            SetPlayerInv();
        }
        SaveSystem.SavePlayer(this, GetComponent<PlayerResourceHandler>());
        Debug.Log("Player saved!");
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        lives = data.lives;
        health = data.health;
        currentLevel = data.currentLevel;
        currentWave = data.currentWave;

        GetComponent<PlayerResourceHandler>().fuelCount = data.fuelCount;
        GetComponent<PlayerResourceHandler>().ammoCount = data.ammoCount;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        // transform.position = position;

        Vector3 theLastCheckPointPosition;
        theLastCheckPointPosition.x = data.lastCheckPointPosition[0];
        theLastCheckPointPosition.y = data.lastCheckPointPosition[1];
        theLastCheckPointPosition.z = data.lastCheckPointPosition[2];
        lastCheckPointPosition = theLastCheckPointPosition;

        if(SceneManager.GetActiveScene().name != "MainMenu") {
            playerInventoryList = data.playerInventoryList;
            SetGameHandlerInventory();
        }

        Debug.Log("Player loaded!");
    }

    public void ResetPlayer() {
        lives = 3;
        health = 100;
        transform.position = spawnPoint;
        lastCheckPointPosition = new Vector3(0, 0, 0);
        // level = 1;
        GetComponent<PlayerResourceHandler>().fuelCount = 0;
        GetComponent<PlayerResourceHandler>().ammoCount = 0;
        // clearInventoryInstance();
        playerInventoryList.Clear();
        if (spawnPoint != null) {
            transform.position = spawnPoint;
        } else {
            Debug.LogError("No spawnpoint found");
            transform.position = new Vector3(0, 0, 0);
        }
        Debug.Log("Player reset!");
        SavePlayer();
    }

    void clearInventoryInstance() {
        
    }

    public void SoftResetPlayer() {
        if (lives != 0) {
            lives -= 1;
            health = 100;
            if (lastCheckPointPosition != new Vector3(0, 0, 0)) {
                transform.position = lastCheckPointPosition;   
            } else {
                transform.position = spawnPoint;
            }
        } else {
            ResetPlayer();
        }
        playerIsDead = false;
    }

    void PlayerHasDied() {
        if (playerIsDead == false) {
            GetComponent<PlayerMovement>().PlayerDieAnimation();
            playerIsDead = true;
            StartCoroutine(PlayerHasDiedFollowip(0, 2));
        }
    }

    IEnumerator PlayerHasDiedFollowip(float time1, float time2)
    {
        yield return new WaitForSeconds(time1);
    
        gameManager.GetComponent<CanvasHandler>().CloseAllUI();

        yield return new WaitForSeconds(time2);
    
        gameManager.GetComponent<CanvasHandler>().ShowDeathScreen();
    }

    public void HealPlayer(bool isInInventory, int itemIndex, int ammount, string itemName) {
        if (health < maxHealth) {
            health += ammount;
            if(health > maxHealth) {
                health = maxHealth;
            }
            if (isInInventory == true) {
                Inventory.instance.RemoveByIndex(itemIndex);
            }
        } else {
            Debug.Log("Player is already at full health");
            return;
        }
    }
}

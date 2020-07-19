using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int lives;
    public string currentLevel;
    public int currentWave;
    public int health;
    public int fuelCount;
    public int ammoCount;
    public float[] position;
    public float[] lastCheckPointPosition;
    public List<string> openedChests;
    public bool hasOpenedStoneGate;

    public List<string> playerInventoryList;

    public PlayerData (PlayerHandler player, PlayerResourceHandler resources) {
        lives = player.lives;
        currentLevel = player.currentLevel;
        currentWave = player.currentWave;
        health = player.health;
        fuelCount = resources.fuelCount;
        ammoCount = resources.ammoCount;
        openedChests = resources.openedChestsList;
        hasOpenedStoneGate = resources.hasOpenedStoneGate;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        lastCheckPointPosition = new float[3];
        lastCheckPointPosition[0] = player.lastCheckPointPosition.x;
        lastCheckPointPosition[1] = player.lastCheckPointPosition.y;
        lastCheckPointPosition[2] = player.lastCheckPointPosition.z;

        playerInventoryList = player.playerInventoryList;
    }
}

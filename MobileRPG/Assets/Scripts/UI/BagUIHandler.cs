﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagUIHandler : MonoBehaviour
{
    public string currentInventory;
    public GameObject craftingPannel;
    public GameObject inventoryPannel;
    public GameObject settingsPannel;
    public GameObject loadoutInventory;
    public Button craftingBtn;
    public Button loadoutBtn;
    public Button settingsBtn;
    public Color normalColor;
    public Color dimmedColor;
    public Vector3 normalScale;
    public Vector3 smallScale;

    void Start() {
        CraftingInventory();
    }

    public void CraftingInventory() {
        currentInventory = "crafting";

        craftingBtn.GetComponent<Image>().color = normalColor;
        loadoutBtn.GetComponent<Image>().color = dimmedColor;
        settingsBtn.GetComponent<Image>().color = dimmedColor;

        craftingBtn.transform.localScale = normalScale;
        loadoutBtn.transform.localScale = smallScale;
        settingsBtn.transform.localScale = smallScale;

        craftingPannel.SetActive(true);
        inventoryPannel.SetActive(true);
        settingsPannel.SetActive(false);
        loadoutInventory.SetActive(false);
    }

    public void LoadoutInventory() {
        currentInventory = "loadout";

        craftingBtn.GetComponent<Image>().color = dimmedColor;
        loadoutBtn.GetComponent<Image>().color = normalColor;
        settingsBtn.GetComponent<Image>().color = dimmedColor;

        craftingBtn.transform.localScale = smallScale;
        loadoutBtn.transform.localScale = normalScale;
        settingsBtn.transform.localScale = smallScale;

        craftingPannel.SetActive(false);
        inventoryPannel.SetActive(true);
        settingsPannel.SetActive(false);
        loadoutInventory.SetActive(true);
    }

    public void Settings() {
        currentInventory = "settings";

        craftingBtn.GetComponent<Image>().color = dimmedColor;
        loadoutBtn.GetComponent<Image>().color = dimmedColor;
        settingsBtn.GetComponent<Image>().color = normalColor;

        craftingBtn.transform.localScale = smallScale;
        loadoutBtn.transform.localScale = smallScale;
        settingsBtn.transform.localScale = normalScale;

        craftingPannel.SetActive(false);
        inventoryPannel.SetActive(false);
        settingsPannel.SetActive(true);
        loadoutInventory.SetActive(false);
    }
}

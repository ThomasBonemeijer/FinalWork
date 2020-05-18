using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectHandler : MonoBehaviour
{
    public Button knifeButton;
    public Button gunButton;
    GameObject player;

    public void SetSelectedWeapon (string selectedWeapon) {
        player = GameObject.Find("Player");
        Debug.Log(selectedWeapon);
        player.GetComponent<PlayerHandler>().SetPlayerWeapon(selectedWeapon);
        ChangeButtonSize(selectedWeapon);
    }

    void ChangeButtonSize(string pressedButton) {
        if (pressedButton == "gun") {
            gunButton.GetComponent<RectTransform>( ).sizeDelta = new Vector2(200, 200);
            knifeButton.GetComponent<RectTransform>( ).sizeDelta = new Vector2(175, 175);
        } else if (pressedButton == "knife") {
            gunButton.GetComponent<RectTransform>( ).sizeDelta = new Vector2(175, 175);
            knifeButton.GetComponent<RectTransform>( ).sizeDelta = new Vector2(200, 200);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject mainUI;
    public TMP_Text objectiveTxt;
    public Animator objectiveTxtAnimator;
    GameObject objectiveTextGameObject;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (objectiveTxt != null) {
            objectiveTextGameObject = objectiveTxt.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        objectiveTxt.text = player.GetComponent<PlayerHandler>().currentObjective;
    }

    public void SetObjective(string theObjective) {

        if (player != null) {
            mainUI.GetComponent<MainUIHandler>().ShowScreenText("Objective Updated!");
            player.GetComponent<PlayerHandler>().currentObjective = theObjective;
            // objectiveTxtAnimator.SetTrigger("Show");
            objectiveTxtAnimator.SetBool("ShowTxt", true);
            player.GetComponent<PlayerHandler>().SavePlayer();
        }
    }

    public void ShowObjective() {
        if (objectiveTxtAnimator.GetBool("ShowTxt") != true) {
            objectiveTxtAnimator.SetBool("ShowTxt", true);
        }
    }
}

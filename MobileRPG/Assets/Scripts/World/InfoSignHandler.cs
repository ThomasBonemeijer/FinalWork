using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSignHandler : MonoBehaviour
{
    public Canvas btnCanvas;
    public CircleCollider2D theCol;
    public GameObject player;
    public Sprite infoSprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        checkIfPlayerIsInRadius();
    }

    void checkIfPlayerIsInRadius() {
        if (theCol != null) {
            if (theCol.bounds.Contains(player.transform.position)) {
                btnCanvas.gameObject.SetActive(true);
            } else {
                btnCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void ShowInfo() {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameObject != null) {
            gameManager.GetComponent<CanvasHandler>().OpenNotificationScreen(infoSprite);
        }
    }
}

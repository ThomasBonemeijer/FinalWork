using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPoint : MonoBehaviour
{
    public GameObject gameManager;
    public Sprite NotificationImage;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            gameManager.GetComponent<CanvasHandler>().OpenNotificationScreen(NotificationImage);
            Destroy(gameObject);
        }
    }
}

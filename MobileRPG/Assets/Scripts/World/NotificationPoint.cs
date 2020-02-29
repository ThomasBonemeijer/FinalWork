using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPoint : MonoBehaviour
{
    public GameObject gameManager;
    public string title;
    public string text;
    public Sprite image;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            gameManager.GetComponent<CanvasHandler>().OpenNotificationScreen(title, text, image);
            Destroy(gameObject);
        }
    }
}

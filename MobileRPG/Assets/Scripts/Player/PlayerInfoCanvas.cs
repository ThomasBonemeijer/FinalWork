using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoCanvas : MonoBehaviour
{
    public Canvas PCanvas;
    public Image PImage;
    public TMP_Text PText;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Item")) {
            PImage.sprite = col.GetComponent<SpriteRenderer>().sprite;
            animator.SetTrigger("Show");
        }
    }
}

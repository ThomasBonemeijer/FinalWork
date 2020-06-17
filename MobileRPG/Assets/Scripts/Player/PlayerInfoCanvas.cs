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

    public void showPickup(Sprite itemImage) {
        PImage.sprite = itemImage;
        animator.SetTrigger("Show");
    }
}

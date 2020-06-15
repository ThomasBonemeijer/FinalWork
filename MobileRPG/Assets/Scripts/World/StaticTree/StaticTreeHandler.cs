using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTreeHandler : MonoBehaviour
{
    Color originalColor;
    Color fadedColor;
    public GameObject rootParent;
    bool isABorderTree;
    
    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        fadedColor = originalColor;
        fadedColor.a = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        isABorderTree = rootParent.GetComponent<StaticTreeRootHandler>().isBorderTree;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player" && isABorderTree == false) {
            GetComponent<SpriteRenderer>().color = fadedColor;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }

    void SetSpriteLayer() {
        GetComponent<SpriteRenderer>().sortingOrder = (int) rootParent.transform.position.y;
    }
}

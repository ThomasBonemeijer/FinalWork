using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTreeStumpHandler : MonoBehaviour
{
    Collider2D theColider;
    GameObject player;
    public GameObject rootParent;
    bool isABorderTree;
    // Start is called before the first frame update
    void Start()
    {
        theColider = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        isABorderTree = rootParent.GetComponent<StaticTreeRootHandler>().isBorderTree;
        SetSpriteLayer();
    }

    void SetSpriteLayer() {
        if (player != null && isABorderTree == false) {
            if (player.GetComponent<CapsuleCollider2D>().bounds.max.y > theColider.bounds.max.y) {
                GetComponent<SpriteRenderer>().sortingLayerName = "PropsFront"; 
            } else {
                GetComponent<SpriteRenderer>().sortingLayerName = "PropsBack";
            } 
        }
        GetComponent<SpriteRenderer>().sortingOrder = (int) rootParent.transform.position.y;
    }
}

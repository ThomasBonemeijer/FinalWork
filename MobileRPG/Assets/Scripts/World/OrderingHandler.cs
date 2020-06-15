using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingHandler : MonoBehaviour
{
    public List <GameObject> spritesToBeOrdered;
    public Collider2D theColider;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetSpriteLayer();
    }

    void SetSpriteLayer() {
        if (player != null) {
            foreach (GameObject theSprite in spritesToBeOrdered) {
                if (player.GetComponent<CapsuleCollider2D>().bounds.max.y > theColider.bounds.max.y) {
                    theSprite.GetComponent<SpriteRenderer>().sortingLayerName = "PropsFront"; 
                } else {
                    theSprite.GetComponent<SpriteRenderer>().sortingLayerName = "PropsBack";
                } 
                theSprite.GetComponent<SpriteRenderer>().sortingOrder = (int) theSprite.transform.position.y;
            }
        }
    }
}

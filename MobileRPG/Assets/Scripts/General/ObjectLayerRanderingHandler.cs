using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLayerRanderingHandler : MonoBehaviour
{
    public List<GameObject> spriteList;
    public Collider2D theCollider;
    GameObject player;
    public float yPos;
    public Vector3 colPos;
    public float playerYPos;
    public string coliderType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (coliderType == "Box") {
            theCollider = GetComponent<BoxCollider2D>();
        } else if (coliderType == "Circle") {
            theCollider = GetComponent<CircleCollider2D>();
        } else if (coliderType == "Poly") {
            theCollider = GetComponent<PolygonCollider2D>();
        }  else if (coliderType == "Capsule") {
            theCollider = GetComponent<CapsuleCollider2D>();
        }
        // yPos = transform.position.y + theCollider.offset.y;
    }

    // Update is called once per frame
    void Update()
    {
        setOrderingLayers();
    }

    private void setOrderingLayers() {
        yPos = theCollider.bounds.center.y;
        playerYPos = player.transform.GetComponent<CapsuleCollider2D>().bounds.center.y;

        foreach (GameObject theSprite in spriteList) {
            int currentSpriteIndex = spriteList.IndexOf(theSprite);
            if (playerYPos > yPos) {
                theSprite.GetComponent<SpriteRenderer>().sortingLayerName = "PropsFront";
                if (spriteList.Count > 1) {
                    theSprite.GetComponent<SpriteRenderer>().sortingOrder = (int)(theSprite.transform.position.y + currentSpriteIndex);
                } else {
                    theSprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)(theSprite.transform.position.y  -currentSpriteIndex);
                }
            } else {
                theSprite.GetComponent<SpriteRenderer>().sortingLayerName = "PropsBack";
                if (spriteList.Count > 1) {
                    theSprite.GetComponent<SpriteRenderer>().sortingOrder = (int)(theSprite.transform.position.y + currentSpriteIndex);
                } else {
                    theSprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)(theSprite.transform.position.y  -currentSpriteIndex);
                }
            }
        }
    }
}

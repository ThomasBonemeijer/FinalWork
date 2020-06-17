using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingHandler : MonoBehaviour
{
    public List<GameObject> sprites;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetRenderLayer();
    }

    void SetRenderLayer() {
        foreach (GameObject theSprite in sprites) {
            if (theSprite.name.Contains("Eyes")) {
                theSprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y + 1;
            } else {
               theSprite.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
            }
        }
    }
}

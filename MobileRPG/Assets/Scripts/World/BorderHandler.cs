using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderHandler : MonoBehaviour
{
    public List<GameObject> vertBorders;
    public List<GameObject> horizontalBorders;

    // Start is called before the first frame update
    void Start()
    {
        // setBorderImageRenderLayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setBorderImageRenderLayers() {
        foreach(GameObject border in vertBorders) {
            foreach(Transform child in border.transform) {
                child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int) child.transform.position.y;
            }
        }

        foreach(GameObject border in horizontalBorders) {
            foreach(Transform child in border.transform) {
                child.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int) child.transform.position.x;
            }
        }
    }
}

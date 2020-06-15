using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTreeRootHandler : MonoBehaviour
{
    public bool isBorderTree = false;
    BoxCollider2D theColider;
    public bool isVertBorder;
    public BoxCollider2D vertCol;
    public BoxCollider2D horiCol;
    // Start is called before the first frame update
    void Start()
    {
        theColider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBorderTree == true) {
            if (isVertBorder == true) {
                SetColiderStats(vertCol);
            } else {
                SetColiderStats(horiCol);
            }
            theColider.enabled = true;
        } else {
            theColider.enabled = false;
        }
    }

    void SetColiderStats(BoxCollider2D col) {
        theColider.offset = col.offset;
        theColider.size = col.size;
    }
}
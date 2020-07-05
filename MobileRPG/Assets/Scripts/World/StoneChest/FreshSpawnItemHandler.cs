using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshSpawnItemHandler : MonoBehaviour
{
    public GameObject theItem;
    public SpriteRenderer childSprite;
    // Start is called before the first frame update
    void Start()
    {
        childSprite.sprite = theItem.GetComponent<SpriteRenderer>().sprite;
        Invoke("SpawnItem", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnItem() {
        Instantiate(theItem, childSprite.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

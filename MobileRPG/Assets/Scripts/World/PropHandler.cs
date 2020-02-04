using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHandler : MonoBehaviour
{
    Collider2D col;
    GameObject player;
    SpriteRenderer propSprite;
    public bool isResourceNode = false;
    public GameObject resource;
    public int remainingResources;
    public bool canSpawnResource = true;

    void Start() {
        player = GameObject.Find("Player");
        col = GetComponent<CapsuleCollider2D>();
        propSprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        SetSpriteLayer();
    }

    void SetSpriteLayer() {
        if (player != null && propSprite != null) {
            if (player.GetComponent<CapsuleCollider2D>().bounds.max.y > col.bounds.max.y) {
                propSprite.sortingLayerName = "PropsFront";
                
            } else {
                propSprite.sortingLayerName = "PropsBack";
                
            }
        }
    }

    public void spawnResource() {
        if (isResourceNode == true) {
            if (remainingResources > 0 && resource != null) {
                Instantiate(resource, transform.position, transform.rotation, GameObject.Find("Items").transform);
                remainingResources -= 1;
                if (remainingResources == 0) {
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (player.GetComponent<PlayerHandler>().currentWeapon == "knife" && col.gameObject.name == "Knife") {
            if (canSpawnResource == true) {
            transform.parent.GetComponent<Animator>().SetTrigger("Hit");
                spawnResource();
                canSpawnResource = false;
                StartCoroutine(CanSpawnAgain(.25f));
            }
        }
    }

    private IEnumerator CanSpawnAgain(float time) {
        if (canSpawnResource == false) {
            yield return new WaitForSeconds(time);
            canSpawnResource = true;
        }
    }
}

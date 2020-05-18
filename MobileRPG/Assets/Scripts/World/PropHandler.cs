using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHandler : MonoBehaviour
{
    public CapsuleCollider2D capsuleCol;
    public GameObject player;
    SpriteRenderer propSprite;
    public bool isResourceNode = false;
    public bool animatorIsOnParent = true;
    public GameObject resource;
    public int remainingResources;
    public bool canSpawnResource = true;
    public List<GameObject> propElements;

    void Start() {
        
    }

    void Update() {
        SetSpriteLayer();
    }

    void SetSpriteLayer() {
        if (player != null && propElements.Count > 0) {
            for (int i = 0; i < propElements.Count; i++) {
                if (player.GetComponent<CapsuleCollider2D>().bounds.max.y > capsuleCol.bounds.max.y) {
                    propElements[i].GetComponent<SpriteRenderer>().sortingLayerName = "PropsFront"; 
                } else {
                    propElements[i].GetComponent<SpriteRenderer>().sortingLayerName = "PropsBack";
                }
            }
        }
    }

    public void spawnResource() {
        if (isResourceNode == true) {
            if (remainingResources > 0 && resource != null) {
                // Instantiate(resource, transform.position, transform.rotation, GameObject.Find("Items").transform);
                remainingResources -= 1;
                if (remainingResources == 0) {
                    // Instantiate(resource, transform.position, transform.rotation, GameObject.Find("Items").transform);
                    Instantiate(resource, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (player.GetComponent<PlayerHandler>().currentWeapon == "knife" && col.gameObject.name == "Knife" && isResourceNode == true) {
            if (canSpawnResource == true) {
                if (animatorIsOnParent == true) {
                    transform.parent.GetComponent<Animator>().SetTrigger("Hit");
                } else if (animatorIsOnParent == false) {
                    transform.GetComponent<Animator>().SetTrigger("Hit");
                }
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

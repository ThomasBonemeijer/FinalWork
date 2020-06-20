using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTreeRootHandler : MonoBehaviour
{
    public bool isBorderTree = false;
    public GameObject spawnObject;
    public Transform objectSpawnPoint;
    BoxCollider2D theColider;
    public bool isVertBorder;
    public BoxCollider2D vertCol;
    public BoxCollider2D horiCol;
    public List<GameObject> fruitSpawns;
    int applesLeftToSpawn = 3;
    public GameObject theApple;
    public ParticleSystem leavesPFX;
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

    public void spawnApple() {
        Instantiate(leavesPFX, transform.position,Quaternion.identity);
        if (applesLeftToSpawn > 0 && isBorderTree == false) {
            var theAppleInstantiation = Instantiate(theApple, fruitSpawns[Random.Range(0, fruitSpawns.Count)].transform.position, Quaternion.identity);
            applesLeftToSpawn -= 1;
        } else if (applesLeftToSpawn <= 0 && isBorderTree == false) {
            if (spawnObject != null) {
                Instantiate (spawnObject, objectSpawnPoint.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
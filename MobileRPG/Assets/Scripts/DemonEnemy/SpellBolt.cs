using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBolt : MonoBehaviour
{
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        InvokeRepeating("DestroyThis", 1.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 8 * Time.deltaTime);
    }

    void DestroyThis() {
        Destroy(gameObject);
    }
}

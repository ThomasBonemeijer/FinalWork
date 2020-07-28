using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBolt : MonoBehaviour
{
    Transform playerPos;
    public ParticleSystem hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        Invoke("ResetObject", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 8 * Time.deltaTime);
    }

    void DestroyThis() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            HasHitTarget(col.gameObject);
        }
    }

    void HasHitTarget(GameObject target) {
        target.GetComponent<PlayerHandler>().takeDamage(25);
        if (hitEffect != null) {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        // Destroy(gameObject);
        ResetObject();
    }

    public void ResetObject() {
        transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }
}

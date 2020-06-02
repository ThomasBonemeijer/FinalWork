using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedSpellHandler : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D rb2D;
    bool hasBeenCast = false;
    public bool isMainFocusSpell;
    public GameObject theSubSpell;
    public List<Transform> subSpellsList;
    public float speed = 8;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (target == null) {
            target = GameObject.Find("Player");
        }
        if (isMainFocusSpell == true) {
            speed = 5;
            transform.right = target.transform.position - transform.position;
            InvokeRepeating("CastSubSpells", 3f, 3f);
        }
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.name == "Player") {
            Debug.Log("Focused spell hit player");
            if (isMainFocusSpell) {
                col.GetComponent<PlayerHandler>().takeDamage(25);
            } else {
                Destroy(gameObject);
                col.GetComponent<PlayerHandler>().takeDamage(10);
            }
        }
    }

    void CastSubSpells(){
        foreach (Transform subSpell in subSpellsList) {
            Debug.Log(subSpell.name);
            Instantiate(theSubSpell, subSpell.position, subSpell.rotation);
        }
    }
}

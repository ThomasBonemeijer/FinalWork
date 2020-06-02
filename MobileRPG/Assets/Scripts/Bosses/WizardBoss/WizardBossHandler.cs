using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossHandler : MonoBehaviour
{
    public float health = 400;
    public bool hasBeenAwoken = false;
    public GameObject focusedAttackPoint;
    public Animator bodyAnimator;
    int currentAttackCount = 0;
    public GameObject focusedSpell;
    // Start is called before the first frame update
    void Start()
    {
            InvokeRepeating ("WizardAttack", 2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WizardAttack() {
        if (hasBeenAwoken == true) {
            if (currentAttackCount != 2) {
                WizardFocusedAttack();
                currentAttackCount += 1;
            } else {
                WizardAOEAttack();
                currentAttackCount = 0;
            }
        }
    }

    void WizardFocusedAttack () {
        bodyAnimator.SetTrigger("FocusedAttack");
        Instantiate(focusedSpell, focusedAttackPoint.transform.position, Quaternion.identity);
    }

    void WizardAOEAttack () {
        bodyAnimator.SetTrigger("AOEAttack");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            if (hasBeenAwoken == false) {
                hasBeenAwoken = true;
            } else {
                Debug.Log("BulletHit!");
                Destroy(col.gameObject);
                health -= 25;
                if (health <= 0) {
                Destroy(gameObject);
            }
            }
        }
    }
}

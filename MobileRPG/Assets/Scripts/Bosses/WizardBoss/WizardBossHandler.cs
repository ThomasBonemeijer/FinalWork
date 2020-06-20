using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossHandler : MonoBehaviour
{
    public GameObject damageCanvas;
    public GameObject infoPoint;
    public float maxHealth = 400;
    public float health = 400;
    public bool hasBeenAwoken = false;
    public GameObject focusedAttackPoint;
    public Animator bodyAnimator;
    int currentAttackCount = 0;
    public GameObject focusedSpell;
    // Start is called before the first frame update
    void Start()
    {
            health = maxHealth;
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
                TakeDamage(25, col.gameObject, true);
            }
        } else if (col.CompareTag("Knife")) {
            TakeDamage(25, col.gameObject, false);
        }
    }

    private void TakeDamage(int damage, GameObject theCol, bool isBullet) {
        Debug.Log("BulletHit!");
        var instantiatedDamageCanvas = Instantiate(damageCanvas, infoPoint.transform.position, Quaternion.identity);
        instantiatedDamageCanvas.GetComponent<DamageInfoCanvas>().ShowDamageNumbers(damage);

        if (isBullet == true) {
            Destroy(theCol);
        }
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

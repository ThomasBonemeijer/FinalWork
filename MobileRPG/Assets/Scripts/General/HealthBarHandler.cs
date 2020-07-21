using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    public GameObject parentObject;
    public Slider slider;
    public int maxhealth;
    public float theHealth;
    public bool enemyHasAwakeState = false;
    bool enemyIsAwake = false;
    MonoBehaviour parentScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleHealthBar();
        DisplayHealthBar();
    }

    void HandleHealthBar() {
        if (parentObject.name.Contains("WizardBoss")) {
            maxhealth = (int) parentObject.GetComponent<WizardBossHandler>().maxHealth;
            slider.value = parentObject.GetComponent<WizardBossHandler>().health / maxhealth;
        } else if (parentObject.name.Contains("DemonEnemy1")) {
            maxhealth = (int) parentObject.GetComponent<EnemyMovementHandler>().maxHealth;
            slider.value = parentObject.GetComponent<EnemyMovementHandler>().health / maxhealth;
        } else if (parentObject.name.Contains("BigDemonEnemy")) {
            maxhealth = (int) parentObject.GetComponent<BigDemonHandler>().maxHealth;
            slider.value = parentObject.GetComponent<BigDemonHandler>().health / maxhealth;
        } else if (parentObject.name.Contains("BigSlugEnemy")) {
            maxhealth = (int) parentObject.GetComponent<BigSlug>().maxHealth;
            slider.value = parentObject.GetComponent<BigSlug>().health / maxhealth;
        }
        else if (parentObject.name.Contains("BushEnemy")) {
            maxhealth = (int) parentObject.GetComponent<BushEnemyHandler>().maxHealth;
            slider.value = parentObject.GetComponent<BushEnemyHandler>().health / maxhealth;
            enemyIsAwake = parentObject.GetComponent<BushEnemyHandler>().animator.GetBool("IsAwake");
            if (enemyIsAwake == false && parentObject.GetComponent<BushEnemyHandler>().hasBeenForceWokenUp == false) {
                parentObject.GetComponent<BushEnemyHandler>().ResetEnemyStats();
            }
        }
    }

    void DisplayHealthBar() {
        if (enemyHasAwakeState == true) {
            if (enemyIsAwake == true) {
                GetComponent<Canvas>().enabled = true;
            } else {
                GetComponent<Canvas>().enabled = false;
            }
        }
    }



}

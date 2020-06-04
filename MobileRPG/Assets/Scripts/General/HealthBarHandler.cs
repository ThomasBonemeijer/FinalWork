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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Parent object is wizard
        if (parentObject.name.Contains("WizardBoss")) {
            maxhealth = (int) parentObject.GetComponent<WizardBossHandler>().maxHealth;
            slider.value = parentObject.GetComponent<WizardBossHandler>().health / maxhealth;
        } else if (parentObject.name.Contains("DemonEnemy1")) {
            maxhealth = (int) parentObject.GetComponent<EnemyMovementHandler>().maxHealth;
            slider.value = parentObject.GetComponent<EnemyMovementHandler>().health / maxhealth;
        } else if (parentObject.name.Contains("BigSlugEnemy")) {
            maxhealth = (int) parentObject.GetComponent<BigSlug>().maxHealth;
            slider.value = parentObject.GetComponent<BigSlug>().health / maxhealth;
        }
    }

}

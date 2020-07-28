using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarkZoneHandler : MonoBehaviour
{   
    public bool lanternIsOn;
    public bool isInDarkzone;
    public List<GameObject> darkZones;
    GameObject darkZonesHolder;
    public GameObject worldLight;
    
    // Start is called before the first frame update
    void Start()
    {
        if (darkZonesHolder == null) {
            darkZonesHolder = GameObject.Find("DarkZonesHolder");
        }

        worldLight = GameObject.Find("WorldLight");

        fillDarkZonesList();

        InvokeRepeating("TakeDarkzoneDamage", 0f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInDarkzone();
        lanternIsOn = GetComponent<PlayerHandler>().lanternIsOn;

        if (isInDarkzone == true) {
            worldLight.SetActive(false);
        } else {
            worldLight.SetActive(true);
        }
    }

    void fillDarkZonesList() {
        if (darkZonesHolder != null) {
            foreach (Transform theDzone in darkZonesHolder.transform) {
                darkZones.Add(theDzone.gameObject);
            }
        } else {
            // Debug.LogError("No DarkZonesHolder GameObject in scene!");
        }
    }

    // void OnTriggerEnter2D (Collider2D col) {
    //     if (darkZones.Contains(col.gameObject)) {
    //         isInDarkzone = true;
    //     }
    // }

    // void OnTriggerExit2D (Collider2D col) {
    //     if (darkZones.Contains(col.gameObject)) {
    //         isInDarkzone = false;
    //     }
    // }

    public void TakeDarkzoneDamage() {
        if (isInDarkzone == true) {
            if (lanternIsOn == false) {
                GetComponent<PlayerHandler>().takeDamage(5);
            }
        }
    }

    void CheckIfInDarkzone() {
        foreach (Transform theDzone in darkZonesHolder.transform) {
            if (theDzone.gameObject.GetComponent<Collider2D>().bounds.Contains(transform.position)) {
                // Debug.Log("Player IS in darkzone!");
                isInDarkzone = true;
            } else {
                isInDarkzone = false;
            }
        }
    }
}

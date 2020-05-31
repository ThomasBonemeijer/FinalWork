using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceHandler : MonoBehaviour
{
    public int fuelCount;
    public int ammoCount;
    public bool hasWaveSpawnObject;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FuelTick", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FuelTick() {
        if(fuelCount > 0 && GetComponent<PlayerHandler>().lanternIsOn == true) {
            fuelCount -= 1;
        }
    }
}

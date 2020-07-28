using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public GameObject poolObjHolder;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.parent = poolObjHolder.transform;
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation) {
        if (!PoolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // var pooledObjectScript = objectToSpawn.GetComponent<SpellBolt>();

        // if (pooledObjectScript != null) {

        // }

        PoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsManager : MonoBehaviour
{
    #region MEMBERS

    [Header("Items list")]
    public List<Item> items = new List<Item>();

    [Header("Spawn Properties")]
    public float spawnOds = 0.2f;
    [Tooltip("in seconds")]
    public int spawnRate = 2;
    public bool spawnEnable = true; /*
    {
        get  { return spawnEnable; } 
        set  { StartCoroutine(SpawnNewItem()); } 
    }*/


    #endregion MEMBERS

    #region METHODS

    // Start is called before the first frame update
    void Start()
    {
        if (spawnEnable) { StartCoroutine(SpawnNewItem()); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnNewItem()
    {
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        Vector2 randomPos = randomVec2OnDisk(5);
        Instantiate(pickRandomItem(), new Vector3(randomPos.x, randomPos.y, 0f), quaternion.identity);

        yield return new WaitForSeconds(spawnRate);
        if(spawnEnable) { StartCoroutine(SpawnNewItem()); }

        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private Item pickRandomItem()
    {
        return items[Random.Range(0, items.Count)];
    }
    static Vector2 randomVec2OnDisk(float radius) {
        Vector2 vec = new Vector2(0f, 0f);
        do {
            vec.x = Random.Range(0.0f, radius);
            vec.y = Random.Range(0.0f, radius);
        } while (vec.magnitude > radius);

        return vec;
    }
    #endregion PRIVATE_METHODS

}

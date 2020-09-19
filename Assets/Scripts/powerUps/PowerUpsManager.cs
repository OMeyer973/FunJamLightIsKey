using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpsManager : MonoBehaviour
{
    #region MEMBERS

    [Header("Items list")]
    public List<PowerUp> availablePowerUps = new List<PowerUp>();

    [Header("Spawn Properties")]
    public float spawnOds = 0.2f;
    [Tooltip("in seconds")]
    public int spawnRate = 2;

    private bool spawnEnable = true;

    #endregion MEMBERS

    #region METHODS

    // Start is called before the first frame update
    void Start()
    {
        startSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void stopSpawn() { spawnEnable = false; }
    public void startSpawn()
    { 
        spawnEnable = true;
        StartCoroutine(SpawnNewPowerUp());
    }

    IEnumerator SpawnNewPowerUp()
    {
        Debug.Log("Started Coroutine(SpawnNewItem) at timestamp : " + Time.time);
        while (spawnEnable)
        {
            Vector2 randomPos = Random.insideUnitCircle * 5;
            Instantiate(pickRandomPowerUp(), new Vector3(randomPos.x, 0f, randomPos.y), quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
        Debug.Log("Finished Coroutine(SpawnNewItemp) at timestamp : " + Time.time);
    }

    #endregion METHODS

    #region PRIVATE_METHODS

    private PowerUp pickRandomPowerUp() { return availablePowerUps[Random.Range(0, availablePowerUps.Count)]; }


    #endregion PRIVATE_METHODS
}

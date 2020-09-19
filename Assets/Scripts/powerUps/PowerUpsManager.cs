using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class PowerUpsManager : MonoBehaviour
{
    #region MEMBERS

    [System.Serializable]
    public class PowerUpEntry
    {
        public PowerUp powerUp;
        public float spawnProbability = 1;
    }

    private List<float> normalizedProbabilities;

    [Header("Items list")]
    public List<PowerUpEntry> availablePowerUps = new List<PowerUpEntry>();

    [Header("Spawn Properties")]
    [Tooltip("in seconds")]
    public int spawnRate = 2;

    private bool spawnEnable = true;

    #endregion MEMBERS

    #region METHODS

    // Start is called before the first frame update
    void Start()
    {
        // create probabilities distribution
        normalizedProbabilities = availablePowerUps.Select(powerUpEntry => powerUpEntry.spawnProbability).ToList();
        float sum = normalizedProbabilities.Sum();
        normalizedProbabilities.ForEach(p => p /= sum);

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

    private PowerUp pickRandomPowerUp()
    {
        return availablePowerUps[DiscreteProbabilityDistribution(normalizedProbabilities)].powerUp;
    }

    private int DiscreteProbabilityDistribution(List<float> proba)
    {
        for (int i = 1; i < proba.Count; i++) { proba[i] += proba[i - 1]; }

        float u = Random.value;

        int j = 0;
        while (u > proba[j] && j < proba.Count - 1) { j++; }

        return j;
    }

    #endregion PRIVATE_METHODS
}

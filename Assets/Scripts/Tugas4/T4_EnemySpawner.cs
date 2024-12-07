using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public class T4_Enemy
{
    public string name;
    public GameObject prefab;
    [Range(0f, 100f)] public float spawnChance = 100f;
    [HideInInspector] public double _weight;
}

public class T4_EnemySpawner : MonoBehaviour
{
    [SerializeField] private T4_Enemy[] enemies;
    private double accumulatedWeight;
    private System.Random rand = new System.Random();
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();


    private void Awake()
    {
        CalculateWeights();
    }

    private void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            SpawnRandomEnemy(new Vector2(Random.Range(-8f, 8f), Random.Range(-3f, 3f)));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateWeights();
            foreach (GameObject enemy in spawnedEnemies)
            {
                enemy.SetActive(false);
            }

            spawnedEnemies.Clear();
            for (int i = 0; i < 200; i++)
            {
                SpawnRandomEnemy(new Vector2(Random.Range(-8f, 8f), Random.Range(-3f, 3f)));
            }
        }
    }

    private void SpawnRandomEnemy(Vector2 position)
    {
        T4_Enemy randomEnemy = enemies[GetRandomEnemyIndex()];
        // Instantiate(randomEnemy.prefab, position, Quaternion.identity, transform);
        GameObject enemy = Instantiate(randomEnemy.prefab, position, Quaternion.identity);
        spawnedEnemies.Add(enemy);
        Debug.Log("<color=" + randomEnemy.name + ">●</color> Chance: <b>" + randomEnemy.spawnChance + "</b>%");
    }

    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeight;
        for (int i = 0; i < enemies.Length; i++)
            if (enemies[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeights()
    {
        accumulatedWeight = 0f;
        foreach (T4_Enemy enemy in enemies)
        {
            accumulatedWeight += enemy.spawnChance;
            enemy._weight = accumulatedWeight;
        }
    }
}
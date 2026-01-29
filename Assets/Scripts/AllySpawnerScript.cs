using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class AllySpawnerScript : MonoBehaviour
{
    public static AllySpawnerScript instance;
    [Header("Ally")]
    public GameObject[] allyTypes;
    public Dictionary<GameObject, int> allyPrices;
    public Dictionary<GameObject, float> allyBuildTime;
    public float money;
    GameObject selectedTroop;
    public Queue<GameObject> buildingTroops;
    public float currentBuildTimer;

    [Header("Ennemy")]
    public GameObject enemy;
    public float enemySpawnRate;
    public float enemySpawnTimer;
    public float decayEachSpawn;
    public float spawnDistance;

    [Header("UI")]
    public TMP_Text moneyText;

    [Header("Life")]
    public int hp;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        allyPrices = new Dictionary<GameObject, int>();
        allyPrices.Add(allyTypes[0], 20);
        allyPrices.Add(allyTypes[1], 40);
        allyPrices.Add(allyTypes[2], 80);
        allyBuildTime = new Dictionary<GameObject, float>();
        allyBuildTime.Add(allyTypes[0], 0.5f);
        allyBuildTime.Add(allyTypes[1], 1f);
        allyBuildTime.Add(allyTypes[2], 3f);
        buildingTroops = new Queue<GameObject>();
    }

    void Update()
    {
        SpawnEnemy();
        if (buildingTroops.Count > 0)
        {
            currentBuildTimer -= Time.deltaTime;
            if (currentBuildTimer < 0)
            {
                GameObject instantiated = Instantiate(buildingTroops.Dequeue(), transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)), Quaternion.identity);
                if (buildingTroops.Count > 0)
                {
                    currentBuildTimer = allyBuildTime[buildingTroops.Peek()];
                }
            }
        }
        moneyText.text = "money : " + money.ToString();
    }
    void SpawnEnemy()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnRate)
        {
            Instantiate(enemy, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * spawnDistance, Quaternion.identity);
            enemySpawnRate -= decayEachSpawn;
            enemySpawnTimer = 0;
        }
    }
    public void SpawnPaysan()
    {
        selectedTroop = allyTypes[0];
        if (allyPrices[selectedTroop] <= money)
        {
            money -= allyPrices[selectedTroop];
            if (buildingTroops.Count == 0)
            {
                currentBuildTimer = allyBuildTime[selectedTroop];
            }
            buildingTroops.Enqueue(selectedTroop);
            
            //GameObject instantiated = Instantiate(selectedTroop, transform.position, Quaternion.identity);            
        }
    }

    public void SpawnEpeiste()
    {
        selectedTroop = allyTypes[1];
        if (allyPrices[selectedTroop] <= money)
        {
            money -= allyPrices[selectedTroop];
            if (buildingTroops.Count == 0)
            {
                currentBuildTimer = allyBuildTime[selectedTroop];
            }
            buildingTroops.Enqueue(selectedTroop);
            //GameObject instantiated = Instantiate(selectedTroop, transform.position, Quaternion.identity);
        }
    }

    public void SpawnPaladin()
    {
        selectedTroop = allyTypes[2];
        if (allyPrices[selectedTroop] <= money)
        {
            money -= allyPrices[selectedTroop];
            if (buildingTroops.Count == 0)
            {
                currentBuildTimer = allyBuildTime[selectedTroop];
            }
            buildingTroops.Enqueue(selectedTroop);
            //GameObject instantiated = Instantiate(selectedTroop, transform.position, Quaternion.identity);
        }
    }
}

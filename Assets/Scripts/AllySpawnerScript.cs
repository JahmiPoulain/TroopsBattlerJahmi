using UnityEngine;
using System.Collections.Generic;
public class AllySpawnerScript : MonoBehaviour
{
    public static AllySpawnerScript instance;
    public GameObject[] allyTypes;
    public Dictionary<GameObject, int> allyPrices;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        allyPrices = new Dictionary<GameObject, int>();
        allyPrices.Add(allyTypes[0], 5);
        allyPrices.Add(allyTypes[1], 10);
        allyPrices.Add(allyTypes[2], 20);
    }

    void Update()
    {
        
    }
}

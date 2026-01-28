using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public List<Transform> alliesOnTerrain;
    public List<Transform> ennemiesOnTerrain;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        
    }
    public GameObject FindClosestAlly(Vector3 pos)
    {
        int alliesCount = alliesOnTerrain.Count;
        Transform closest = null;
        float smallestDist = Mathf.Infinity;
        for (int i = 0; i < alliesCount; i++)
        {
             float currentDist = (alliesOnTerrain[i].position - pos).sqrMagnitude;
            if (currentDist < smallestDist)
            {
                smallestDist = currentDist;
                closest = alliesOnTerrain[i];
            }
        }
        return closest.gameObject;
    }

    public Vector3 CheckForCollision(Vector3 pos, float sqrRad)
    {
        int amountOfCol = 0;
        
        for (int i = 0; i < alliesOnTerrain.Count; i++)
        {
            // direction entre moi et l'autre
            Vector3 dir = alliesOnTerrain[i].position - pos;
            // si la distance entre nous est plus petite que mon rayon + l'autre rayon
            if (dir.sqrMagnitude < (sqrRad + alliesOnTerrain[i].gameObject.GetComponent<SphereColliderScript>().sqrRadius) / 2)
            {                
                amountOfCol++;    
            }

            if (amountOfCol > 1)
            {
                // l'endroit aproximatif de la collision
                return dir.normalized * Mathf.Sqrt(sqrRad);
            }
        }

        for (int i = 0; i < ennemiesOnTerrain.Count; i++)
        {
            // direction entre moi et l'autre
            Vector3 dir = ennemiesOnTerrain[i].position - pos;
            // si la distance entre nous est plus petite que mon rayon + l'autre rayon
            if (dir.sqrMagnitude < (sqrRad + ennemiesOnTerrain[i].gameObject.GetComponent<SphereColliderScript>().sqrRadius) / 2)
            {
                amountOfCol++;
            }

            if (amountOfCol > 1)
            {
                // l'endroit aproximatif de la collision
                return dir.normalized * Mathf.Sqrt(sqrRad);
            }
        }

        // pas de collision, on retourne 0,0,0
        return new Vector3 (0,0,0);
    }
}

using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public List<Transform> alliesOnTerrain;
    public List<Transform> ennemiesOnTerrain;

    public int ennemyKilled;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // trouver l'allier le plus proche
    public GameObject FindClosestAlly(Vector3 pos)
    {
        int alliesCount = alliesOnTerrain.Count;
        Transform closest = null;
        float smallestDist = Mathf.Infinity;
        // pour tout les alliers
        for (int i = 0; i < alliesCount; i++)
        {
            Vector3 currentPos = alliesOnTerrain[i].position;
            if (currentPos != pos) // si se n'est pas nous
            {
                // calcul de la distance sans prendre en compte le rayon
                float currentDist = (alliesOnTerrain[i].position - pos).sqrMagnitude;
                // si la distance est plus petite que la precedente
                if (currentDist < smallestDist)
                {
                    // elle devient la plus petite distance
                    smallestDist = currentDist;
                    closest = alliesOnTerrain[i];
                }
            }
        }
        if (closest != null)
        {
            return closest.gameObject;
        }
        return null;
    }

    // trouver l'ennemi le plus proche
    public GameObject FindClosestEnemy(Vector3 pos)
    {
        int enemiesCount = ennemiesOnTerrain.Count;
        Transform closest = null;
        float smallestDist = Mathf.Infinity;
        // pour tout les alliers
        for (int i = 0; i < enemiesCount; i++)
        {
            if (ennemiesOnTerrain[i] != null)
            {
                Vector3 currentPos = ennemiesOnTerrain[i].position;
                if (currentPos != pos) // si se n'est pas nous
                {
                    // calcul de la distance sans prendre en compte le rayon
                    float currentDist = (ennemiesOnTerrain[i].position - pos).sqrMagnitude;
                    // si la distance est plus petite que la precedente
                    if (currentDist < smallestDist)
                    {
                        // elle devient la plus petite distance
                        smallestDist = currentDist;
                        closest = ennemiesOnTerrain[i];
                    }
                }
            }
        }
        if (closest != null)
        {
            return closest.gameObject;
        }
        return null;
    }

    // trouver une collision
    public Vector3 CheckForCollision(Vector3 pos, float sqrRad)
    {
        int amountOfCol = 0;

        for (int i = 0; i < alliesOnTerrain.Count; i++)
        {
            // direction entre moi et l'autre
            Vector3 dir = alliesOnTerrain[i].position - pos;
            // si la distance entre nous est plus petite que mon rayon + l'autre rayon
            if (dir.sqrMagnitude < (sqrRad + alliesOnTerrain[i].gameObject.GetComponent<SphereColliderScript>().sqrRadius) / 4)
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
            if (dir.sqrMagnitude < (sqrRad + ennemiesOnTerrain[i].gameObject.GetComponent<SphereColliderScript>().sqrRadius) / 4)
            {
                amountOfCol++;
            }

            if (amountOfCol > 1) // on collide avec sois meme donc seule la deuxieme collision compte
            {
                // l'endroit aproximatif de la collision
                return dir.normalized * Mathf.Sqrt(sqrRad);
            }
        }

        // pas de collision, on retourne 0,0,0
        return new Vector3(0, 0, 0);
    }

    public Vector3 CheckForEnemyCollision(Vector3 pos, float sqrRad)
    {
        

        for (int i = 0; i < alliesOnTerrain.Count; i++)
        {
            // direction entre moi et l'autre
            Vector3 dir = alliesOnTerrain[i].position - pos;
            // si la distance entre nous est plus petite que mon rayon + l'autre rayon
            if (dir.sqrMagnitude < (sqrRad + alliesOnTerrain[i].gameObject.GetComponent<SphereColliderScript>().sqrRadius) / 4)
            {
                //Debug.Log("col ennemy");
                // l'endroit aproximatif de la collision
                return dir.normalized * Mathf.Sqrt(sqrRad);
                
            }

        }

        return new Vector3(0, 0, 0);
    }
    public void EnemyDamage(Transform enemy)
    {       
        AllySpawnerScript.instance.hp--;
        EnemyKilled(enemy);
    }
    public void EnemyKilled(Transform enemy)
    {
        if (enemy != null) 
        {           
            ennemiesOnTerrain.Remove(enemy);            
        }
        ennemyKilled++;
        AllySpawnerScript.instance.money++;
        Destroy(enemy.gameObject);
    }
}

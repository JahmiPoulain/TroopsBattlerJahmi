using UnityEngine;

public class SphereColliderScript : MonoBehaviour
{
    //GameManagerScript gameManagerScript;    
    public float radius;
    public float sqrRadius;   
    Vector3 direction;
    public float bounceVelocity;
    public float velocityDecay;
    public float currentBounceVelocity;
    public Vector3 currentBounceDirection;
    public bool isAlly;

    void Start()
    {
        if (isAlly)
        {
            GameManagerScript.instance.alliesOnTerrain.Add(transform);
        }
        else
        {
            GameManagerScript.instance.ennemiesOnTerrain.Add(transform);
        }
        
        //ameManagerScript = 
    }

    void Update()
    {
        if (currentBounceVelocity > 0)
        {
            currentBounceVelocity -= velocityDecay * Time.deltaTime;
        }
    }

    public Vector3 MoveThere(Vector3 currentPos, Vector3 targetPos)
    {
        // le vector3 d'une potentielle collision
       // Vector3 nextTargetPos = targetPos + currentBounceDirection * currentBounceVelocity;
        Vector3 nextPosCollision = GameManagerScript.instance.CheckForCollision(targetPos, sqrRadius);

        // si il y a une collision
        if (nextPosCollision != new Vector3(0, 0, 0))
        {
            //Debug.Log(nextPosCollision + " " + currentPos);
            return currentPos - nextPosCollision.normalized * Time.deltaTime;            
        }
        //Debug.Log("rien");
        return targetPos;        
    }

    public Vector3 MoveEnemyThere(Vector3 currentPos, Vector3 targetPos)
    {
        // le vector3 d'une potentielle collision
        // Vector3 nextTargetPos = targetPos + currentBounceDirection * currentBounceVelocity;
        Vector3 nextPosCollision = GameManagerScript.instance.CheckForEnemyCollision(targetPos, sqrRadius);

        // si il y a une collision
        if (nextPosCollision != new Vector3(0, 0, 0))
        {
            //GetComponent<EnemyScript>().isAlive = false;
            //Debug.Log(nextPosCollision + " " + currentPos);
            
            GameManagerScript.instance.EnemyKilled(transform);
            

            return currentPos;
        }
        //Debug.Log("rien");
        if (targetPos.magnitude < 1)
        {
            GameManagerScript.instance.EnemyDamage(transform);
        }
        return targetPos;
    }

    public void SetRadius(float rad)
    {
        radius = rad;
        sqrRadius = rad * rad;
        bounceVelocity = 1 / radius;
    }

    /*bool CheckCollision(Vector3 pos)
    {
        if (gameManagerScript.)
        return false;
    }*/
}

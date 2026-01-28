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

    void Start()
    {
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
            return currentPos;
            
        }
        //Debug.Log("rien");
        return targetPos;        
    }

    public Vector3 MoveEnemyThere(Vector3 currentPos, Vector3 targetPos)
    {
        // le vector3 d'une potentielle collision
        // Vector3 nextTargetPos = targetPos + currentBounceDirection * currentBounceVelocity;
        Vector3 nextPosCollision = GameManagerScript.instance.CheckForCollision(targetPos, sqrRadius);

        // si il y a une collision
        if (nextPosCollision != new Vector3(0, 0, 0))
        {
            //Debug.Log(nextPosCollision + " " + currentPos);
            return currentPos;

        }
        //Debug.Log("rien");
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

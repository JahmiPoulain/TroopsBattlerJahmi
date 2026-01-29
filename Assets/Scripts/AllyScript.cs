using UnityEngine;

[RequireComponent(typeof(SphereColliderScript))] // on doit avoir un script collider
public class AllyScript : MonoBehaviour
{
    SphereColliderScript sphereColliderScript;
    public float speed;
    public float radius;

    public GameObject currentTarget;
    void Start()
    {
        sphereColliderScript = GetComponent<SphereColliderScript>();
        sphereColliderScript.isAlly = true;
        sphereColliderScript.SetRadius(radius);
        transform.localScale = Vector3.one * radius;
    }

    void Update()
    {
        currentTarget = GameManagerScript.instance.FindClosestEnemy(transform.position);
        if (currentTarget != null)
        {
            Debug.Log("targeted");
            try
            {
                Vector3 nextPos = transform.position + (currentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime; // la prochaine position
                transform.position = sphereColliderScript.MoveThere(transform.position, nextPos);
            }
            catch 
            {
                currentTarget = GameManagerScript.instance.FindClosestEnemy(transform.position);
            }

        }       
    }
}

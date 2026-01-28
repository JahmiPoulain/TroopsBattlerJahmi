using UnityEngine;
[RequireComponent(typeof(SphereColliderScript))] // on doit avoir un script collider
public class EnemyScript : MonoBehaviour
{
    SphereColliderScript sphereColliderScript;
    public float speed;
    public float radius;


    public GameObject currentTarget;
    void Start()
    {
        currentTarget = AllySpawnerScript.instance.gameObject;
        sphereColliderScript = GetComponent<SphereColliderScript>();
        sphereColliderScript.SetRadius(radius);
        transform.localScale = Vector3.one * radius;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentTarget != null)
        {
            Vector3 nextPos = transform.position + (currentTarget.transform.position - transform.position).normalized * speed * Time.deltaTime; // la prochaine position
            transform.position = sphereColliderScript.MoveThere(transform.position, nextPos);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(SphereColliderScript))] // on doit avoir un script collider
public class AllyScript : MonoBehaviour
{
    SphereColliderScript sphereColliderScript;
    public float speed;
    public float radius;

    GameObject currentTarget;
    void Start()
    {
        sphereColliderScript = GetComponent<SphereColliderScript>();
        sphereColliderScript.SetRadius(radius);
        transform.localScale = Vector3.one * radius;
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 nextPos = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.position = sphereColliderScript.MoveThere(transform.position, nextPos);        
    }
}

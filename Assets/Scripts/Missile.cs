using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;

    public PlayerController target;
    public PlayerController emitter;
    public float speed = 2;
    public float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(explosionPrefab, position, rotation);
        Destroy(gameObject);
    }

}

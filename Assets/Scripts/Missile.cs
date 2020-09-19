using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;

    public Spaceship emitter;
    public float speed = 4;
    public float damage = 10;

    // Start is called before the first frame update
    void Start() {}

    public void Initiate(Spaceship emitter, Spaceship target)
    {
        this.emitter = emitter;
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("missile colision");
        Spaceship collidedPlayer = collision.gameObject.GetComponent<Spaceship>();
        if (collidedPlayer != null && collidedPlayer == emitter) return;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        if (explosionPrefab) Instantiate(explosionPrefab, position, rotation);
        else Debug.LogError("unassiged explosionPrefab in missile");
        if (collidedPlayer != null && collidedPlayer != emitter)
        {
            Debug.Log("missile colision w target");
            collidedPlayer.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}

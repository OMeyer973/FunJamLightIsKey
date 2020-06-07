using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;

    public Spaceship target;
    public Spaceship emitter;
    public float speed = 4;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("missile colision");
        Spaceship collidedPlayer = collision.gameObject.GetComponent<Spaceship>();
        if (collidedPlayer != null && collidedPlayer == emitter) return;
        ContactPoint2D contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;
        Instantiate(explosionPrefab, position, rotation);

        if (collidedPlayer != null && collidedPlayer == target)
        {
            Debug.Log("missile colision w target");
            collidedPlayer.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}

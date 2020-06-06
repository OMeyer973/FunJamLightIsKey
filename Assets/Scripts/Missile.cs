using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public PlayerController target;
    public PlayerController emitter;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}

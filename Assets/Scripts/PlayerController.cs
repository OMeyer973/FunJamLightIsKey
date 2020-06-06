using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float Speed =.5f;
    public float acceleration = .2f;
    public float deceleration = 1f;

    public float baseRotationSpeed;
    public Transform ReferencePlanet;

    private float eps = .01f; // epsilon on the target speed to determine if we use acceleration or deceleration factor
    private Vector3 currSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 targetSpeed = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Speed;
        if (targetSpeed.magnitude <= eps)
            currSpeed = Vector3.Lerp(currSpeed, targetSpeed, deceleration);
        else currSpeed = Vector3.Lerp(currSpeed, targetSpeed, acceleration);

        transform.position += currSpeed * Time.fixedDeltaTime;

        transform.RotateAround(ReferencePlanet.transform.position, Vector3.forward, baseRotationSpeed * Time.fixedDeltaTime);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float rotationSpeed = 50f;
    public float normalSpeed = 50f;
    public float acceleration = .2f;
    public float deceleration = 1f;

    public float baseRotationSpeed = 30f;
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
        Move()
    }

    private void Move()
    {
        Vector3 targetDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        /*
        // controls full classique
        if (targetSpeed.magnitude <= eps)
            currSpeed = Vector3.Lerp(currSpeed, targetSpeed, deceleration);
        else currSpeed = Vector3.Lerp(currSpeed, targetSpeed, acceleration);
        transform.position += currSpeed * Time.fixedDeltaTime;
        */

        Vector3 orbitNormal = (ReferencePlanet.transform.position - transform.position).normalized;
        Vector3 orbitTangent = Vector3.Cross(orbitNormal, Vector3.forward);

        float currRotationSpeed = Vector3.Dot(targetDirection, orbitTangent) * rotationSpeed;
        float currNormalSpeed = Vector3.Dot(targetDirection, orbitNormal) * normalSpeed;

        transform.RotateAround(ReferencePlanet.transform.position, Vector3.forward, currRotationSpeed * Time.fixedDeltaTime);
        transform.position += currNormalSpeed * orbitNormal;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    #region MEMBERS
    // INPUTS
    [SerializeField]
    private int inputIndex = 0;
    private Vector2 inputMove;

    [Header("Movements")]
    public float angularSpeed = 500f;
    public float normalSpeed = 10f;
    public float angularAcceleration = .05f;
    public float angularDeceleration = .01f;
    public float normalAcceleration = .05f;
    public float normalDeceleration = .05f;
    private float eps = .01f; // epsilon on the target speed to determine if we use acceleration or deceleration factor
    private float currAngularSpeed;
    private float currNormalSpeed;

    [Header("Astronomy physics stuff")]
    public float baseRotationSpeed = 30f;
    public Transform ReferencePlanet;

    [Header("FIGHT")]
    public float healthPoints = 100;
    public Spaceship enemy;
    public Missile missilePrefab;
    #endregion MEMBERS

    #region METHODS
    // Start is called before the first frame update
    void Start() {}

// called at player initialization to link it to the input prefab thingy
public int GetInputIndex() { return inputIndex; }

    // Update is called once per frame
    void Update() {}

    private void FixedUpdate() { Move(); }

    private void Move()
    {
        Vector3 targetDirection = new Vector3(inputMove.x, inputMove.y, 0);
        /*
        // controls full classique
        if (targetSpeed.magnitude <= eps)
            currSpeed = Vector3.Lerp(currSpeed, targetSpeed, deceleration);
        else currSpeed = Vector3.Lerp(currSpeed, targetSpeed, acceleration);
        transform.position += currSpeed * Time.fixedDeltaTime;
        */

        //Vector3 orbitNormal = (Vector3.zero - transform.position).normalized;
        Vector3 planetSpaceShip = ReferencePlanet.transform.position - transform.position;
        Vector3 orbitNormal = (planetSpaceShip).normalized;
        Vector3 orbitTangent = Vector3.Cross(orbitNormal, Vector3.forward);

        float targetAngularSpeed = Vector3.Dot(targetDirection, orbitTangent) * angularSpeed;
        float targetNormalSpeed = Vector3.Dot(targetDirection, orbitNormal) * normalSpeed;

        if (Mathf.Abs(targetAngularSpeed) <= eps)
            currAngularSpeed = Mathf.Lerp(currAngularSpeed, targetAngularSpeed, angularDeceleration);
        else currAngularSpeed = Mathf.Lerp(currAngularSpeed, targetAngularSpeed, angularAcceleration);

        if (Mathf.Abs(targetNormalSpeed) <= eps)
            currNormalSpeed = Mathf.Lerp(currNormalSpeed, targetNormalSpeed, normalDeceleration);
        else currNormalSpeed = Mathf.Lerp(currNormalSpeed, targetNormalSpeed, normalAcceleration);

        //transform.RotateAround(Vector3.zero, Vector3.forward, currAngularSpeed * Time.fixedDeltaTime);
        transform.RotateAround(ReferencePlanet.transform.position, Vector3.forward, currAngularSpeed / planetSpaceShip.magnitude * Time.fixedDeltaTime);
        transform.position += orbitNormal * currNormalSpeed * Time.fixedDeltaTime;
    }

    public void setInputMoveVector(Vector2 vec2) { inputMove = vec2; }

    public void ShootEnemy()
    {
        Missile missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        missile.emitter = this;
        missile.target = enemy;
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
            Die();
    }

    private void Die()
    {
        // todo
    }

    #endregion METHODS
}

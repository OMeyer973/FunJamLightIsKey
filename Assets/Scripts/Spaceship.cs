using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    #region MEMBERS
    // INPUTS
    [SerializeField]
    private int inputIndex = 0;
    private Vector2 inputMove;

    [Header("MOVEMENT")]
    public float angularSpeed = 500f;
    public float normalSpeed = 10f;
    public float angularAcceleration = .05f;
    public float angularDeceleration = .01f;
    public float normalAcceleration = .05f;
    public float normalDeceleration = .05f;
    private float eps = .01f; // epsilon on the target speed to determine if we use acceleration or deceleration factor
    private float currAngularSpeed;
    private float currNormalSpeed;

    [Header("ASTRONOMY")]
    public float baseRotationSpeed = 3f;
    public Transform referencePlanet;

    [Header("FIGHT")]
    [SerializeField]
    private float maxHealthPoints = 100;
    public float healthPoints;
    public Spaceship enemy;
    public Missile missilePrefab;
    private Turret turret;
    #endregion MEMBERS

    #region METHODS
    // Start is called before the first frame update
    void Start() 
    {
        healthPoints = maxHealthPoints;
        turret = GetComponentInChildren<Turret>();
        if (turret) turret.Initiate(this, enemy); 
        else Debug.LogError("Spaceship can't find child turret");
    }

// called at player initialization to link it to the input prefab thingy
public int GetInputIndex() { return inputIndex; }

    void Update() {}

    private void FixedUpdate()
    {
        Move();
        turret.Aim();
    }

    private void Move()
    {
        Vector3 targetDirection = new Vector3(inputMove.x, 0, inputMove.y);
        /*
        // controls full classique
        if (targetSpeed.magnitude <= eps)
            currSpeed = Vector3.Lerp(currSpeed, targetSpeed, deceleration);
        else currSpeed = Vector3.Lerp(currSpeed, targetSpeed, acceleration);
        transform.position += currSpeed * Time.fixedDeltaTime;
        */

        //Vector3 orbitNormal = (Vector3.zero - transform.position).normalized;
        Vector3 planetSpaceShip = referencePlanet.transform.position - transform.position;
        Vector3 orbitNormal = (planetSpaceShip).normalized;
        Vector3 orbitTangent = Vector3.Cross(orbitNormal, Vector3.up);

        float targetAngularSpeed = Vector3.Dot(targetDirection, orbitTangent) * angularSpeed;
        float targetNormalSpeed = Vector3.Dot(targetDirection, orbitNormal) * normalSpeed;

        if (Mathf.Abs(targetAngularSpeed) <= eps)
            currAngularSpeed = Mathf.Lerp(currAngularSpeed, targetAngularSpeed, angularDeceleration);
        else currAngularSpeed =Mathf.Lerp(currAngularSpeed, targetAngularSpeed, angularAcceleration);

        if (Mathf.Abs(targetNormalSpeed) <= eps)
            currNormalSpeed = Mathf.Lerp(currNormalSpeed, targetNormalSpeed, normalDeceleration);
        else currNormalSpeed = Mathf.Lerp(currNormalSpeed, targetNormalSpeed, normalAcceleration);

        Vector3 prevPosition = transform.position;
        //transform.RotateAround(Vector3.zero, Vector3.forward, currAngularSpeed * Time.fixedDeltaTime);
        transform.RotateAround(referencePlanet.transform.position, Vector3.up, currAngularSpeed / planetSpaceShip.magnitude * Time.fixedDeltaTime);
        Vector3 newPosition = transform.position + orbitNormal * currNormalSpeed * Time.fixedDeltaTime;
        newPosition.y = 0;
        transform.position = newPosition;
        transform.LookAt(newPosition + newPosition - prevPosition, Vector3.up);
    }

    public void setInputMoveVector(Vector2 vec2) { inputMove = vec2; }

    public void ShootEnemy() { turret.Shoot(missilePrefab); }
    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
            Die();
    }

    public void Regen(float healthAmount)
    {
        healthPoints += healthAmount;
        if (healthPoints > maxHealthPoints) { healthPoints = maxHealthPoints; }
    }

    private void Die()
    {
        // todo
    }

    #endregion METHODS
}

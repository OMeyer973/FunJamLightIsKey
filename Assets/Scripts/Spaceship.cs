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
    public float rotationSpeed = 50f;
    public float normalSpeed = 50f;
    public float acceleration = .2f; // NOT USED
    public float deceleration = 1f; // NOT USED
    private float eps = .01f; // NOT USED // epsilon on the target speed to determine if we use acceleration or deceleration factor
    private Vector3 currSpeed;

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

        Vector3 orbitNormal = (Vector3.zero - transform.position).normalized;
        //Vector3 orbitNormal = (ReferencePlanet.transform.position - transform.position).normalized;
        Vector3 orbitTangent = Vector3.Cross(orbitNormal, Vector3.forward);
        float currRotationSpeed = Vector3.Dot(targetDirection, orbitTangent) * rotationSpeed;
        float currNormalSpeed = Vector3.Dot(targetDirection, orbitNormal) * normalSpeed;

        transform.RotateAround(Vector3.zero, Vector3.forward, currRotationSpeed * Time.fixedDeltaTime);
        //transform.RotateAround(ReferencePlanet.transform.position, Vector3.forward, currRotationSpeed * Time.fixedDeltaTime);
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

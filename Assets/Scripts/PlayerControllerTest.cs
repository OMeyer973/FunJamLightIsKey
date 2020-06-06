using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTest : MonoBehaviour
{
    private Vector2 InputMovement;
    public float speed = 10f;

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
        Vector3 movment = new Vector3(InputMovement.x, InputMovement.y, 0f);
        transform.Translate(movment * speed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        InputMovement = value.Get<Vector2>();
        // Debug.Log("move");
    }
    private void OnShoot()

    {
        Debug.Log("button A pressed (shoot)");
    }
}

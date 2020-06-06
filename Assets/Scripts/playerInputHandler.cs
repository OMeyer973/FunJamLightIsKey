using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController playerController;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

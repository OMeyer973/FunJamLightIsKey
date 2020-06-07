using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Spaceship playerController;

    private void Awake()
    {
        // get input for this controller
        playerInput = GetComponent<PlayerInput>();
        int index = playerInput.playerIndex;

        // assign playerController
        var playerControllers = FindObjectsOfType<Spaceship>();
        playerController = playerControllers.FirstOrDefault(p => p.GetInputIndex() == index);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMove(InputValue value)
    {
        playerController.setInputMoveVector(value.Get<Vector2>());
    }

}

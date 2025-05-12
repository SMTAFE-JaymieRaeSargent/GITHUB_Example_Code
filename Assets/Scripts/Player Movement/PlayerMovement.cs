using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //the direction we are moving
    [SerializeField] Vector3 _moveDirection;
    //the reference to the CharacterController
    [SerializeField] CharacterController _characterController;
    //walk, crouch, sprint, jump, gravity
    [SerializeField] float _movementSpeed, _walk = 5, _run = 10, _crouch = 2.5f, _jump = 8, _gravity = 20;
    Vector2 newInput;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void SpeedControls()
    {
        if (Input.GetKey(KeybindManager.keys["Sprint"]))
        {
            _movementSpeed = _run;
        }
        else if (Input.GetKey(KeybindManager.keys["Crouch"]))
        {
            _movementSpeed = _crouch;
        }
        else
        {
            _movementSpeed = _walk;
        }
    }
    void Move()
    {
        //if our reference to the character controller has a value aka we ected it yay!!! woop 
        if (_characterController != null)
        {
            //check of we are on the ground so we can move coz thats how people work 
            if (_characterController.isGrounded)
            {
                if (Input.GetKey(KeybindManager.keys["Left"]))
                {
                    newInput.x = -1;
                }
                else if (Input.GetKey(KeybindManager.keys["Right"]))
                {
                    newInput.x = 1;
                }
                else
                {
                    newInput.x = 0;
                }
                if (Input.GetKey(KeybindManager.keys["Backward"]))
                {
                    newInput.y = -1;
                }
                else if (Input.GetKey(KeybindManager.keys["Forward"]))
                {
                    newInput.y = 1;
                }
                else
                {
                    newInput.y = 0;
                }
                // This sets _moveDirection based on player input from the Horizontal and Vertical axes.
                _moveDirection = new Vector3(newInput.x, 0, newInput.y);
                
                //move in the direction we are facing
                _moveDirection = transform.TransformDirection(_moveDirection);
                // This multiplies the direction by the current movement speed.
                _moveDirection *= _movementSpeed;
                // This checks if the player has pressed the Jump button.
                if (Input.GetKey(KeybindManager.keys["Jump"]))
                {
                    // This sets the y component of _moveDirection to the jump force, making the character jump.
                    _moveDirection.y = _jump;
                }
            }
            //add gravity to direction
            _moveDirection.y -= _gravity * Time.deltaTime;
            //apply movement
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        SpeedControls();
        Move();
    }

}

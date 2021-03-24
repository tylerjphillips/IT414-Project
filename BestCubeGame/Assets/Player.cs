using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float jumpHeight = 8.0f;
    public float gravity = 10.0f;
    private Vector3 movingDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Update()
    {
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            movingDirection.y = jumpHeight;
        }
        movingDirection.y -= gravity * Time.deltaTime;
        controller.Move(movingDirection * Time.deltaTime);
    }
}

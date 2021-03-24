using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InvokerCode : MonoBehaviour
{
    AbstractCommand keyJump;
    public GameObject aGameObject;
    CharacterController aCharacterController;
    private Vector3 gravity = Vector3.zero;


    
    void Start()
    {
        keyJump = new Jump(); // Initializes a new jump command
        aCharacterController = aGameObject.GetComponent<CharacterController>();
        

    }

    void Update()
    {
        // Commands Execute Methods Start Here:

        // Jump Command execute method
        if (aCharacterController.isGrounded && Input.GetButton("Jump"))
        {
            keyJump.Execute(aGameObject, aCharacterController);
        }

        // Constantly moving back down towards surface after command is executed
        else
        {
            gravity.y -= 1 * Time.deltaTime;
            aCharacterController.Move(gravity * Time.deltaTime);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 5;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if(Input.GetKey("w")) {
            controller.SimpleMove(controller.transform.forward * speed);
        }
        if(Input.GetKey("a")) {
            controller.SimpleMove(controller.transform.right * -speed);

        }
        else if(Input.GetKey("s")) {
            controller.SimpleMove(controller.transform.forward * -speed);

        }
        else if(Input.GetKey("d")) {
            controller.SimpleMove(controller.transform.right * speed);

        }
    }
}

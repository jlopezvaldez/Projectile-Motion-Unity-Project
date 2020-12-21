using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBasic : MonoBehaviour
{
    public const float SPEED = 10f;
    private void HandleMovement() {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) {
            moveX += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveX -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveZ += 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveZ -= 1f;
        }

        Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDir * SPEED * Time.deltaTime;
    }

    void Update()
    {
        HandleMovement();
    }

    // private void OnTriggerStay(Collider c) {
    //     if (c.tag == "Turret") {
    //         print("Player within radius!");
    //         if (Input.GetMouseButton(0))
    //         {
    //             moveX += 1f;
    //         }
    //     }
    // }
}

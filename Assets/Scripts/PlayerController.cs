using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 direction;
    [SerializeField]
    float multiplier = 3f;

    private int jumpCount = 1;

    bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;   
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < 10)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                multiplier *= 1.5f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) 
            {
                multiplier /= 1.5f;
            }
            //transform.position += multiplier * velocity * Time.deltaTime;
            rb.AddForce(direction * multiplier);
        }
    }
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        float movementX = input.x;
        float movementZ = input.y;
        

        direction = new Vector3(movementX, 0, movementZ) * multiplier;
    }

    void OnJump()
    {
        if (onGround || jumpCount > 0) {
            Vector3 jumpForce = new Vector3(0, 200, 0);
            rb.AddForce(jumpForce);
            jumpCount--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            onGround = false;
            jumpCount = 1;
        }
    }
}

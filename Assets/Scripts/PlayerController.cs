using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 direction;
    [SerializeField]
    float multiplier = 3f;

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
        if (onGround) {
            Vector3 jumpForce = new Vector3(0, 200, 0);
            rb.AddForce(jumpForce);
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
        }
    }
}

using System;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    private bool isGrounded = true;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isGrounded)
        {
            body.gravityScale = 2.1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isGrounded)
            {
                body.gravityScale += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                body.AddForce(Vector3.up * jumpSpeed);
                isGrounded = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col);
        isGrounded = true;
    }

}


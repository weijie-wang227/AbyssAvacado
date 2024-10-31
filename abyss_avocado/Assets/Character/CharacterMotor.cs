using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    private bool isGrounded = true;
    private Rigidbody2D body;
    private SpriteRenderer renderer;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
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
            renderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            renderer.flipX = false;
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
        isGrounded = true;
        if (col.collider.CompareTag("Enemy") && !Player.Instance.invulnerable)
        {
            body.AddForce((col.transform.position - transform.position).normalized * -500f);
            Player.Instance.Damage(2);
        }
    }

}


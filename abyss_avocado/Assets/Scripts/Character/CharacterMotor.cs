using System;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    private bool isGrounded = false;
    private bool rightContact = false;
    private bool leftContact = false;
    private Rigidbody2D body;
    private new SpriteRenderer renderer;
    private Vector2 currentPos;
    private LayerMask mask;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        mask = LayerMask.GetMask("Platform");
    }
    void Update()
    {
        currentPos = transform.position;
        if (Physics2D.Linecast(currentPos - new Vector2(-0.25f, 0.5f), currentPos - new Vector2(0.25f, 0.5f), mask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics2D.Linecast(currentPos - new Vector2(-0.35f, -0.5f), currentPos - new Vector2(-0.35f, 0.4f), mask))
        {
            rightContact = true;
        }
        else
        {
            rightContact = false;
        }

        if (Physics2D.Linecast(currentPos - new Vector2(0.35f, -0.5f), currentPos - new Vector2(0.35f, 0.4f), mask))
        {
            leftContact = true;
        }
        else
        {
            leftContact = false;
        }

        if (isGrounded)
        {
            body.gravityScale = 2.1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !leftContact)
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            renderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !rightContact)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            renderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isGrounded)
            {
                body.gravityScale += 1.5f;
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
        if (col.collider.CompareTag("Enemy"))
        {
            body.AddForce((col.transform.position - transform.position).normalized * -5f, ForceMode2D.Impulse);
            col.rigidbody.AddForce((col.transform.position - transform.position).normalized * 5f, ForceMode2D.Impulse);
        }
    }



}


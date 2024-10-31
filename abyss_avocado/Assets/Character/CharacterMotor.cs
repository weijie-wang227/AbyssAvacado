using System;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    [SerializeField] private bool isGrounded = false;
    private Rigidbody2D body;
    private new SpriteRenderer renderer;
    private Vector2 currentPos;
    private Transform test;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        test = transform.GetChild(0);
    }
    void Update()
    {
        currentPos = transform.position;
        test.position = currentPos - new Vector2(-0.25f, 0.5f);
        if (Physics2D.Linecast(currentPos - new Vector2(-0.25f, 0.5f), currentPos - new Vector2(0.25f, 0.5f)))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

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
        if (col.collider.CompareTag("Enemy") && !Player.Instance.invulnerable)
        {
            body.AddForce((col.transform.position - transform.position).normalized * -500f);
        }
    }



}

